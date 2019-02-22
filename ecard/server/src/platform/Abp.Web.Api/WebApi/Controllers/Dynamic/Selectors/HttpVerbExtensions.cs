using System;
using System.Net.Http;
using Abp.Web;
using PlatformService.BridgeComponent.Enum;

namespace Abp.WebApi.Controllers.Dynamic.Selectors
{
    /// <summary>
    /// Extension methods for <see cref="HttpVerb"/>.
    /// </summary>
    public static class HttpVerbExtensions
    {
        public static readonly HttpMethod HttpPatch = new HttpMethod("PATCH");

        public static HttpMethod ToHttpMethod(this HttpVerb verb)
        {
            switch (verb)
            {
                case HttpVerb.Get:
                    return HttpMethod.Get;
                case HttpVerb.Post:
                    return HttpMethod.Post;
                case HttpVerb.Put:
                    return HttpMethod.Put;
                case HttpVerb.Delete:
                    return HttpMethod.Delete;
                case HttpVerb.Options:
                    return HttpMethod.Options;
                case HttpVerb.Trace:
                    return HttpMethod.Trace;
                case HttpVerb.Head:
                    return HttpMethod.Head;
                case HttpVerb.Patch:
                    return HttpPatch;
                default:
                    throw new ArgumentException("Given HttpVerb is unknown: " + verb, nameof(verb));
            }
        }

        public static HttpVerb ToHttpVerb(this HttpMethod method)
        {
            switch (method.Method)
            {
                case "GET":
                    return HttpVerb.Get;
                case "POST":
                    return HttpVerb.Post;
                case "PUT":
                    return HttpVerb.Put;
                case "DELETE":
                    return HttpVerb.Delete;
                case "OPTIONS":
                    return HttpVerb.Options;
                case "TRACE":
                    return HttpVerb.Trace;
                case "HEAD":
                    return HttpVerb.Head;
                case "PATCH":
                    return HttpVerb.Patch;
                default:
                    throw new AbpException("Unknown HTTP METHOD: " + method);
            }
        }
    }
}