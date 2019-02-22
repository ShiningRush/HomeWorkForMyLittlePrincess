using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Domain.Entities
{

    /// <summary>
    /// 树形实体
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface ITreeEntity<TPrimaryKey>: IEntity<TPrimaryKey> where TPrimaryKey : struct
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        TPrimaryKey? ParentId { get; set; }
    }
}
