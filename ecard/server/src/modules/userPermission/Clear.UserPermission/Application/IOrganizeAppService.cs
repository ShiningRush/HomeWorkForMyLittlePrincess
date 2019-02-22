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
    /// 结构管理服务接口
    /// </summary>
    public interface IOrganizeAppService : IOpenWebApi
    {
        /// <summary>
        /// 添加机构
        /// </summary>
        /// <param name="entity">添加实体</param>
        /// <returns>返回新增记录的主键Id</returns>
        string AddOrganize(Dtos.OrganizeDTO entity);

        /// <summary>
        /// 更新机构信息
        /// </summary>
        /// <param name="entity">更新实体</param>
        /// <returns></returns>
        void UpdateOrganize(Dtos.UpdateOrganizeInputDTO entity);
        /// <summary>
        /// 删除机构信息
        /// </summary>
        /// <param name="ids">删除id列表</param>
        /// <returns></returns>
        void DeleteOrganize(List<string> ids);
        /// <summary>
        /// 查询机构信息
        /// </summary>
        /// <param name="inputDto">查询实体</param>
        /// <returns></returns>
        PagerResult<Dtos.GetOrganizeOutputDTO> GetOrganize(Dtos.GetOrganizeInputDTO inputDto);

        /// <summary>
        /// 获取当前登录用户的机构信息
        /// </summary>
        /// <returns></returns>
        [DontNeedAuth]
        List<Dtos.GetUserOrganizeOutputDTO> GetCurrentUserOrganizes();

    }
}
