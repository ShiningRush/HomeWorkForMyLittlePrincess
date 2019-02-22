using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Clear.UserPermission.Entities;
using Abp.Domain.Repositories;
using Abp.Application.Services;
using PlatformService.BridgeComponent.WebApiConfig;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Clear.UserPermission.Domain;
using Clear.UserPermission.Extensions;
using PlatformService.BridgeComponent.CustomException;
using PlatformService.BridgeComponent.Service.Session;
using Clear.UserPermission.Domain.Authorization.Users;
using YiBan.Common.Pages;
using PlatformService.BridgeComponent.Domain.Repositories;

namespace Clear.UserPermission.Application
{
    public class UserAppService : ApplicationService,IUserAppService
    {
        private readonly ITreeRepository<Department, Guid> _departmentRepository;
        private readonly ITreeRepository<Organize, Guid> _organizeRepository;
        private readonly IRepository<User, Guid> _userRepository;
        private readonly IUserStore _userStore;

        public ISessionManager SessionManager { get; set; }
        public UserAppService(ITreeRepository<Department, Guid> departmentRepository
            , ITreeRepository<Organize, Guid> organizeRepository
            , IRepository<User, Guid> userRepository
            , IUserStore userStore)
        {
            _departmentRepository = departmentRepository;
            _organizeRepository = organizeRepository;
            _userRepository = userRepository;
            _userStore = userStore;

            SessionManager = NullSessionManager.Instance;
        }

        public Guid CreateUser(InputCreateUser input)
        {
            var user = input.MapTo<User>();
            user.RestDefaultPassword();
            if (input.DepartmentIds != null)
            {
                user.Departments = new List<Department>();
                foreach (var departmentId in input.DepartmentIds)
                {
                    var department = _departmentRepository.Get(departmentId);
                    user.Departments.Add(department);
                }
            }
           
            return _userRepository.InsertAndGetId(user);
        }

        public void DeleteUser(Guid userId)
        {
            var user = _userRepository.Get(userId);
           
            _userRepository.Delete(user);
        }


        public PagerResult<UserDto> GetUsers(InputGetUsers input)
        {
            List<Guid> organizeIds = null;
            List<Guid> departmentIds = null;
            if (input.OrganizeId != null && input.OrganizeId != Guid.Empty)
            {
                organizeIds = _organizeRepository.GetAllChildren(input.OrganizeId.Value).Select(s => s.Id).ToList();
            }
            if (input.DepartmentId != null && input.DepartmentId != Guid.Empty)
            {
                departmentIds = _departmentRepository.GetAllChildren(input.DepartmentId.Value).Select(s => s.Id).ToList();
            }
            var users = _userRepository.GetAll()
                .WhereIf(!input.LoginName.IsNullOrEmpty(), s => s.LoginName.Contains(input.LoginName))
                .WhereIf(!input.UserName.IsNullOrEmpty(), s => s.UserName.Contains(input.UserName))
                .WhereIf(!input.Mobile.IsNullOrEmpty(), s => s.Mobile == input.Mobile)
                .WhereIf(organizeIds != null && organizeIds.Count > 0, s => organizeIds.Contains(s.OrganizeId))
                .WhereIf(departmentIds != null && departmentIds.Count > 0, s => s.Departments.Any(d => departmentIds.Contains(d.Id)));

            var totalResult = from user in users
                         join org in _organizeRepository.GetAll()
                         on user.OrganizeId equals org.Id
                         select new { User = user ,OrganizeName = org.Name};
            var result = from user in totalResult.OrderBy(s=>s.User.CreateTime).Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize).ToList()
                         select new UserDto
                         {
                             Id = user.User.Id,
                             OrganizeId = user.User.OrganizeId,
                             OrganizeName = user.OrganizeName,
                             Departments = user.User.Departments.ToDictionary(s => s.Id, s => s.Name),
                             Duty = user.User.Duty,
                             EMail = user.User.EMail,
                             IsReset = user.User.IsReset,
                             IsStop = user.User.IsStop,
                             JobNo = user.User.JobNo,
                             LoginName = user.User.LoginName,
                             Mobile = user.User.Mobile,
                             ProfessionalLevel = user.User.ProfessionalLevel,
                             Remark = user.User.Remark,
                             Roles = user.User.Roles.ToDictionary(s => s.Id, s => s.Name),
                             UserName = user.User.UserName
                         };
            var pageResult = new PagerResult<UserDto>()
            {
                PageIndex = input.PageIndex,
                PageSize = input.PageSize,
                TotalCount = totalResult.Count(),
                Result = result.ToList(),
            };

            return pageResult;
        }

        public List<SimpleUserDto> GetSimpleUsers(InputGetSimpleUsers input)
        {
            List<Guid> departmentIds = null;
            if (input.DepartmentId.HasValue)
            {
                departmentIds = _departmentRepository.GetAllChildren(input.DepartmentId.Value).Select(s => s.Id).ToList();
            }
            return _userRepository.GetAllIncluding(c=>c.Departments)
                .WhereIf(input.OrganizeId.HasValue, p => p.OrganizeId == input.OrganizeId.Value)
                .WhereIf(!input.UserName.IsNullOrEmpty(), p => p.UserName.Contains(input.UserName))
                .WhereIf(!input.LoginName.IsNullOrEmpty(), p => p.LoginName.Equals(input.LoginName))
                .WhereIf(departmentIds != null && departmentIds.Count > 0, s => s.Departments.Any(d => departmentIds.Contains(d.Id)))
                .Take(50).ToList()
                .Select(s => new SimpleUserDto()
                {
                    Id = s.Id,
                    UserName = s.UserName,
                    JobNo = s.JobNo,
                    LoginName = s.LoginName,
                    DepartmentName = string.Join(",", s.Departments.Select(p => p.Name).ToArray())
                }).ToList();
        }

        public void RestUserPassword(Guid userId)
        {
            var user = _userRepository.Get(userId);
            user.RestDefaultPassword();
            _userRepository.Update(user);
        }

        public void UpdateUser(InputUpdateUser input)
        {
            var user = _userRepository.Get(input.Id);
            user = input.MapTo(user);
            user.Departments.Clear();
            if(input.DepartmentIds != null)
            {
                foreach (var departmentId in input.DepartmentIds)
                {
                    var department = _departmentRepository.Get(departmentId);
                    user.Departments.Add(department);
                }
            }
            _userRepository.Update(user);
        }


        private User GetLoginUser()
        {
            if (!SessionManager.UserId.HasValue)
            {
                throw new CustomHttpException("用户没有登入，请登入！");
            }
            return _userRepository.Get(SessionManager.UserId.Value);
        }
        public UserAuthorizationDto GetUserInfo()
        {
            var user = GetLoginUser();
            var userDto = user.MapTo<UserAuthorizationDto>();
            userDto.Modules = _userStore.GetUserModules(user.Id).OrderBy(s => s.ParentId).ThenBy(s=>s.SortCode).MapTo<List<ModuleDto>>();
            userDto.ModuleAuths = _userStore.GetUserModuleAuths(user.Id).MapTo<List<ModuleAuthDto>>();

            return userDto;
        }

        public void ChangePassword(ChangePasswordInput input)
        {
            var user = GetLoginUser();
            if (!user.CheckPassword(input.OldPassword))
            {
                throw new CustomHttpException("旧密码不正确");
            }

            user.SetNewPassword(input.NewPassword);
            _userRepository.Update(user);
        }

        public void Logout()
        {
            SessionManager.ClearSessionInfo();
        }
    }
}
