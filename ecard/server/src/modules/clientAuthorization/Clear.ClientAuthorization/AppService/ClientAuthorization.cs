using Abp.Application.Services;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using Clear.ClientAuthorization.Domain;
using Clear.ClientAuthorization.Infrastructure.Encryption;
using PlatformService.BridgeComponent.Authorization;
using PlatformService.BridgeComponent.Service;
using PlatformService.Core.Common.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.ClientAuthorization.AppService
{
    public class ClientAuthorization :IApplicationService, IClientAuthorization, ISingletonDependency, IAuthorizationAppService
    {
        private readonly IDecryptor _decryptor;
        private readonly IEncryptor _encryptor;
        private readonly IRepository<AuthorizationInfo> _authorizationInfoRepo;
        private readonly IServiceContext _serviceContext;
        private readonly List<AuthorizationCache> _auths;

        public ClientAuthorization(
            IDecryptor decryptor,
            IEncryptor encryptor,
            IServiceContext serviceContext,
            IRepository<AuthorizationInfo> authorizationInfoRepo)
        {
            _decryptor = decryptor;
            _authorizationInfoRepo = authorizationInfoRepo;
            _encryptor = encryptor;
            _serviceContext = serviceContext;

            // 初始化授权数据并保存到缓存中
            var authorizationCaches = _authorizationInfoRepo.GetAllList().Select(p => {
                return new AuthorizationCache(p, _serviceContext);
            }).ToList();
            _auths = authorizationCaches;
        }

        public bool CheckAuthentication(string id, string secret)
        {
            var authorizationCaches = _auths.Where(p => p.AppID.Equals(id));
            if (authorizationCaches.Count() != 0 &&
                !authorizationCaches.First().IsKeyExpired() &&
                !authorizationCaches.First().IsAuthorizationExpired &&
                authorizationCaches.First().RSAKeys.Any(p => p.ClientIp.Equals(_serviceContext.ClientIP)))
            {
                var decryptedSecret = _decryptor.DecryptHttpSecret(secret, authorizationCaches.First().GetPrivateKey());
                if (authorizationCaches.First().IsValid(decryptedSecret, _encryptor))
                    return true;
            }

            return false;
        }

        public bool CheckMethodAuthorization(string id, string method)
        {
            var authorization = _auths.Where(p => p.AppID.Equals(_serviceContext.ClientID));
            if (authorization.Count() > 0 && authorization.First().ValidateUrl(method))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取所有的授权信息
        /// </summary>
        /// <returns>所有接入应用</returns>
        public List<AuthorizationOutputDTO> GetAllApp()
        {
            var apps = _authorizationInfoRepo.GetAllList().Select(c=>new AuthorizationOutputDTO
                        {
                           Id = c.Id,
                           AppID = c.AppID,
                           AppName = c.AppName,
                           ExpireTime = _auths.Where(p => p.AppID.Equals(c.AppID)).First().ExpireTime,

                       });
            return apps.ToList();
        }
    }
}
