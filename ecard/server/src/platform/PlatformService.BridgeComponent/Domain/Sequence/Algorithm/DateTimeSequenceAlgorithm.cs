using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Domain.Sequence.Algorithm
{
    public class DateTimeSequenceAlgorithm : SequenceAlgorithm
    {
        protected override string GetSequenceNo(Sequence sequence)
        {
            return $"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}";
        }
    }
}
