using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.WebApiConfig
{
    public class HttpFileOutput
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public bool IsTempFile { get; set; }

        public HttpFileOutput()
        {
            IsTempFile = true;
        }
    }
}
