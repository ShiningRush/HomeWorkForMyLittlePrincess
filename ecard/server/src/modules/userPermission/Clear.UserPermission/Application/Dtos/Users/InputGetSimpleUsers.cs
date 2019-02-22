using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Application
{
    public class InputGetSimpleUsers
    {
        /// <summary>
        /// 所属机构
        /// </summary>
        public virtual Guid? OrganizeId { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public virtual Guid? DepartmentId { get; set; }
        /// <summary>
        /// 登录名
        /// </summary> 
        public virtual string LoginName { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary> 
        public virtual string UserName { get; set; }
       
    }
}
