using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiBan.Common.Pages;

namespace Clear.UserPermission.Application.Dtos
{
    /// <summary>
    /// 获取部门信息DTO
    /// </summary>
    public class GetDepartmentInputDTO : PagerParameter
    {
        /// <summary>
        /// 所属组织机构Id
        /// </summary>
        public Guid? OrganizeId { get; set; }
        /// <summary>
        /// 上级部门Id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 管理者名称
        /// </summary>
        public string Manager { get; set; }
    }
}
