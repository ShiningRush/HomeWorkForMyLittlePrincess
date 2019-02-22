using Abp.WebApi.Controllers;
using MySql.Data.MySqlClient;
using PlatformService.BridgeComponent.CustomException;
using PlatformService.BridgeComponent.EntityFramework;
using PlatformService.BridgeComponent.WebApiConfig;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using System.Xml;

namespace PlatformService.WebHost.ApiControllers
{
    [DontNeedAuth]
    public class SystemInitController : AbpApiController
    {
        private readonly DbMigratorManager _dbMigratorManager;

        private string _DbPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db.config");

        /// <summary>
        /// Contructor
        /// </summary>
        public SystemInitController(DbMigratorManager dbMigratorManager)
        {
            _dbMigratorManager = dbMigratorManager;
        }

        /// <summary>
        /// 初始化系统
        /// </summary>
        /// <param name="dbAddress">数据库地址</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="dbPort">数据库端口</param>
        /// <param name="userId">数据库用户</param>
        /// <param name="password">密码</param>
        public object InitialSystem(string dbAddress, string dbName, string dbPort,string userId,string password)
        {
            var resultCode = "0";
            if (!File.Exists(_DbPath))
            {
                var defaultConn = "server={0};port={2};user id={3};password={4};persistsecurityinfo=True;database={1};Allow User Variables = True";
                defaultConn = string.Format(defaultConn, dbAddress, dbName, dbPort, userId, password);
                if (this.ValidateDbConnString(defaultConn))
                    ChangeConfiguration("Default", defaultConn);
            }

            if (_dbMigratorManager.IsNeedMigrator())
            {
                try
                {
                    _dbMigratorManager.Initialize();
                }
                catch (Exception)
                {
                    if (File.Exists(_DbPath))
                    {
                        File.Delete(_DbPath);
                    }

                    resultCode = "1";
                }
            }

            return new { code= resultCode };
        }

        private bool ValidateDbConnString(string dbConnString)
        {
            using (var mySqlConnection = new MySqlConnection(dbConnString))
            {
                try
                {
                    mySqlConnection.Open();
                }
                catch (Exception ex)
                {
                    var innerEx = ex.InnerException;
                    if (innerEx != null && innerEx.Message.Contains("Unknown database"))
                        return true;

                    throw new CustomHttpException("无法访问数据库, 请重新确认数据库配置参数是否有误");
                }
                return true;
            }
        }

        /// <summary>
        /// 是否需要初始化系统
        /// </summary>
        /// <returns></returns>
        public object GetDbStatus()
        {
            var isInited = File.Exists(_DbPath);
            var isLatest = false;
            if (isInited)
            {
                isLatest = !_dbMigratorManager.IsNeedMigrator();
            }

            return new
            {
                code = "0", data = new { isInited = isInited, isLatest = isLatest }
            };
        }

        private string ChangeConfiguration(string key, string defaultValue)
        {
            if (!File.Exists(_DbPath)) {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml($"<connectionStrings><add name=\"Default\" connectionString=\"{defaultValue}\" providerName=\"MySql.Data.MySqlClient\" /></connectionStrings> ");
                xmldoc.Save(_DbPath);
            }
            return defaultValue;
        }
    }
}
