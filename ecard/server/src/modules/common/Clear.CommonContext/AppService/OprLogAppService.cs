using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clear.CommonContext.AppService.Dtos.OprLog;
using Abp.Domain.Repositories;
using Clear.CommonContext.Domain.OperationLogAggregate;
using YiBan.Common.Pages;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using PlatformService.BridgeComponent.Domain.Repositories;
using PlatformService.BridgeComponent.Domain.Entities;
using PlatformService.BridgeComponent.Extensions;

namespace Clear.CommonContext.AppService
{
    /// <summary>
    /// 操作日志服务
    /// </summary>
    public class OprLogAppService : ApplicationService, IOprLogAppService
    {
        private readonly IRepository<Operationlog, Guid> _operationlogRepository;
        private readonly IRepository<UserBase,Guid> _userRepository;

        public OprLogAppService(
            IRepository<Operationlog, Guid> operationlogRepository, IRepository<UserBase, Guid> userRepository)
        {
            _operationlogRepository = operationlogRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 获取操作日志列表
        /// </summary>
        /// <param name="inputDto">入参</param>
        /// <returns></returns>
        public PagerResult<GetOprLogsOutput> GetOprLogs(GetOprLogsInput inputDto)
        {
            var operationlogs = _operationlogRepository.GetAll()
                .WhereIf(!inputDto.ClientIpAddress.IsNullOrEmpty(), p => p.ClientIpAddress.Equals(inputDto.ClientIpAddress))
                .WhereIf(!inputDto.Module.IsNullOrEmpty(), p => p.Module.Equals(inputDto.Module))
                .WhereIf(!inputDto.OperationType.IsNullOrEmpty(), p => p.OperationType.Equals(inputDto.OperationType))
                .WhereIf(inputDto.StartTime.HasValue, p => p.CreateTime > inputDto.StartTime)
                .WhereIf(inputDto.EndTime.HasValue, p => p.CreateTime <= inputDto.EndTime)
                .OrderByDescending(s=>s.CreateTime).Select(operationlog => new GetOprLogsOutput() {
                    ClientIpAddress = operationlog.ClientIpAddress,
                    ClientName = operationlog.ClientName,
                    Context = operationlog.LogContext,
                    Creator = operationlog.OperationUserId,
                    Module = operationlog.Module,
                    OperationType = operationlog.OperationType,
                    CreateTime = operationlog.CreateTime,
                }).Page(inputDto);
            var userIds = operationlogs.Result.Where(s=>s.Creator!= null).Select(u => u.Creator.Value).Distinct().ToArray();
            var users = _userRepository.GetAll().Where(s => userIds.Contains(s.Id)).ToList();
            operationlogs.Result.ForEach((item)=> {
                if (!item.Creator.HasValue) return;
                item.CreatorName = users.FirstOrDefault(s => s.Id == item.Creator)?.UserName;
            });
            return operationlogs;
        }
    }
}


