using PlatformService.BridgeComponent.WebApiConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiBan.Common.Pages;

namespace Clear.UserPermission.Application
{
    /// <summary>
    /// 角色服务接口
    /// </summary>
    public interface IRoleAppService : IOpenWebApi
    {
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="entity">添加实体</param>
        /// <returns>返回新增记录的主键Id</returns>
        string AddRole(Dtos.RoleDTO entity);

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="entity">更新实体</param>
        /// <returns></returns>
        void UpdateRole(Dtos.UpdateRoleInputDTO entity);
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="ids">删除id列表</param>
        /// <returns></returns>
        void DeleteRole(List<string> ids);
        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="inputDto">查询实体</param>
        /// <returns></returns>
        PagerResult<Dtos.GetRoleOutputDTO> GetRoles(Dtos.GetRoleInputDTO inputDto);
        /// <summary>
        /// 获取角色用户
        /// </summary>
        /// <param name="roleId">角色</param>
        /// <returns></returns>
        List<Dtos.GetRoleUserOutputDTO> GetRoleUser(string roleId);

        /// <summary>
        /// 保存角色用户信息
        /// </summary>
        /// <param name="dto"></param>
        void SaveRoleUser(Dtos.SaveRoleUserInputDTO dto);

        /// <summary>
        /// 获取角色模块和功能权限信息
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        Dtos.GetRoleModuleAuthOutputDTO GetRoleModuleAuth(string roleId);

        /// <summary>
        /// 保存角色模块和模块权限信息
        /// </summary>
        /// <param name="dto"></param>
        void SaveRoleModuleAuth(Dtos.SaveRoleModuleAuthInputDTO dto);
    }
}
