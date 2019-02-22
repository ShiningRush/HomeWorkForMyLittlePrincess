using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Extensions;
using Abp.Web.Models;
using Abp.WebApi.Configuration;
using System.IO;
using Castle.Core.Logging;
using Yiban.CoreService.Web.Api.Web.Http;
using PlatformService.BridgeComponent.Data;
using PlatformService.BridgeComponent.WebApiConfig;

namespace Abp.WebApi.Controllers
{
    /// <summary>
    /// Wraps Web API return values by <see cref="RespData"/>.
    /// </summary>
    public class ResultWrapperHandler : DelegatingHandler, ITransientDependency
    {
        private readonly IAbpWebApiConfiguration _configuration;
        public ILogger Logger { get; set; }

        public ResultWrapperHandler(IAbpWebApiConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken).ContinueWith(
                task =>
                {
                    WrapResultIfNeeded(request, task.Result);
                    return task.Result;

                }, cancellationToken);
        }

        protected virtual void WrapResultIfNeeded(HttpRequestMessage request, HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                return;
            }

            if (_configuration.SetNoCacheForAllResponses)
            {
                //Based on http://stackoverflow.com/questions/49547/making-sure-a-web-page-is-not-cached-across-all-browsers
                response.Headers.CacheControl = new CacheControlHeaderValue
                {
                    NoCache = true,
                    NoStore = true,
                    MaxAge = TimeSpan.Zero,
                    MustRevalidate = true
                };
            }

            var wrapAttr = HttpActionDescriptorHelper.GetWrapResultAttributeOrNull(request.GetActionDescriptor())
                           ?? _configuration.DefaultWrapResultAttribute;

            if (!wrapAttr.WrapOnSuccess)
            {
                return;
            }

            if (IsIgnoredUrl(request.RequestUri))
            {
                return;
            }

            // *King
            object resultObject;
            if (!response.TryGetContentValue(out resultObject) || resultObject == null)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new ObjectContent<RespData>(
                    new RespData(),
                    _configuration.HttpConfiguration.Formatters.JsonFormatter
                    );
                return;
            }

            if (resultObject is RespDataBase)
            {
                return;
            }

            // *King
            // Check returns type
            if (resultObject is HttpFileOutput) {
                var fileOutput = (HttpFileOutput)resultObject;
                if (!File.Exists(fileOutput.FilePath))
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Content = new StringContent("文件不存在，请重新确认文件Url");
                    return;
                }
                if (fileOutput.FileName.IsNullOrEmpty())
                    fileOutput.FileName = Path.GetFileName(fileOutput.FilePath);

                var fileInfo = File.Open(fileOutput.FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                response.StatusCode = HttpStatusCode.OK;
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content = new CustomStreamContent(fileInfo, 1024 * 1024, () => {
                    if (fileOutput.IsTempFile)
                    {
                        File.Delete(fileOutput.FilePath);
                    }
                });
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = fileOutput.FileName;
                Logger.DebugFormat("开始传输，文件路径{0}", fileOutput.FilePath);

                return;
            }

            response.Content = new ObjectContent<RespData>(
                new RespData(resultObject),
                _configuration.HttpConfiguration.Formatters.JsonFormatter
                );
        }

        private bool IsIgnoredUrl(Uri uri)
        {
            if (uri == null || uri.AbsolutePath.IsNullOrEmpty())
            {
                return false;
            }

            return _configuration.ResultWrappingIgnoreUrls.Any(url => uri.AbsolutePath.StartsWith(url));
        }
    }
}