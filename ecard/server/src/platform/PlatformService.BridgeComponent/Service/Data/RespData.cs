using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Data
{
    public class RespData<T> : RespDataBase
    {
        /// <summary>
        /// 数据载体
        /// </summary>
        public T Data { get; set; }

        public RespData():base()
        {

        }

        public RespData(T obj) : base()
        {
            this.Data = obj;
        }
    }

    public class RespData : RespData<object>
    {
        public RespData() : base()
        {
        }

        public RespData(object obj) : base(obj)
        {
        }
    }
}
