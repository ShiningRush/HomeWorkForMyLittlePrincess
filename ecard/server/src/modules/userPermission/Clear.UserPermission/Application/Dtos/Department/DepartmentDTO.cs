using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Application.Dtos
{
    /// <summary>
    /// 部门信息基础DTO
    /// </summary>
    [AutoMap(typeof(Entities.Department))]
    public class DepartmentDTO
    {
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
        /// <summary>
        /// 负责人主键
        /// </summary> 
        public virtual string ManagerId { get; set; }
        /// <summary>
        /// 内线电话
        /// </summary> 
        public virtual string Phone { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary> 
        public virtual string Email { get; set; }
        /// <summary>
        /// 部门传真
        /// </summary> 
        public virtual string Fax { get; set; }
        /// <summary>
        /// 层
        /// </summary> 
        public virtual int Layer { get; set; }
        /// <summary>
        /// 排序码
        /// </summary> 
        public virtual int SortCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary> 
        public virtual string Description { get; set; }
        
    }
}
