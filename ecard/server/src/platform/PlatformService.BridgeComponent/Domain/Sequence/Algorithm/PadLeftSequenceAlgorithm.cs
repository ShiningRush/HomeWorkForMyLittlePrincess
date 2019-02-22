using PlatformService.BridgeComponent.Domain.Sequence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Domain.Sequence.Algorithm
{
    /// <summary>
    /// 序号左侧补全算法
    /// </summary>
    public class PadLeftSequenceAlgorithm : SequenceAlgorithm, IAlgorithmNeedStorage
    {
        public PadLeftSequenceAlgorithm(int sequenceDigit = 9)
        {
            SequenceDigit = sequenceDigit;
        }

        public int SequenceDigit { get; private set; }

        protected override string GetSequenceNo(Sequence sequence)
        {
            return $"{sequence.SequenceName}{sequence.SerialNumber.ToString().PadLeft(SequenceDigit, '0')}";
        }
    }
}
