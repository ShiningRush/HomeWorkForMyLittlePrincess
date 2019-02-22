using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clear.CommonContext.Domain.DataItemAggregate;
using PlatformService.BridgeComponent.WebApiConfig;
using Abp.Extensions;
using Abp.Collections.Extensions;
using PlatformService.BridgeComponent.CustomException;
using System.IO;
using System.Xml;
using System.Configuration;
using System.Diagnostics;
using PlatformService.Core.Common.Const;
using Abp.Dependency;

namespace Clear.CommonContext.AppService
{
    /// <summary>
    /// 客户端包相关应用服务接口
    /// </summary>
    public class ClientPackAppService : IClientPackAppService, ITransientDependency
    {
        private readonly static string _clientFileDir = "ClientFile";
        private readonly static string _pluginFileName = "plugin.xml";
        private readonly static string _updateFileName = "update.xml";
        private readonly static string _clientConfigFile = "ClientConfig.xml";
        /// <summary>
        /// 客户端相关文件目录
        /// </summary>
        private readonly static string _clientFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _clientFileDir);
        /// <summary>
        /// 客户端升级配置文件
        /// </summary>
        private readonly static string _updateFilePath = Path.Combine(_clientFilePath, _updateFileName);
        
        /// <summary>
        /// 客户端图标文件
        /// </summary>
        private readonly static string _appIcoFilePath = Path.Combine(_clientFilePath, "app.ico");
        /// <summary>
        /// 客户端主程序安装文件
        /// </summary>
        private readonly static string _appSetupFilePath = Path.Combine(_clientFilePath, "setup.exe");
        /// <summary>
        /// 压缩程序exe路径
        /// </summary>
        private readonly static string _rarFilePath = Path.Combine(_clientFilePath, "Rar.exe");
        
        /// <summary>
        /// 根据Web前端传回来的IP生成客户端安装包
        /// </summary>
        /// <param name="clientIp">用户访问的ip地址域名</param>
        /// <returns>安装包下载相对路径</returns>
        public HttpFileOutput GetClientPackFile(string clientIp)
        {
            #region 参数校验
            if (string.IsNullOrWhiteSpace(clientIp))
            {
                throw new CustomHttpException("参数格式不正确！");
            }

            string ClientId = ConfigurationManager.AppSettings["ClientId"];
            string ClientTitle = ConfigurationManager.AppSettings["ClientTitle"];
            string UrlEnabled = ConfigurationManager.AppSettings["UrlEnabled"];
            if (string.IsNullOrWhiteSpace(ClientId) || string.IsNullOrWhiteSpace(ClientTitle) || string.IsNullOrWhiteSpace(UrlEnabled))
            {
                throw new CustomHttpException("config文件中没有配置客户端所需参数！");
            }
            Guid id;
            if (!Guid.TryParse(ClientId, out id))
            {
                throw new CustomHttpException("config文件中ClientId参数格式不正确！");
            }
            bool b;
            if (!bool.TryParse(UrlEnabled, out b))
            {
                throw new CustomHttpException("config文件中UrlEnabled参数格式不正确！");
            }

            if (!File.Exists(_appSetupFilePath) || !File.Exists(_rarFilePath) || !File.Exists(Path.Combine(_clientFilePath, "Default.SFX")))
            {
                throw new CustomHttpException("web站点没有配置安装包文件！");
            }
            #endregion

            XmlDocument xmldoc = new XmlDocument();
            using (System.IO.Stream ss = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Clear.CommonContext.ClientConfig.xml"))
            {
                xmldoc.Load(ss);
                ss.Close();
            }

            string homeUrl = clientIp;
            XmlNode node = xmldoc.SelectSingleNode("ClientConfig/ClientId");
            node.InnerText = ClientId;
            node = xmldoc.SelectSingleNode("ClientConfig/ClientTitle");
            node.InnerText = ClientTitle;
            node = xmldoc.SelectSingleNode("ClientConfig/UrlEnabled");
            node.InnerText = UrlEnabled;
            node = xmldoc.SelectSingleNode("ClientConfig/HomeUrl");
            node.InnerText = homeUrl;
            node = xmldoc.SelectSingleNode("ClientConfig/PluginUrl");
            node.InnerText = homeUrl+"/"+ _clientFileDir+"/"+ _pluginFileName;
            node = xmldoc.SelectSingleNode("ClientConfig/UpdateURL");
            node.InnerText = homeUrl + "/" + _clientFileDir + "/" + _updateFileName;
            
            string downLoadFileDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PlatformServiceConst.TEMP_FILE_NAME);
            if (!Directory.Exists(downLoadFileDir))
                Directory.CreateDirectory(downLoadFileDir);


            string times = DateTime.Now.ToString("MMddHHmmssfff");
            string fileDir = Path.Combine(downLoadFileDir, times);
            if (!Directory.Exists(fileDir))
                Directory.CreateDirectory(fileDir);
            string xmlConfigFile = Path.Combine(fileDir, _clientConfigFile);
            xmldoc.Save(xmlConfigFile);
            string sfxFile = Path.Combine(fileDir, "sfx.txt");
            StringBuilder sbsfx = new StringBuilder();
            sbsfx.AppendLine("Path=.\\temp");
            sbsfx.AppendLine("Setup=setup.exe");
            sbsfx.AppendLine("Silent=1");
            sbsfx.AppendLine("Overwrite=1");
            sbsfx.AppendLine("Title="+ ClientTitle);
            File.WriteAllText(sfxFile, sbsfx.ToString(), Encoding.Default);
            string clientsetupFile = Path.Combine(fileDir, "ClientSetup.exe");//" + times + "
            string appIcoFile = string.Empty;
            if (!File.Exists(_appIcoFilePath))
            {
                string faviconFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "favicon.ico");
                if (File.Exists(faviconFile))
                {
                    File.Copy(faviconFile, _appIcoFilePath);
                    appIcoFile = " \""+_appIcoFilePath+"\"";
                }
            }
            else
                appIcoFile = " \"" + _appIcoFilePath + "\"";
            try
            {
                using (Process pro = new Process())
                {
                    pro.StartInfo.WorkingDirectory = Path.GetDirectoryName(_rarFilePath);
                    pro.StartInfo.FileName = _rarFilePath;
                    pro.StartInfo.UseShellExecute = false;
                    pro.StartInfo.Arguments = $"a -ep -sfx -z\"{sfxFile}\" \"{clientsetupFile}\" \"{_appSetupFilePath}\" \"{xmlConfigFile}\"" + appIcoFile;
                    pro.Start();
                    if (pro.WaitForExit(90 * 1000))
                    {
                        return new HttpFileOutput()
                        {
                            FilePath = clientsetupFile,
                            IsTempFile = true
                        };
                    }
                    else
                        throw new CustomHttpException("调用生成安装包程序超时！");
                }
            }
            catch(Exception ex)
            {
                throw new CustomHttpException("生成安装包失败！",ex);
            }
        }

        /// <summary>
        /// 返回服务器端客户端最新版本号
        /// </summary>
        /// <returns>版本号</returns>
        [DontNeedAuth]
        public string GetLatestVersion()
        {
            Version ver = new Version("1.0.0.0");
            if (File.Exists(_updateFilePath))
            {
                try
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(_updateFilePath);
                    XmlNode node = xmldoc.SelectSingleNode("UpdateConfig/PackVersion");
                    if (node != null)
                        ver = new Version(node.InnerText);
                }
                catch(Exception ex)
                {
                    throw new CustomHttpException("版本升级文件update.xml内容错误！", ex);
                }
            }
            return ver.ToString();
        }
    }
}
