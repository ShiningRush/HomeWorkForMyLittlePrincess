using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Application.Dtos
{
    /// <summary>
    /// 获取角色授权的模块或按钮权限
    /// </summary>
    public class GetRoleModuleAuthOutputDTO
    {
        /// <summary>
        /// 角色模块id信息
        /// </summary>
        public string[] ModuleIds { get; set; }
        /// <summary>
        /// 角色模块权限id信息
        /// </summary>
        public string[] ModuleAuthIds { get; set; }
    }
}
