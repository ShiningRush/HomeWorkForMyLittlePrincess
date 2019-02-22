using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiBan.Common.Pages;

namespace Clear.UserPermission.Application.Dtos
{
    public class GetRoleInputDTO : PagerParameter
    {
        /// <summary>
        /// 角色名称
        /// </summary> 
        public string Name { get; set; }
        /// <summary>
        /// 所属机构ID
        /// </summary> 
        public string OrganizeId { get; set; }
        /// <summary>
        /// 所属机构名称
        /// </summary> 
        public string OrganizeName { get; set; }
    }
}
