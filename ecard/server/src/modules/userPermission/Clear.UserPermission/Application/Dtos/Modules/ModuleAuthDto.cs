using Abp.AutoMapper;
using Clear.UserPermission.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Application
{
    [AutoMap(typeof(ModuleAuth))]
    public class ModuleAuthDto : Abp.Application.Services.Dto.EntityDto<Guid>
    {
        /// <summary>
        /// 功能主键
        /// </summary> 
        public virtual Guid ModuleId { get; set; }
        /// <summary>
        /// 编码
        /// </summary> 
        public virtual string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary> 
        public virtual string Name { get; set; }
    }
}
