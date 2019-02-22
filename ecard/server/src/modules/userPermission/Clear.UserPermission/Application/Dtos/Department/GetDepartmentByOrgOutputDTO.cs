using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Application.Dtos
{
    /// <summary>
    /// 通过机构id查询部门信息DTO
    /// </summary>
    public class GetDepartmentByOrgOutputDTO
    {
        /// <summary>
        /// 主键
        /// </summary> 
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 机构主键
        /// </summary> 
        public virtual Guid OrganizeId { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary> 
        public virtual Guid? ParentId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary> 
        public virtual string Name { get; set; }
    }
}
