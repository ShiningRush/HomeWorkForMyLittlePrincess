using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Domain.Entities
{

    public class TreeEntity<TPrimaryKey> : Entity<TPrimaryKey>, ITreeEntity<TPrimaryKey> where TPrimaryKey : struct
    {
        public virtual TPrimaryKey? ParentId { get; set; }
    }
}
