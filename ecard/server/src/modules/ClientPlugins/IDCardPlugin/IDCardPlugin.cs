using Chromium.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clear.ECSIDCardPlugin
{
    [Export(typeof(IChromiumPlugin))]
    public class IDCardPlugin : IChromiumPlugin
    {
        private ILogger _logger = LogManager.GetLogger(typeof(IDCardPlugin));
        /// <summary>
        /// 装载插件
        /// </summary>
        /// <param name="webBrowser">浏览器操作接口</param>
        public void Load(IChromiumWebBrowser webBrowser)
        {
            //webBrowser.RegisterJsFunction("PluginTesterCall", (o) =>
            // {
            //     webBrowser.EvaluateJavascript("addCallback(100, 200)",
            //         (value, exception) => {
            //             webBrowser.MainForm.BeginInvoke((MethodInvoker)(() => {
            //                 MessageBox.Show(webBrowser.MainForm, value);
            //             }));
            //     });
            // }
            //);
            bool b = webBrowser.RegisterJsFunction("ReadCertificateInfo", (s) =>
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\ReadCertificateInfo.json"))
                    return File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\ReadCertificateInfo.json");
                string str = Newtonsoft.Json.JsonConvert.SerializeObject(ReadCertificateInfo(s));
                _logger.Info("ReadCertificateInfo Json:" + str);
                return str;
            });
            _logger.Info("注册ReadCertificateInfo:" + b);
            b = webBrowser.RegisterJsFunction("ReadCardInfo", (s) =>
            {
                try
                {
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\ReadCardInfo.json"))
                        return File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\ReadCardInfo.json");
                    string str = Newtonsoft.Json.JsonConvert.SerializeObject(ReadCardInfo(s));
                    _logger.Info("ReadCardInfo Json:" + str);
                    return str;
                }
                catch(Exception ex)
                {
                    return ex.Message;
                }
            });
            _logger.Info("注册ReadCardInfo:" + b);
        }
        /// <summary>
        /// 卸载插件
        /// </summary>
        /// <param name="webBrowser">浏览器操作接口</param>
        public void Unload(IChromiumWebBrowser webBrowser)
        {

        }
        /// <summary>
        /// 读取身份证件信息
        /// </summary>
        /// <param name="idCardType"></param>
        /// <returns></returns>
        private ResultInfo<IDCardInfo> ReadCertificateInfo(string idCardType)
        {
            _logger.Info("ReadIDCard idCardType:" + idCardType);
            ResultInfo<IDCardInfo> resultinfo = new ResultInfo<IDCardInfo>();
            resultinfo.Code = -1;
            resultinfo.Message = "未知错误";
            try
            {
                string comcfgFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory , "idcard.cfg");
                int port = 1001;
                if (File.Exists(comcfgFile))
                {
                    string[] lines = File.ReadAllLines(comcfgFile);
                    if (lines != null && lines.Length>0)
                    {
                        port = int.Parse(lines[0]);
                    }
                }
                IDCardHD ihd = new IDCardHD();
                int state = ihd.Init(port);
                if (state != 0)
                    resultinfo.Message = "初始化设备失败,错误代码：" + state;
                else
                {
                    IDCardData cardData = new IDCardData();
                    state = ihd.IDCardRead(ref cardData);
                    if (state != 0)
                        resultinfo.Message = "读卡失败,错误代码：" + state;
                    else
                    {
                        ihd.Close();
                        resultinfo.Code = 0;
                        resultinfo.Message = string.Empty;
                        resultinfo.Data = new IDCardInfo();
                        if (File.Exists(cardData.IDFrontImgFileName))
                        {
                            resultinfo.Data.Avatar = File.ReadAllBytes(cardData.IDFrontImgFileName);
                            File.Delete(cardData.IDFrontImgFileName);
                        }

                        resultinfo.Data.Name = cardData.Nation;
                        if (cardData.Sex == "男")
                            resultinfo.Data.Sex = "M";
                        else if (cardData.Sex == "女")
                            resultinfo.Data.Sex = "F";
                        else
                            resultinfo.Data.Sex = "";
                        resultinfo.Data.IDCardNo = cardData.IDCardNo;
                        resultinfo.Data.Nation = cardData.Nation;
                        resultinfo.Data.Address = cardData.Address;
                        string str = cardData.Born;//20121002
                        if (str.Length==8)
                        {
                            str = str.Insert(4, "-");
                            str = str.Insert(7, "-");
                            resultinfo.Data.BirthDay = DateTime.Parse(str);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                resultinfo.Message = ex.Message;
                _logger.Error("ReadIDCard" ,ex);
            }
            return resultinfo;
        }
        /// <summary>
        /// 读卡信息
        /// </summary>
        /// <param name="cardType"></param>
        /// <returns></returns>
        private ResultInfo<string> ReadCardInfo(string cardType)
        {
            ResultInfo<string> resultinfo = new ResultInfo<string>();
            resultinfo.Code = -1;
            resultinfo.Message = "未知错误";
            try
            {
                //未实现访问硬件设备代码
                resultinfo.Code = 0;
                resultinfo.Message = "";
                resultinfo.Data = "646432223";
            }
            catch (Exception ex)
            {
                resultinfo.Message = ex.Message;
                _logger.Error("ReadCard", ex);
            }
            return resultinfo;
        }
    }

    public class ResultInfo<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public T Data { get; set; }
    }

    public class IDCardInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 身份证件号码
        /// </summary>
        public string IDCardNo { get; set; }
        /// <summary>
        /// 性别 转成 一卡通字典对应的代码
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 民族身份证的原始内容
        /// </summary>
        public string Nation { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? BirthDay { get; set; }
        /// <summary>
        /// 身份图像
        /// </summary>
        public byte[] Avatar { get; set; }

    }
}
