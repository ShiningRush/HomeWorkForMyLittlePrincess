using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Application
{
    public class SimpleUserDto : Abp.Application.Services.Dto.EntityDto<Guid>
    {
        /// <summary>
        /// 登录名
        /// </summary> 
        public  string LoginName { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary> 
        public  string UserName { get; set; }
        /// <summary>
        /// 工号
        /// </summary> 
        public  string JobNo { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary> 
        public  string DepartmentName { get; set; }


    }
}
