using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Domain.Sequence
{
    public interface ISequenceRepository
    {
        Sequence FindOrCreate(string sequenceName);

        void Save(Sequence sequence);


    }
}
