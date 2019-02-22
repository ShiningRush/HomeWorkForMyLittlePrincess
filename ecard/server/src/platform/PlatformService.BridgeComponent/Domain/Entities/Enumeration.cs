using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Domain.Entities
{
    public abstract class Enumeration
    {
        /// <summary>
        /// 
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// 类别
        /// </summary>
        public abstract string Category { get; }

        protected Enumeration()
        {
        }

        protected Enumeration(string key,string value)
        {
            Key = key;
            Value = value;
        }
    }
}
