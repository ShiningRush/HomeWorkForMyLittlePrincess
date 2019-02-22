using Abp.Extensions;
using Abp.Logging;
using Clear.ClientAuthorization.Consts;
using Clear.ClientAuthorization.Infrastructure.Encryption;
using HISP.Authorization;
using PlatformService.BridgeComponent.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Clear.ClientAuthorization.Domain
{
    public class AuthorizationCache
    {
        private readonly IServiceContext _serviceContext;

        /// <summary>
        /// 终端应用ID
        /// </summary>
        public string AppID { get; set; }

        /// <summary>
        /// 终端应用名
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// 应用授权密钥hash值
        /// 应用授权密钥的Hash值，加密信息关联机器硬件码信息
        /// </summary>
        public string AppSecretHash { get; set; }

        /// <summary>
        /// 授权密钥加密随机值
        /// </summary>
        public string AppSecretSalt { get; set; }

        /// <summary>
        /// 授权验证数据
        /// </summary>
        public string ApiAuthorizations { get; set; }

        /// <summary>
        /// 剩余时间
        /// </summary>
        public int RestDays => this.ExpireTime.Subtract(DateTime.Now).Days;

        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime ExpireTime => _authDataReader.ExpireTime;

        /// <summary>
        /// RSA字典
        /// </summary>
        public List<RSAKey> RSAKeys { get; set; }

        /// <summary>
        /// 是否过期
        /// </summary>
        public bool IsAuthorizationExpired => _authDataReader.IsExpire;

        private AuthDataReader _authDataReader { get; set; }

        /// <summary>
        /// 初始化授权信息缓存
        /// </summary>
        /// <param name="initInfo"></param>
        /// <param name="serviceContext"></param>
        public AuthorizationCache(AuthorizationInfo initInfo, IServiceContext serviceContext)
        {
            _serviceContext = serviceContext;

            this.AppName = initInfo.AppName;
            this.AppID = initInfo.AppID;
            this.AppSecretHash = initInfo.AppSecretHash;
            this.AppSecretSalt = initInfo.AppSecretSalt;
            this.RSAKeys = new List<RSAKey>();

            string errMsg;
            _authDataReader = new AuthDataReader();
            _authDataReader.AddLogger += (msg) => {
                LogHelper.Logger.Info(msg);
            };

            _authDataReader.InitAuthData(this.AppID, this.AppSecretHash, initInfo.AppSecretSalt, initInfo.ValidateData, "Ecard", out errMsg);
            if (!errMsg.IsNullOrEmpty())
            {
                LogHelper.Logger.ErrorFormat("初始化授权信息时发生了错误。错误信息: {0}", errMsg);
                //throw new AbpException(errMsg);
            }

            this.ApiAuthorizations = _authDataReader.AuthData;
            LogHelper.Logger.DebugFormat("已加载授权记录缓存，AppID: {0},权限路径:{1}", this.AppID, this.ApiAuthorizations);
        }

        /// <summary>
        /// 验证Secret是否有效
        /// </summary>
        /// <param name="decryptedSecret"></param>
        /// <param name="encryptor"></param>
        /// <returns></returns>
        public bool IsValid(string decryptedSecret, IEncryptor encryptor)
        {
            if (decryptedSecret.IsNullOrEmpty())
            {
                return false;
            }

            return this.AppSecretHash.Equals(encryptor.EncryptSecret(decryptedSecret, this.AppSecretSalt));
        }

        /// <summary>
        /// 验证Url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool ValidateUrl(string url)
        {
            var serviceName = url.Substring(0, url.LastIndexOf(@"/"));

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(this.ApiAuthorizations);

            var moduleNodes = xmlDoc.SelectNodes("/AuthData/ApiList/Module[@IsFull='true']");
            foreach (XmlNode aModuleNode in moduleNodes)
            {
                if (aModuleNode.Attributes["Name"].Value.Equals(serviceName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            var apiNodes = xmlDoc.SelectNodes("/AuthData/ApiList/Module/API");
            foreach (XmlNode aApiNode in apiNodes)
            {
                if (aApiNode.Attributes["Name"].Value.Equals(url, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 获取公钥
        /// </summary>
        /// <param name="encryptor"></param>
        /// <returns></returns>
        public string GetPublicKey(IEncryptor encryptor)
        {
            var cachePrivateKey = this.RSAKeys.Where(p => p.ClientIp.Equals(_serviceContext.ClientIP));
            if (cachePrivateKey.Count() > 0)
            {
                if (!cachePrivateKey.First().IsExpired())
                {
                    cachePrivateKey.First().ExpireTime = DateTime.Now.AddMinutes(ClientAuthorizationConsts.PUBLIC_KEY_EXPIRE_TIME);
                    return cachePrivateKey.First().PublicValue;
                }

                return this.RefreshRSAKey(encryptor, cachePrivateKey.First()).PublicValue;
            }


            var newPrivateKey = this.RefreshRSAKey(encryptor);
            this.RSAKeys.Add(newPrivateKey);
            return newPrivateKey.PublicValue;
        }

        /// <summary>
        /// 刷新RSAKey过期时间
        /// </summary>
        /// <param name="encryptor"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private RSAKey RefreshRSAKey(IEncryptor encryptor, RSAKey key = null)
        {
            var defaultKey = new RSAKey();
            if (key != null)
            {
                defaultKey = key;
            }

            string publicKey, privateKey;
            encryptor.GetPublicAndPrivateKey(out publicKey, out privateKey);

            defaultKey.ClientIp = _serviceContext.ClientIP;
            defaultKey.PrivateValue = privateKey;
            defaultKey.PublicValue = publicKey;
            defaultKey.ExpireTime = DateTime.Now.AddMinutes(ClientAuthorizationConsts.PUBLIC_KEY_EXPIRE_TIME);

            return defaultKey;
        }

        public string GetPrivateKey()
        {
            var matchedRSAKeys = this.RSAKeys.Where(p => p.ClientIp.Equals(_serviceContext.ClientIP));
            if (matchedRSAKeys.Count() > 0)
            {
                return matchedRSAKeys.First().PrivateValue;
            }

            return string.Empty;
        }

        public bool IsKeyExpired()
        {
            var targetCacheKey = this.RSAKeys.Where(p => p.ClientIp.Equals(_serviceContext.ClientIP));
            if (targetCacheKey.Count() != 0)
            {
                return targetCacheKey.First().IsExpired();
            }

            return true;
        }
    }
    public class RSAKey
    {
        public string ClientIp { get; set; }
        public string PublicValue { get; set; }
        public string PrivateValue { get; set; }
        public DateTime ExpireTime { get; set; }

        public bool IsExpired()
        {
            return DateTime.Now >= this.ExpireTime;
        }
    }
}
