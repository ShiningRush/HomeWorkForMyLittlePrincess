using Abp.Dependency;
using Castle.Core.Logging;
using Microsoft.Owin;
using PlatformService.Core.Common.Const;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformService.Host.HttpPreHandle
{
    public class HttpContextPreHandler : OwinMiddleware
    {
        public ILogger Logger { get; set; }

        public HttpContextPreHandler( OwinMiddleware next) : base(next)
        {
            Logger = IocManager.Instance.Resolve<ILogger>();
        }

        private void CreateServiceContext(IOwinRequest request)
        {
            var copyHeader = new Dictionary<string, IEnumerable<string>>();

            var stringBuilder = new StringBuilder();
            foreach (var eachHeader in request.Headers)
            {
                copyHeader.Add(eachHeader.Key, eachHeader.Value);
                stringBuilder.AppendFormat(" Key: {0}, Value: {1}", eachHeader.Key, string.Join(";", eachHeader.Value));
                stringBuilder.AppendLine();
            }

            copyHeader.Add(HttpContextConst.HTTP_CLIENT_IP, new List<string>() { request.RemoteIpAddress });
            if (request.Context.Authentication.User != null)
            {
                copyHeader.Add(HttpContextConst.HTTP_CLIENT_ID, new List<string>() { request.User.Identity.Name });
            }

            if (((ClaimsPrincipal)request.User).Claims.Any(p => p.Type.Equals("password") || p.Type.Equals("ssoLogin")))
                copyHeader.Add("grant_type", new string[] { "password" });
            else
                copyHeader.Add("grant_type", new string[] { "client" });

            Logger.DebugFormat("接受到来自 {0} 的请求，请求头如下:\r\n{1}", request.RemoteIpAddress, stringBuilder.ToString());
            CallContext.LogicalSetData(HttpContextConst.HTTP_REQUEST_CONTEXT, copyHeader);
        }

        private void LogAccessInfo(IOwinRequest request)
        {
            var requestContent = "请求内容大于85000个字节,防止内存泄漏，不显示该消息";
            string[] contentLength;
            if (request.Method.Equals("get", StringComparison.OrdinalIgnoreCase))
            {
                requestContent = "Query:{0}";
                requestContent = string.Format(requestContent, 
                    string.Join(",",request.Query.Select(p => string.Format("{0} : {1}", p.Key, p.Value))));
            }

            if (request.Headers.TryGetValue("Content-Length", out contentLength) && 
                Convert.ToInt64(contentLength.First()) < 85000) {
                var ms = new MemoryStream();
                request.Body.CopyTo(ms);
                ms.Position = 0L;
                var buffer = new byte[ms.Length];
                ms.Read(buffer, 0, buffer.Length);
                requestContent = Encoding.UTF8.GetString(buffer);

                if (request.Body != null && !request.Body.CanSeek)
                {
                    ms.Position = 0L;
                    request.Body.Dispose();
                    request.Body = ms;
                }
                else
                {
                    request.Body.Position = 0L;
                }
            }

            Logger.DebugFormat("核心服务开始处理来自 {0} 的 {1} 请求，请求Url: {3}，请求格式: {4}，请求内容: {5}。",
                request.RemoteIpAddress,
                request.Method,
                Thread.CurrentThread.ManagedThreadId,
                request.Uri.PathAndQuery,
                request.ContentType,
                requestContent
                );
        }

        public override Task Invoke(IOwinContext context)
        {
            try
            {
                this.CreateServiceContext(context.Request);
                this.LogAccessInfo(context.Request);

                var watch = new Stopwatch();
                watch.Start();
                // 跨域允许
                context.Response.Headers.Add("Access-Control-Allow-Origin", new string[] { "*" });
                if (context.Request.Method.Equals("options", StringComparison.OrdinalIgnoreCase))
                {
                    context.Response.StatusCode = 200;
                    context.Response.Headers.Add("Access-Control-Allow-Methods", new string[] { "GET,POST" });
                    context.Response.Headers.Add("Access-Control-Allow-Headers", new string[] { "Authorization,Content-Type" });
                    return Task.FromResult(0);
                }

                return Next.Invoke(context).ContinueWith(task=> {
                    watch.Stop();
                    if (watch.ElapsedMilliseconds > 1000)
                    {
                        Logger.InfoFormat("来自: {0} 的请求处理完成，请求Url: {1}，所花时间: {2}",
                            context.Request.RemoteIpAddress,
                            context.Request.Uri,
                            watch.ElapsedMilliseconds);
                    }
                    else
                    {
                        Logger.DebugFormat("来自: {0} 的请求处理完成，请求Url: {1}，所花时间: {2}",
                            context.Request.RemoteIpAddress,
                            context.Request.Uri,
                            watch.ElapsedMilliseconds);
                    }
                });
            }
            catch(Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                Logger.Error("核心服务发生了意料外的错误，请查看日志。", ex );
                return null;
            }
        }
    }
}
