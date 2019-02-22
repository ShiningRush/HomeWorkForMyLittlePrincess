using PlatformService.BridgeComponent.Data;
using PlatformService.BridgeComponent.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.CustomException
{
    public class CustomHttpException : Exception
    {
        private HttpResponseCode _code = HttpResponseCode.Error;
        private string _customCode;

        public CustomHttpException() : base(string.Empty)
        {
        }

        public CustomHttpException(string errMsg) : base(errMsg) { }
        public CustomHttpException(string errMsg, Exception ex) : base(errMsg, ex) { }

        [Obsolete("HttpCode中已定义了错误消息，请尽量避免使用该方法。")]
        public CustomHttpException(HttpResponseCode code, string errMsg) : base(errMsg)
        {
            _code = code;
        }

        public CustomHttpException(HttpResponseCode code) : this(code, string.Empty)
        {
        }

        public CustomHttpException(string customCode, string errMsg) : base(errMsg)
        {
            _customCode = customCode;
        }


        public virtual RespDataBase GetRespData()
        {
            if (!string.IsNullOrEmpty(this._customCode))
            {
                return new RespDataBase(_customCode, this.Message);
            }

            if (!string.IsNullOrEmpty(base.Message))
            {
                return new RespDataBase(_code, this.Message);
            }

            return new RespDataBase(_code);
        }

        public static CustomHttpException GetHttpErrorFromEx( Exception ex)
        {
            var defaultError = new CustomHttpException();

            if (ex is CustomHttpException)
            {
                defaultError =  ex as CustomHttpException;
            }

            if (!(ex is CustomHttpException) && ex.InnerException != null)
            {
                defaultError = GetHttpErrorFromEx(ex.InnerException);
            }

            return defaultError;
        }
    }
}
