using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Domain.Sequence.Algorithm
{
    /// <summary>
    /// 生成序列算法
    /// </summary>
    public abstract class SequenceAlgorithm
    {

        /// <summary>
        /// 生成序列号
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        protected abstract string GetSequenceNo(Sequence sequence);

        /// <summary>
        /// 生成序列号
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public string NextSequenceNo(Sequence sequence)
        {
            sequence.NextSerialNumber();
            return GetSequenceNo(sequence);
        }

    }
}
