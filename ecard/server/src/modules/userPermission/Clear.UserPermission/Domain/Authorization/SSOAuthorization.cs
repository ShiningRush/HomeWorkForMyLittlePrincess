using Abp.Dependency;
using Clear.UserPermission.Domain.Authorization.Users;
using Newtonsoft.Json;
using PlatformService.BridgeComponent.Authorization;
using PlatformService.BridgeComponent.CustomException;
using PlatformService.BridgeComponent.Service.Session;
using PlatformService.BridgeComponent.WebApiConfig;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Domain.Authorization
{
    /// <summary>
    /// SSO相关服务
    /// </summary>
    [DontNeedAuth]
    public interface ISsoService : IOpenWebApi
    {
        /// <summary>
        /// SSO回调绑定接口
        /// </summary>
        /// <param name="userId">SSO用户ID</param>
        /// <param name="appId">在SSO的应用ID</param>
        void BindAccount(string userId, string appId);
    }

    public class SSOAuthorization : ISSOAuthorization, ISsoService, ITransientDependency
    {
        private readonly IUserStore _userStore;
        private readonly ISessionManager _sessionManager;

        public SSOAuthorization(ISessionManager sessionManager, IUserStore userStore)
        {
            _sessionManager = sessionManager;
            _userStore = userStore;
        }

        /// <summary>
        /// SSO回调绑定接口
        /// </summary>
        /// <param name="userId">SSO用户ID</param>
        /// <param name="appId">在SSO的应用ID</param>
        public void BindAccount(string userId, string appId)
        {
            var ssoServerIp = ConfigurationManager.AppSettings["SSOServerIp"];
            var appUserName = _sessionManager.LoginName;
            using (var httpClient = new HttpClient())
            {
                var result = httpClient.PostAsync(ssoServerIp + $"/api/auth/user/bind?userId={userId}&appId={appId}&appUserName={appUserName }", null);
                var ssoResult = JsonConvert.DeserializeObject<SSOValidateResult>(result.Result.Content.ReadAsStringAsync().Result);
                if (!ssoResult.success)
                    throw new CustomHttpException("调用统一门户绑定接口失败， 原因：" + ssoResult.message);
            }
        }

        public bool CheckTicket(string token, string userName)
        {
            var ssoServerIp = ConfigurationManager.AppSettings["SSOServerIp"];
            using (var httpClient = new HttpClient())
            {
                var result = httpClient.PostAsync(ssoServerIp + $"/api/auth/token/valid?token={token}&userName={userName}" , null);
                var ssoResult = JsonConvert.DeserializeObject<SSOValidateResult>(result.Result.Content.ReadAsStringAsync().Result);
                if (ssoResult.success && SetSessionInfo(userName))
                    return true;

                return false;
            }
        }

        private bool SetSessionInfo(string loginName)
        {
            var user = _userStore.Find(loginName);
            if (user == null)
                return false;

            var sessionInfo = new SessionInfo()
            {
                UserId = user.Id,
                UserName = user.UserName,
                LoginName = user.LoginName,
                CurrentOrganizeId = user.OrganizeId,
            };
            _sessionManager.SetSessionInfo(loginName, sessionInfo);
            return true;
        }
    }

    public class SSOValidateResult
    {
        public bool success { get; set; }
        public string code { get; set; }
        public string message { get; set; }
    }
}
