using PlatformService.BridgeComponent.Domain.Sequence.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Domain.Sequence
{
    /// <summary>
    /// 序列生成器(用于生产流水号)
    /// </summary>
    public interface ISequenceGenerator
    {
        string Next(string sequenceName, SequenceAlgorithm sequenceAlgorithm);

        string Next(string sequenceName);

    }
}
