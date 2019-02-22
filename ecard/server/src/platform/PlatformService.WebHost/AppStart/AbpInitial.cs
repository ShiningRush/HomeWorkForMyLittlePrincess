using Abp;
using Abp.Dependency;
using Castle.Facilities.Logging;
using System;
using Abp.Castle.Logging.Log4Net;
using Abp.PlugIns;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PlatformService.Host.AppStart
{
    public static class AbpInitial
    {
        public static void InitHostAbpSetting(this AbpBootstrapper @this)
        {
            // Abp Log
            //IocManager.Instance.IocContainer.AddFacility<LoggingFacility>(f => f.UseAbpYibanLog(
            //   ServerConfigManager.Instance.GetConfigValue("PrintProxy", "AppName", "核心服务")
            //   , ""
            //   , ServerConfigManager.Instance.GetConfigValue("PrintProxy", "LoggerLevel", "Info").AsTargetType(YiBan.LogClient.Enums.Loglevel.Info)
            //   , ServerConfigManager.Instance.GetConfigValue("LoggerServer", "Host", "127.0.0.1")
            //   , ServerConfigManager.Instance.GetConfigValue("LoggerServer", "Port", "55000").AsTargetType<int>(55000)
            //   , ServerConfigManager.Instance.GetConfigValue("PrintProxy", "UploadInterval", "60").AsTargetType<int>(10)
            //));

            IocManager.Instance.IocContainer.AddFacility<LoggingFacility>(
                f => f.UseAbpLog4Net().WithConfig("log4net.config")
            );

            // 创建插件目录
            var pluginsFolder = AppDomain.CurrentDomain.BaseDirectory + @"Plugins";
            if (!Directory.Exists(pluginsFolder))
            {
                Directory.CreateDirectory(pluginsFolder);
            }

            @this.PlugInSources.AddFolder(pluginsFolder, SearchOption.AllDirectories);
            @this.Initialize();
        }
    }
}
