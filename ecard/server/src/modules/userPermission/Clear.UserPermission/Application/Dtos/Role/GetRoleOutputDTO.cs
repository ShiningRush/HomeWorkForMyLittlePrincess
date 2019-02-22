using Abp.AutoMapper;
using System;

namespace Clear.UserPermission.Application.Dtos
{
    [AutoMap(typeof(Entities.Role))]
    public class GetRoleOutputDTO : RoleDTO
    {
        /// <summary>
        /// 主键
        /// </summary> 
        public virtual string Id { get; set; }
        /// <summary>
        /// 创建人
        /// </summary> 
        public virtual string Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary> 
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 最后修改人
        /// </summary> 
        public virtual string LastModifier { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary> 
        public virtual DateTime? LastModifyTime { get; set; }

        public string OrganizeName { get; set; }

        /// <summary>
        /// 角色用户列表
        /// </summary>
        ///public virtual ICollection<User> Users { get; set; }
    }
}
