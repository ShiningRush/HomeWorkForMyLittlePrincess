using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Application.Dtos
{
    /// <summary>
    /// 查询部门信息输出DTO
    /// </summary>
    [AutoMap(typeof(Entities.Department))]
    public class GetDepartmentOutputDTO :DepartmentDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 创建人
        /// </summary> 
        public string Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary> 
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 最后修改人
        /// </summary> 
        public string LastModifier { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary> 
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 上级部门名称
        /// </summary>
        public string ParentName { get; set; }
        /// <summary>
        /// 部门管理者
        /// </summary>
        public string Manager { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary> 
        public string OrganizeName { get; set; }
    }
}
