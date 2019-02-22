using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Domain.Entities
{
    public class OrganizeBase : TreeEntity<Guid>
    {
       
        public OrganizeBase()
        {
        }
        /// <summary>
        /// 机构代码
        /// </summary> 
        public virtual string Code { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary> 
        public virtual string Name { get; set; }

    }
}
