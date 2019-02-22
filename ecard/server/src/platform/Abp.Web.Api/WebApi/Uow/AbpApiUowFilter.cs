using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.WebApi.Configuration;
using Abp.WebApi.Validation;

namespace Abp.WebApi.Uow
{
    public class AbpApiUowFilter : IActionFilter, ITransientDependency
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAbpWebApiConfiguration _configuration;

        public bool AllowMultiple => false;

        public AbpApiUowFilter(
            IUnitOfWorkManager unitOfWorkManager,
            IAbpWebApiConfiguration configuration
            )
        {
            _unitOfWorkManager = unitOfWorkManager;
            _configuration = configuration;
        }

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var methodInfo = actionContext.ActionDescriptor.GetMethodInfoOrNull();
            if (methodInfo == null)
            {
                return await continuation();
            }

            if (actionContext.ActionDescriptor.IsDynamicAbpAction())
            {
                return await continuation();
            }

            //var unitOfWorkAttr = UnitOfWorkAttribute.GetUnitOfWorkAttributeOrNull(methodInfo) ??
            //                     _configuration.DefaultUnitOfWorkAttribute;

            var unitOfWorkAttr = _configuration.DefaultUnitOfWorkAttribute;

            if (unitOfWorkAttr.IsDisabled)
            {
                return await continuation();
            }

            using (var uow = _unitOfWorkManager.Begin(new UnitOfWorkOptions
            {
                IsTransactional = unitOfWorkAttr.IsTransactional,
                IsolationLevel = unitOfWorkAttr.IsolationLevel,
                Timeout = unitOfWorkAttr.Timeout,
                Scope = unitOfWorkAttr.Scope
            }))
            {
                var result = await continuation();
                await uow.CompleteAsync();
                return result;
            }
        }
    }
}