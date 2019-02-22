using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Domain.Entities
{
    public interface IMustHaveCreator : IHasCreatorTime
    {
        Guid Creator { get; set; }
    }


    public interface IMayHaveCreator : IHasCreatorTime
    {
        Guid? Creator { get; set; }
    }

    public interface IHasCreatorTime
    {
        
        DateTime CreateTime { get; set; }
    }
}
