using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Clear.UserPermission.Entities;

namespace Clear.UserPermission.Application
{
    [AutoMapTo(typeof(User))]
    public class InputCreateUser : ICustomValidate
    {
        /// <summary>
        /// 所属机构
        /// </summary> 
        public virtual Guid OrganizeId { get; set; }
        /// <summary>
        /// 登录名
        /// </summary> 
        [RegularExpression("^[a-zA-Z0-9_]+$", ErrorMessage = "登录名必须是数字或字母！")]
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
        [RegularExpression("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$", ErrorMessage = "邮箱格式不正确！")]
        public virtual string EMail { get; set; }
        /// <summary>
        /// 职级
        /// </summary> 
        public virtual string ProfessionalLevel { get; set; }
        /// <summary>
        /// 职务
        /// </summary> 
        public virtual string Duty { get; set; }
        /// <summary>
        /// 备注
        /// </summary> 
        public virtual string Remark { get; set; }

        /// <summary>
        /// 用户所属部门ID列表
        /// </summary>
        public virtual Guid[] DepartmentIds { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {
            
        }
    }
}
