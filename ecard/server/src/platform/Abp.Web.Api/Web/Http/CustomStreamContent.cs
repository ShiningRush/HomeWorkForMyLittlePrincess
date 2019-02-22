using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Yiban.CoreService.Web.Api.Web.Http
{
    public class CustomStreamContent : StreamContent
    {
        private Action _postAction = null;

        public CustomStreamContent(Stream content) : base(content)
        {
            
        }


        public CustomStreamContent(Stream content, int bufferSize, Action completeAction = null) : base(content, bufferSize)
        {
            _postAction = completeAction;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _postAction?.Invoke();
        }
    }
}
