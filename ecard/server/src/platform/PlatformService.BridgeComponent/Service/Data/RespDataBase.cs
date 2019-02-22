using PlatformService.BridgeComponent.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Data
{ 
    public class RespDataBase
    {

        /// <summary>
        /// 状态代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        public RespDataBase()
        {
            this.Code = ((int)HttpResponseCode.Success).ToString();
            var filed = HttpResponseCode.Success.GetType().GetField(HttpResponseCode.Success.ToString());
            this.Message = ((DescriptionAttribute)filed.GetCustomAttributes(typeof(DescriptionAttribute), true).First()).Description;
        }

        public RespDataBase(HttpResponseCode code)
        {
            this.Code = ((int)code).ToString();
            var filed = code.GetType().GetField(code.ToString());
            this.Message = ((DescriptionAttribute)filed.GetCustomAttributes(typeof(DescriptionAttribute), true).First()).Description;
        }

        public RespDataBase(string msg)
        {
            this.Code = ((int)HttpResponseCode.Success).ToString();
            this.Message = msg;
        }

        public RespDataBase(HttpResponseCode code, string msg)
        {
            this.Code = ((int)code).ToString();
            this.Message = msg;
        }

        public RespDataBase(string code, string msg)
        {
            this.Code = code;
            this.Message = msg;
        }
    }
}
