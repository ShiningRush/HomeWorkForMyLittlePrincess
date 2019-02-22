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
    /// 部门管理服务接口
    /// </summary>
    public interface IDepartmentAppService: IOpenWebApi
    {
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="entity">添加实体</param>
        /// <returns>返回新增记录的主键Id</returns>
        string AddDepartment(Dtos.DepartmentDTO entity);

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="entity">更新实体</param>
        /// <returns></returns>
        void UpdateDepartment(Dtos.UpdateDepartmentInputDTO entity);
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="ids">删除id列表</param>
        /// <returns></returns>
        void DeleteDepartment(List<string> ids);
        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="inputDto">查询实体</param>
        /// <returns></returns>
        PagerResult<Dtos.GetDepartmentOutputDTO> GetDepartment(Dtos.GetDepartmentInputDTO inputDto);


        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="organizeId">查询实体</param>
        /// <returns></returns>
        [DontNeedAuth]
        List<Dtos.GetDepartmentByOrgOutputDTO> GetDepartmentByOrganizeId(Guid organizeId);
    }
}
