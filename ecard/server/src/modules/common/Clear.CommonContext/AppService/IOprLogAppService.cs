using Clear.CommonContext.AppService.Dtos.OprLog;
using PlatformService.BridgeComponent.WebApiConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiBan.Common.Pages;

namespace Clear.CommonContext.AppService
{
    /// <summary>
    /// 操作日志服务
    /// </summary>
    public interface IOprLogAppService: IOpenWebApi
    {
        /// <summary>
        /// 获取操作日志列表
        /// </summary>
        /// <param name="inputDto">入参</param>
        /// <returns></returns>
        PagerResult<GetOprLogsOutput> GetOprLogs(GetOprLogsInput inputDto);
    }
}
