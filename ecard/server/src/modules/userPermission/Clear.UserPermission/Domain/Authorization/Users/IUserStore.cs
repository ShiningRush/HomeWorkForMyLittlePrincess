using Clear.UserPermission.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Domain.Authorization.Users
{
    public interface IUserStore
    {
        User Find(string loginName, string password);

        User Find(string loginName);

        bool HasPermission(User user,string webApiName);

        List<Module> GetUserModules(Guid userId);

        List<ModuleAuth> GetUserModuleAuths(Guid userId);
    }
}
