using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clear.UserPermission.Entities;
using Abp.Domain.Repositories;
using Abp.Collections.Extensions;
using PlatformService.BridgeComponent.Domain.Repositories;

namespace Clear.UserPermission.Domain.Authorization.Users
{
    public class UserStore : IUserStore, Abp.Domain.Services.IDomainService
    {
        private readonly IRepository<User, Guid> _userRepository;
        private readonly IRepository<ModuleAuth, Guid> _moduleAuthRepository;
        private readonly ITreeRepository<Module,Guid> _moduleRepository;

        public UserStore(IRepository<User, Guid> userRepository
            , IRepository<ModuleAuth, Guid> moduleAuthRepository
            , ITreeRepository<Module, Guid> moduleRepository)
        {
            _userRepository = userRepository;
            _moduleAuthRepository = moduleAuthRepository;
            _moduleRepository = moduleRepository;
        }


        public User Find(string loginName, string password)
        {
            var user = _userRepository.FirstOrDefault(s=>s.LoginName == loginName);
            if(user != null && user.CheckPassword(password))
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public User Find(string loginName)
        {
            return _userRepository.FirstOrDefault(s => s.LoginName == loginName);
        }

        public List<ModuleAuth> GetUserModuleAuths(Guid userId)
        {
            var user = _userRepository.Get(userId);
            if (CheckIsSystemUser(user))
            {
                return _moduleAuthRepository.GetAllList();
            }
            return user.Roles.SelectMany(s => s.ModuleAuths).Distinct().ToList();
        }

        public List<Module> GetUserModules(Guid userId)
        {
            var modules = new List<Module>();
            var user = _userRepository.Get(userId);
            if (CheckIsSystemUser(user))
            {
                return _moduleRepository.GetAll().Where(s => s.IsEnabled == true).ToList();
            }
            else
            {
                modules = user.Roles.SelectMany(s => s.Modules).Where(s => s.IsEnabled == true).Distinct().ToList();
            }

            var moduleList = new List<Module>();
            modules.ForEach(item => {
                _moduleRepository.GetAllParents(item.Id).Where(s => s.IsEnabled == true).ToList().ForEach(m => {
                    moduleList.AddIfNotContains(m);
                });
            });
            return moduleList;
        }

        
        public bool HasPermission(User user, string webApiName)
        {
            if(CheckIsSystemUser(user))
            {
                return true;
            }
            //if (webApiName == @"user/GetUserInfo" || webApiName.Contains(@"/Get"))
            //    return true;
            bool b = user.Roles.SelectMany(s => s.ModuleAuths).Any(s => s.WebAPI != null && s.WebAPI.Contains(webApiName + ";"));
            return b;
        }


        private bool CheckIsSystemUser(User uesr)
        {
            return uesr.LoginName == User.SYSTEM_USERNAME;
        }
    }
}
