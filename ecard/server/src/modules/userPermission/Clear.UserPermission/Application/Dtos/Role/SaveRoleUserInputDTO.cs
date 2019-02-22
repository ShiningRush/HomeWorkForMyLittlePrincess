using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Application.Dtos
{
    /// <summary>
    /// 保存角色用户信息
    /// </summary>
    public class SaveRoleUserInputDTO
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleID { get; set; }
        /// <summary>
        /// 角色用户id信息
        /// </summary>
        public string[] UserIds { get; set; }
    }
}
