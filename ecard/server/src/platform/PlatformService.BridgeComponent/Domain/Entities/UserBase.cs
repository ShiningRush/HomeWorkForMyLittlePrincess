using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Domain.Entities
{
    public class UserBase : Entity<Guid>, IMustHaveOrganize
    {
        public const string SYSTEM_USERID = "dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9";
        public UserBase()
        {
           
        }

        /// <summary>
        /// 所属机构
        /// </summary> 
        public virtual Guid OrganizeId { get; set; }
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
