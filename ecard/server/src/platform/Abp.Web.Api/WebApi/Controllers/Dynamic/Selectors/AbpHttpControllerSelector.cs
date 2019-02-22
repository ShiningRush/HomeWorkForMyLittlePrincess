using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Abp.Collections.Extensions;
using Abp.WebApi.Controllers.Dynamic.Builders;
using System;
using PlatformService.BridgeComponent.CustomException;

namespace Abp.WebApi.Controllers.Dynamic.Selectors
{
    /// <summary>
    /// This class is used to extend default controller selector to add dynamic api controller creation feature of Abp.
    /// It checks if requested controller is a dynamic api controller, if it is,
    /// returns <see cref="HttpControllerDescriptor"/> to ASP.NET system.
    /// </summary>
    public class AbpHttpControllerSelector : DefaultHttpControllerSelector
    {
        private readonly HttpConfiguration _configuration;
        private readonly DynamicApiControllerManager _dynamicApiControllerManager;

        /// <summary>
        /// Creates a new <see cref="AbpHttpControllerSelector"/> object.
        /// </summary>
        /// <param name="configuration">Http configuration</param>
        /// <param name="dynamicApiControllerManager"></param>
        public AbpHttpControllerSelector(HttpConfiguration configuration, DynamicApiControllerManager dynamicApiControllerManager)
            : base(configuration)
        {
            _configuration = configuration;
            _dynamicApiControllerManager = dynamicApiControllerManager;
        }

        /// <summary>
        /// This method is called by Web API system to select the controller for this request.
        /// </summary>
        /// <param name="request">Request object</param>
        /// <returns>The controller to be used</returns>
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            //Get request and route data
            if (request == null)
            {
                return base.SelectController(null);
            }

            var routeData = request.GetRouteData();
            if (routeData == null)
            {
                return base.SelectController(request);
            }

            // TestMock
            //var ss = request.Content.ReadAsStringAsync().Result;
            //if (ss.Length == 0)
            //{
            //    return null;
            //}

            //Get serviceNameWithAction from route
            string serviceNameWithAction;
            // *King
            object tempObj;
            if (!routeData.Values.TryGetValue("serviceNameWithAction", out tempObj))
            {
                return this.DefaultAction(request);
            }
            serviceNameWithAction = (string)tempObj;

            //Normalize serviceNameWithAction
            if (serviceNameWithAction.EndsWith("/"))
            {
                serviceNameWithAction = serviceNameWithAction.Substring(0, serviceNameWithAction.Length - 1);
                routeData.Values["serviceNameWithAction"] = serviceNameWithAction;
            }

            //Get the dynamic controller
            var hasActionName = false;
            var controllerInfo = _dynamicApiControllerManager.FindOrNull(serviceNameWithAction);
            if (controllerInfo == null)
            {
                if (!DynamicApiServiceNameHelper.IsValidServiceNameWithAction(serviceNameWithAction))
                {
                    return this.DefaultAction(request);
                }
                
                var serviceName = DynamicApiServiceNameHelper.GetServiceNameInServiceNameWithAction(serviceNameWithAction);
                controllerInfo = _dynamicApiControllerManager.FindOrNull(serviceName);
                if (controllerInfo == null)
                {
                    return this.DefaultAction(request);                    
                }

                hasActionName = true;
            }
            
            //Create the controller descriptor
            var controllerDescriptor = new DynamicHttpControllerDescriptor(_configuration, controllerInfo);
            controllerDescriptor.Properties["__AbpDynamicApiControllerInfo"] = controllerInfo;
            controllerDescriptor.Properties["__AbpDynamicApiHasActionName"] = hasActionName;

            return controllerDescriptor;
        }

        // *King
        private HttpControllerDescriptor DefaultAction(HttpRequestMessage request)
        {
            try
            {
                return base.SelectController(request);
            }
            catch (Exception ex)
            {
                throw new CustomHttpException("在默认的Webapi选择器中发生了未处理的错误。", ex);
            }
        }
    }
}