using Abp.AutoMapper;
using Clear.CommonContext.Domain.DataItemAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.CommonContext.AppService.Dtos
{
    [AutoMap(typeof(DataitemDetail))]
    public class GetDataItemDetailOutput : DataItemDetailDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 创建人
        /// </summary> 
        public virtual string Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary> 
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 最后修改人
        /// </summary> 
        public virtual string LastModifier { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary> 
        public virtual DateTime LastModifyTime { get; set; }
    }
}
