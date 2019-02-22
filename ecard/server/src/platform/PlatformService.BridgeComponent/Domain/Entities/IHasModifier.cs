using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Domain.Entities
{
    public interface IHasModifier: IHasModifierTime
    {
        Guid? LastModifier { get; set; }
    }

 

    public interface IHasModifierTime
    {
        DateTime? LastModifyTime { get; set; }
    }
}
