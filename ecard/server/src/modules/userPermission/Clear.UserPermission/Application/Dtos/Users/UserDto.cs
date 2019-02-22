using Abp.AutoMapper;
using Clear.UserPermission.Entities;
using PlatformService.BridgeComponent.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Application
{
    [AutoMapFrom(typeof(User))]
    public class UserDto:Abp.Application.Services.Dto.EntityDto<Guid>
    {
        /// <summary>
        /// 所属机构
        /// </summary> 
        public virtual Guid OrganizeId { get; set; }
        /// <summary>
        /// 所属机构名称
        /// </summary> 
        public virtual string OrganizeName { get; set; }
        /// <summary>
        /// 登录名
        /// </summary> 
        public virtual string LoginName { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary> 
        public virtual string UserName { get; set; }
        /// <summary>
        /// 工号
        /// </summary> 
        public virtual string JobNo { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary> 
        public virtual string Mobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary> 
        public virtual string EMail { get; set; }
        /// <summary>
        /// 职级代码
        /// </summary> 
        [DataItem("ProfessionalLevel")]
        public virtual string ProfessionalLevel { get; set; }

        /// <summary>
        /// 职级名称
        /// </summary>
        public virtual string ProfessionalLevelName { get; set; }
        /// <summary>
        /// 职务代码
        /// </summary> 
        [DataItem("UserDuty")]
        public virtual string Duty { get; set; }
        /// <summary>
        /// 职务名称
        /// </summary> 
        public virtual string DutyName { get; set; }
        /// <summary>
        /// 备注
        /// </summary> 
        public virtual string Remark { get; set; }
        /// <summary>
        /// 是否停用
        /// </summary> 
        public virtual bool IsStop { get; set; }
        /// <summary>
        /// 是否重置密码
        /// </summary> 
        public virtual bool IsReset { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public Dictionary<Guid, string> Departments { get; set; }

        /// <summary>
        /// 所属角色
        /// </summary>
        public Dictionary<Guid, string> Roles { get; set; }
    }
}
