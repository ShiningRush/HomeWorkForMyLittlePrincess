using Abp.Domain.Entities;
using PlatformService.BridgeComponent.Domain.Sequence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.CommonContext.Domain.SequenceModle
{
    [Table("_sequence")]
    public class SequenceModle : Entity<string>
    {
        protected SequenceModle()
        {

        }

        public SequenceModle(Sequence sequence)
        {
            Id = sequence.SequenceName;
            SerialNumber = sequence.SerialNumber;
            Parameter = sequence.Parameter;
        }

        /// <summary>
        /// 参数
        /// </summary>
        public string Parameter { get;  set; }

        /// <summary>
        /// 序号
        /// </summary>
        public long SerialNumber { get;  set; }


        public static implicit operator Sequence(SequenceModle value)
        {
            if (value == null) return null;
            return new Sequence(value.Id, value.Parameter, value.SerialNumber);
        }

        public static implicit operator SequenceModle(Sequence value)
        {
            if (value == null) return null;
            return new SequenceModle(value);
        }
    }
}
