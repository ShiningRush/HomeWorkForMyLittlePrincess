using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Json;
using Castle.Core.Logging;
using Castle.DynamicProxy;
using PlatformService.BridgeComponent.Service;
using PlatformService.BridgeComponent.Service.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Clear.CommonContext.Infrastructure.Extensions;

namespace Clear.CommonContext.Domain.OperationLogAggregate
{
    /// <summary>
    /// 操作日志帮助类
    /// </summary>
    public class OperationLogHelper : IOperationLogHelper, Abp.Dependency.ITransientDependency
    {
        public const string SESSIONINFO_ARGUMENT_NAME = "SessionInfo";

        public ILogger Logger { get; set; } = NullLogger.Instance;
        public ISessionManager SessionManager { get; set; }
        public IServiceContext ServiceContext { get; set; }
        

        private readonly IRepository<Operationlog, Guid> _operationlogRepository;
        private readonly IRepository<OpeartionlogConfig, Guid> _opeartionlogConfigRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private static object _lock = new object();
        private static List<OpeartionlogConfig> _opeartionlogConfigs = null;
        private List<OpeartionlogConfig> OpeartionlogConfigs
        {
            get
            {
                if (_opeartionlogConfigs == null)
                    lock (_lock)
                    {
                        if (_opeartionlogConfigs == null)
                            _opeartionlogConfigs = _opeartionlogConfigRepository.GetAllList();
                    }

                return _opeartionlogConfigs;
            }
        }

        public OperationLogHelper(IRepository<Operationlog, Guid> operationlogRepository
            , IRepository<OpeartionlogConfig, Guid> opeartionlogConfigRepository
            , IUnitOfWorkManager unitOfWorkManager)
        {
            _operationlogRepository = operationlogRepository;
            _opeartionlogConfigRepository = opeartionlogConfigRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 创建操作日志对象
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        public Operationlog CreateOperationlog(IInvocation invocation)
        {
            return CreateOperationlog(invocation, CreateArgumentsDictionary(invocation.Method, invocation.Arguments));
        }

        /// <summary>
        /// 创建操作日志对象
        /// </summary>
        /// <param name="invocation"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public Operationlog CreateOperationlog(IInvocation invocation, IDictionary<string, object> arguments)
        {
            var opeartionlogConfig = GetOpeartionlogConfig(invocation);
            string jsonString = ConvertArgumentsToJson(arguments);
            var operationlog = new Operationlog
            {
                OperationUserId = GetOperationUserId(arguments),
                Parameters = jsonString,
                OperationType = opeartionlogConfig.OperationType,
                Module = opeartionlogConfig.Module,
            };

            try
            {
                operationlog.LogContext = opeartionlogConfig.LogFormat.Subtitute(ObjectFormat.Instance, arguments).TruncateWithPostfix(200);
                operationlog.BrowserInfo = ServiceContext.GetValue("User-Agent").TruncateWithPostfix(64);
                operationlog.ClientIpAddress = ServiceContext.ClientIP;
                operationlog.ClientName = ServiceContext.GetValue("ClientName");
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);
            }

            return operationlog;
        }

        /// <summary>
        /// 保存操作日志对象
        /// </summary>
        /// <param name="operationlog"></param>
        public async Task SaveAsync(Operationlog operationlog)
        {
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                await _operationlogRepository.InsertAsync(operationlog);
                await uow.CompleteAsync();
            }
        }

        /// <summary>
        /// 保存操作日志对象
        /// </summary>
        /// <param name="invocation"></param>
        public async Task SaveAsync(IInvocation invocation)
        {
            await SaveAsync(CreateOperationlog(invocation));
        }

        /// <summary>
        /// 是否需要记录操作日志
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        public bool ShouldRecordOperationLog(IInvocation invocation)
        {
            return GetOpeartionlogConfig(invocation) != null;
        }

        /// <summary>
        /// 获取操作人ID
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        private Guid? GetOperationUserId(IDictionary<string, object> arguments)
        {
            var sessionInfo = arguments.GetOrDefault(SESSIONINFO_ARGUMENT_NAME);
            if(sessionInfo!=null && sessionInfo is SessionInfo)
            {
                return (sessionInfo as SessionInfo).UserId;
            }
            return SessionManager.UserId;
        }


        /// <summary>
        /// 将参数转换成json
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        private string ConvertArgumentsToJson(IDictionary<string, object> arguments)
        {
            try
            {
                if (arguments.IsNullOrEmpty())
                {
                    return "{}";
                }

                var dictionary = new Dictionary<string, object>();

                foreach (var argument in arguments)
                {
                    if (argument.Value == null )
                    {
                        dictionary[argument.Key] = null;
                    }
                    else
                    {
                        dictionary[argument.Key] = argument.Value;
                    }
                }

                return dictionary.ToJsonString();
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);
                return "{}";
            }
        }

        /// <summary>
        /// 读取操作日志配置
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        private OpeartionlogConfig GetOpeartionlogConfig(IInvocation invocation)
        {
            string methodName = $"{invocation.TargetType.FullName}.{invocation.Method.Name}";
            return OpeartionlogConfigs.FirstOrDefault(s => s.MethodName.Equals(methodName));
        }

        /// <summary>
        /// 创建参数字典
        /// </summary>
        /// <param name="method"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public static Dictionary<string, object> CreateArgumentsDictionary(MethodInfo method, object[] arguments)
        {
            var parameters = method.GetParameters();
            var dictionary = new Dictionary<string, object>();

            for (var i = 0; i < parameters.Length; i++)
            {
                dictionary[parameters[i].Name] = arguments[i];
            }

            return dictionary;
        }

   
    }
}
