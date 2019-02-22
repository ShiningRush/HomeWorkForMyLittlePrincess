using Abp.AutoMapper;
using Clear.UserPermission.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Application.Dtos.Modules
{
    [AutoMapFrom(typeof(Module))]
    public class GetModuleAuthsOutput
    {
        /// <summary>
        /// 主键
        /// </summary> 
        public Guid Id { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary> 
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 编码
        /// </summary> 
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary> 
        public string Name { get; set; }

        /// <summary>
        /// API方法
        /// </summary> 
        public virtual string WebAPI { get; set; }
    }
}
