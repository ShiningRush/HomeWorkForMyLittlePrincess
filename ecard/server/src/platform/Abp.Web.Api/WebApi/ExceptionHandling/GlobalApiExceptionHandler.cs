using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Net.Http;
using System.Net;
using Abp.WebApi.Configuration;
using Abp.Events.Bus;
using Abp.Events.Bus.Exceptions;
using Castle.Core.Logging;
using PlatformService.BridgeComponent.CustomException;
using PlatformService.BridgeComponent.Data;

namespace Yiban.CoreService.Web.Api.WebApi.ExceptionHandling
{
    /// <summary>
    /// *King
    /// </summary>
    public class GlobalApiExceptionHandler : ExceptionHandler , ITransientDependency
    {
        private readonly IEventBus _eventBus;
        public ILogger Logger { get; set; }

        public GlobalApiExceptionHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;

            // Do not write logs if no Logger supplied.
            Logger = NullLogger.Instance;
        }

        public override void Handle(ExceptionHandlerContext context)
        {
            Logger.Error(context.Exception.ToString(), context.Exception);

            var aggregateException = context.Exception as AggregateException;
            if (aggregateException != null)
            {
                foreach (var eachException in aggregateException.InnerExceptions)
                {
                    context.Result = new TextPlainErrorResult(CustomHttpException.GetHttpErrorFromEx(eachException));
                }
            }
            else
            {
                base.Handle(context);
            }

            _eventBus.Trigger(this, new AbpHandledExceptionData(context.Exception));
        }

        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            return base.HandleAsync(context, cancellationToken);
        }
    }

    public class TextPlainErrorResult : IHttpActionResult
    {
        private readonly IAbpWebApiConfiguration _configuration;
        public CustomHttpException HandleException { get; private set; }

        public TextPlainErrorResult(CustomHttpException exception)
        {
            _configuration = IocManager.Instance.Resolve<IAbpWebApiConfiguration>();
            this.HandleException = exception;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var respMsg = new HttpResponseMessage(HttpStatusCode.OK);
            respMsg.Content = new ObjectContent<RespDataBase>(
                    this.HandleException.GetRespData(),
                    _configuration.HttpConfiguration.Formatters.JsonFormatter
                    );
            return Task.FromResult(respMsg);
        }
    }
}
