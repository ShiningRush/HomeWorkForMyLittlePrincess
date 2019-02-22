using Clear.UserPermission.Application.Dtos.Modules;
using PlatformService.BridgeComponent.WebApiConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Application
{
    /// <summary>
    /// 模块服务
    /// </summary>
    public interface IModuleAppService : IOpenWebApi
    {
        /// <summary>
        /// 获取所有模块列表
        /// </summary>
        /// <returns></returns>
        List<ModuleDto> GetAllModules();
        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="deleteList">待删除模块的id</param>
        void DeleteModule(List<Guid> deleteList);
        /// <summary>
        /// 获取模块权限列表
        /// </summary>
        /// <param name="inputDto">入参</param>
        /// <returns></returns>
        List<GetModuleAuthsOutput> GetModuleAuths(GetModuleAuthsInput inputDto);
        /// <summary>
        /// 插入模块权限
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        Guid InsertModuleAuth(InsertModuleAuthInput inputDto);
        /// <summary>
        /// 更新模块权限
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        void UpdateModuleAuth(UpdateModuleAuthInput inputDto);

        /// <summary>
        /// 删除模块权限
        /// </summary>
        /// <param name="deleteList"></param>
        void DeleteModuleAuth(List<Guid> deleteList);
    }
}
