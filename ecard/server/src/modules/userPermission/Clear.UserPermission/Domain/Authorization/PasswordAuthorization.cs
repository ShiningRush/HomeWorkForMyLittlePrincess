using Abp.Application.Services;
using Clear.UserPermission.Domain.Authorization.Users;
using Clear.UserPermission.Entities;
using PlatformService.BridgeComponent.Authorization;
using PlatformService.BridgeComponent.Domain.Repositories;
using PlatformService.BridgeComponent.Service.Session;
using PlatformService.BridgeComponent.WebApiConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Domain.Authorization
{
    public class PasswordAuthorization : IPasswordAuthorization,Abp.Dependency.ITransientDependency, IApplicationService
    {
        private readonly IUserStore _userStore;
        private readonly ISessionManager _sessionManager;

        public PasswordAuthorization(IUserStore userStore, ISessionManager sessionManager)
        {
            _userStore = userStore;
            _sessionManager = sessionManager;
        }

        public bool CheckAuthentication(string id, string secret)
        {
            var user = _userStore.Find(id);
            if(user == null || !user.CheckPassword(secret))
            {
                return false;
            }
            SetSessionInfo(id,user);
            return true;
        }

        private void SetSessionInfo(string sessionId,User user)
        {
            //if (_sessionAccessor.GetSessionInfo() != null) return;

            var sessionInfo = new SessionInfo()
            {
                UserId = user.Id,
                UserName = user.UserName,
                LoginName = user.LoginName,
                CurrentOrganizeId = user.OrganizeId,
            };
            _sessionManager.SetSessionInfo(sessionId,sessionInfo);
        }

        [Abp.Domain.Uow.UnitOfWork(false)]
        public bool CheckMethodAuthorization(string id, string method)
        {
            var user = _userStore.Find(id);
            if (user == null) return false;
            return _userStore.HasPermission(user, method);
        }
    }
}
