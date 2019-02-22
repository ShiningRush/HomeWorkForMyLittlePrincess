using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Application
{
    public class ChangePasswordInput : ICustomValidate
    {
       /// <summary>
       /// 旧密码
       /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// 重复新密码
        /// </summary>
        public string RepeatNewPassword { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {
            if(!NewPassword.Equals(RepeatNewPassword))
            {
                context.Results.Add(new ValidationResult("两次输入的新密码不一致"));
            }
        }
    }
}
