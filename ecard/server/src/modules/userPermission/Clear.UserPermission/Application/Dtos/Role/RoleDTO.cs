using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Application.Dtos
{
    [AutoMap(typeof(Entities.Role))]
    public class RoleDTO
    {
        /// <summary>
        /// 机构主键
        /// </summary> 
        public virtual Guid OrganizeId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary> 
        public virtual string Name { get; set; }
        /// <summary>
        /// 数据权限 0：本人；1：所在部门；2：所在机构
        /// </summary> 
        public virtual int DataAuthority { get; set; }
        /// <summary>
        /// 排序码
        /// </summary> 
        public virtual int SortCode { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary> 
        public virtual bool IsEnabled { get; set; }
        /// <summary>
        /// 备注
        /// </summary> 
        public virtual string Description { get; set; }
        
       
    }
}
