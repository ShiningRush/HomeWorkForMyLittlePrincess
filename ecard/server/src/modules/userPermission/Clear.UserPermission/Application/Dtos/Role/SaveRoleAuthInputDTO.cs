using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Runtime.Validation;

namespace Clear.UserPermission.Application.Dtos
{
    /// <summary>
    /// 保存角色模块权限信息
    /// </summary>
    public class SaveRoleModuleAuthInputDTO:ICustomValidate
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleID { get; set; }

        /// <summary>
        /// 角色模块id信息
        /// </summary>
        public string[] ModuleIds { get; set; }

        /// <summary>
        /// 角色模块权限id信息
        /// </summary>
        public string[] ModuleAuthIds { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {
            if (ModuleIds == null || ModuleIds.Length == 0)
            {
                context.Results.Add(new System.ComponentModel.DataAnnotations.ValidationResult("至少需要包含一个系统功能"));
            }
            if (ModuleAuthIds == null || ModuleAuthIds.Length == 0)
            {
                context.Results.Add(new System.ComponentModel.DataAnnotations.ValidationResult("至少需要包含一个系统按钮"));
            }
        }
    }
}
