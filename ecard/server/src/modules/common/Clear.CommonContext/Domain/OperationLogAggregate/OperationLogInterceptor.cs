using Abp.Dependency;
using Castle.Core.Logging;
using Castle.DynamicProxy;
using PlatformService.BridgeComponent.Service.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.CommonContext.Domain.OperationLogAggregate
{
    /// <summary>
    /// 操作日志拦截器
    /// </summary>
    public class OperationLogInterceptor : IInterceptor
    {
        private readonly IOperationLogHelper _operationLogHelper;
        public ISessionManager SessionManager { get; set; } = NullSessionManager.Instance;
        public ILogger Logger { get; set; } = NullLogger.Instance;

        public OperationLogInterceptor(IOperationLogHelper operationLogHelper)
        {
            _operationLogHelper = operationLogHelper;
        }

        public void Intercept(IInvocation invocation)
        {
            var arguments = OperationLogHelper.CreateArgumentsDictionary(invocation.Method, invocation.Arguments);
            PreInjectionArguments(arguments);

            invocation.Proceed();
            try
            {
                PostInjectionArguments(arguments);
                if (_operationLogHelper.ShouldRecordOperationLog(invocation))
                {
                    _operationLogHelper.SaveAsync(_operationLogHelper.CreateOperationlog(invocation, arguments));
                }
            }
            catch (Exception ex)
            {
                Logger.Warn("记录操作日志发生失败",ex);
            }
           
        }

        /// <summary>
        /// Proceed执行之前注入参数
        /// </summary>
        /// <param name="arguments"></param>
        private void PreInjectionArguments(IDictionary<string, object> arguments)
        {
            var sessionInfo = SessionManager.GetSessionInfo();
            if (sessionInfo != null)
            {
                arguments.Add(OperationLogHelper.SESSIONINFO_ARGUMENT_NAME, SessionManager.GetSessionInfo());
            }
        }

        /// <summary>
        /// Proceed执行之后注入参数
        /// </summary>
        /// <param name="arguments"></param>
        private void PostInjectionArguments(IDictionary<string, object> arguments)
        {
            var sessionInfo = SessionManager.GetSessionInfo();
            if (sessionInfo != null && !arguments.ContainsKey(OperationLogHelper.SESSIONINFO_ARGUMENT_NAME))
            {
                arguments.Add(OperationLogHelper.SESSIONINFO_ARGUMENT_NAME, SessionManager.GetSessionInfo());
            }
        }
    }
}
