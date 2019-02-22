using Abp.Application.Services;
using Abp.Dependency;
using PlatformService.BridgeComponent.WebApiConfig;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.Core.AppServices
{
    /// <summary>
    /// 我哦喔喔喔喔Api
    /// </summary>
    public interface ITestAppService : IOpenWebApi
    {
        /// <summary>
        /// 喔喔喔喔喔方法
        /// </summary>
        /// <param name="testParameter">输入参数1</param>
        /// <returns>HelloWorld</returns>
        [DontNeedAuth(SkipAuthType.JustAuthorization)]
        void PostHelloWorld();

        [DontNeedAuth]
        string GetHelloWorld();
    }

    /// <summary>
    /// 实体Api
    /// </summary>
    public class TestAppService : ITestAppService, ITransientDependency
    {
        public string GetHelloWorld()
        {
            var ww = ConfigurationManager.AppSettings["test"];
            return "HelloWorld";
        }

        /// <summary>
        /// 实体方法
        /// </summary>
        /// <returns></returns>
        public void PostHelloWorld()
        {
            
        }
    }
}
