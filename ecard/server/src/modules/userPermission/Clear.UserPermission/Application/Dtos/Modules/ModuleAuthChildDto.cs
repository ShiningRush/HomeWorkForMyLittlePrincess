using Abp.AutoMapper;
using Clear.UserPermission.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Application.Dtos.Modules
{
    /// <summary>
    /// 模块权限
    /// </summary>
    [AutoMapTo(typeof(ModuleAuth))]
    public class ModuleAuthChildDto
    {
        /// <summary>
        /// 编码
        /// </summary> 
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary> 
        public string Name { get; set; }
        /// <summary>
        /// 名称
        /// </summary> 
        public string WebAPI { get; set; }
    }
}
