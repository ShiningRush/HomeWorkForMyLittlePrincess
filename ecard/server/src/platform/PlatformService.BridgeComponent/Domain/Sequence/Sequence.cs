using Abp.Domain.Entities;
using PlatformService.BridgeComponent.Domain.Sequence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Domain.Sequence
{
    public class Sequence 
    {
        protected Sequence()
        {

        }

        public Sequence(string sequenceName,string parameter = null,long serialNumber = 0)
        {
            SequenceName = sequenceName;
            Parameter = parameter;
            SerialNumber = serialNumber; 
        }

        public string SequenceName { get;private set; }

        /// <summary>
        /// 算法
        /// </summary>
        public string Parameter { get; private set; }

        /// <summary>
        /// 序号
        /// </summary>
        public long SerialNumber { get; private set; }

        public long NextSerialNumber()
        {
            return SerialNumber++;
        }
    }
}
