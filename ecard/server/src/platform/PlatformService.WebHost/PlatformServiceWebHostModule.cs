using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection;
using Abp.WebApi;
using PlatformService.BridgeComponent;
using PlatformService.BridgeComponent.EntityFramework;
using PlatformService.BridgeComponent.Enum;
using PlatformService.BridgeComponent.WebApiConfig;
using PlatformService.Core;
using PlatformService.Core.Common.Const;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Timers;
using System.Web;
using System.Web.Http;

namespace PlatformService.WebHost
{
    [DependsOn(
        typeof(BridgeComponentModule),
        typeof(PlatformServiceCoreModule),
        typeof(AbpWebApiModule))]
    public class PlatformServiceWebHostModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpWebApi().HttpConfiguration = new HttpConfiguration();
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            InitDynamicApi();
            InitTaskClearTempFile();
        }
        public override void PostInitialize()
        {

        }

        private void InitDynamicApi()
        {
            var abpAssemblyFinder = IocManager.Resolve<IAssemblyFinder>();

            foreach (var eachFile in abpAssemblyFinder.GetAllAssemblies())
            {
                if (eachFile.GetTypes().Where(p => typeof(IOpenWebApi).IsAssignableFrom(p)).Count() != 0)
                {
                    // 自动生成Api
                    Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                        .ForAll<IOpenWebApi>(eachFile, string.Empty)
                        .ForMethods(builder =>
                        {
                            // 被标记的方法不会创建
                            if (builder.Method.GetCustomAttribute<DisableWebApiAttribute>() != null)
                            {
                                builder.DontCreate = true;
                            }

                            var httpActionAttr = builder.Method.GetCustomAttribute<HttpActionAttribute>();
                            if (httpActionAttr != null)
                            {
                                switch (httpActionAttr.HttpMethod)
                                {
                                    case HttpVerb.Get:
                                        builder.Verb = HttpVerb.Get;
                                        break;
                                    case HttpVerb.Post:
                                        builder.Verb = HttpVerb.Post;
                                        break;
                                    case HttpVerb.Delete:
                                        builder.Verb = HttpVerb.Delete;
                                        break;
                                    case HttpVerb.Put:
                                        builder.Verb = HttpVerb.Put;
                                        break;
                                    default :
                                        builder.Verb = HttpVerb.Get;
                                        break;
                                }
                            }
                        })
                        .WithConventionalVerbs()
                        .Build();
                }
            }
        }

        private void InitTaskClearTempFile()
        {
            var timer = new Timer(PlatformServiceConst.TEMP_FILE_EXPIRE_TIME);
            var tempFilesDir = AppDomain.CurrentDomain.BaseDirectory + PlatformServiceConst.TEMP_FILE_NAME;

            if (!Directory.Exists(tempFilesDir))
            {
                Directory.CreateDirectory(tempFilesDir);
            }

            timer.Elapsed += (sender, e) =>
            {
                var fileDirs = Directory.GetDirectories(tempFilesDir);
                foreach (var fileDir in fileDirs)
                {
                    var timeSpan = DateTime.Now - Directory.GetCreationTime(fileDir);
                    if (timeSpan.TotalMilliseconds > PlatformServiceConst.TEMP_FILE_EXPIRE_TIME)
                    {
                        Directory.Delete(fileDir, true);
                    }
                };
            };
            timer.Start();
        }
    }
}