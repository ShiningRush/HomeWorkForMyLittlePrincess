using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using YiBan.Common.Pages;
using Abp.Extensions;
using YiBan.Common.Extensions;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using AutoMapper;
using PlatformService.BridgeComponent.CustomException;

namespace Clear.UserPermission.Application
{
    /// <summary>
    /// 角色管理服务
    /// </summary>
    public class RoleAppService:  ApplicationService, IRoleAppService
    {
        private readonly IRepository<Entities.Role, Guid> _roleRepository = null;
        private readonly IRepository<Entities.User, Guid> _userRepository = null;
        private readonly IRepository<Entities.Module, Guid> _moduleRepository = null;
        private readonly IRepository<Entities.ModuleAuth, Guid> _moduleAuthRepository = null;
        private readonly IRepository<Entities.Organize, Guid> _organizeRepository = null;
        public RoleAppService(IRepository<Entities.Role, Guid> roleRepository,
            IRepository<Entities.User, Guid> userRepository, IRepository<Entities.Organize, Guid> organizeRepository,
            IRepository<Entities.Module, Guid> moduleRepository,IRepository<Entities.ModuleAuth, Guid> moduleAuthRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _organizeRepository = organizeRepository;
            _moduleRepository = moduleRepository;
            _moduleAuthRepository = moduleAuthRepository;
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="entity">添加实体</param>
        /// <returns>返回新增记录的主键Id</returns>
        public string AddRole(Dtos.RoleDTO entity)
        {
            var insertEntity = entity.MapTo<Entities.Role>();
            insertEntity.Id = Guid.NewGuid();
            string strName = insertEntity.Name.Trim();
            var eixstEntity = _roleRepository.GetAll().FirstOrDefault(o => o.OrganizeId == insertEntity.OrganizeId && o.Name == strName);
            if (eixstEntity != null)
            {
                throw new CustomHttpException("该机构已经存在该名称的角色！");
            }
            _roleRepository.Insert(insertEntity);
            return insertEntity.Id.ToString();
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="entity">更新实体</param>
        /// <returns></returns>
        public void UpdateRole(Dtos.UpdateRoleInputDTO entity)
        {
            var oldEntity = _roleRepository.Get(Guid.Parse(entity.Id));
            var newEntity = entity.MapTo(oldEntity);
            string strName = newEntity.Name.Trim();
            var eixstEntity = _roleRepository.GetAll().FirstOrDefault(o => o.OrganizeId == newEntity.OrganizeId && o.Name == strName);
            if (eixstEntity != null && eixstEntity.Id!= newEntity.Id)
            {
                throw new CustomHttpException("该机构已经存在该名称的角色！");
            }
            _roleRepository.Update(newEntity);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="ids">删除id列表</param>
        /// <returns></returns>
        public void DeleteRole(List<string> ids)
        {
            foreach (var id in ids)
                _roleRepository.Delete(new Guid(id));
        }
        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="inputDto">查询实体</param>
        /// <returns></returns>
        public PagerResult<Dtos.GetRoleOutputDTO> GetRoles(Dtos.GetRoleInputDTO inputDto)
        {
            var pagerWrapper = new PagerResult<Dtos.GetRoleOutputDTO>();
            var table = from c in _roleRepository.GetAll()
                        join o in _organizeRepository.GetAll()
                        on c.OrganizeId equals o.Id into Organize
                        from O in Organize.DefaultIfEmpty()
                        join cu in _userRepository.GetAll() 
                        on c.Creator equals cu.Id into Creator
                        from CU in Creator.DefaultIfEmpty()
                        join uu in _userRepository.GetAll()
                        on c.LastModifier equals uu.Id into Modifier
                        from UU in Modifier.DefaultIfEmpty()
                        select new Dtos.GetRoleOutputDTO
                        {
                            Id = c.Id.ToString(),
                            Name = c.Name,
                            CreateTime = c.CreateTime,
                            Creator = CU == null ? "" : CU.UserName,
                            Description = c.Description,
                            DataAuthority = c.DataAuthority,
                            IsEnabled = c.IsEnabled,
                            OrganizeId = c.OrganizeId,
                            LastModifier = UU == null ? "":UU.UserName,
                            LastModifyTime = c.LastModifyTime,
                            OrganizeName = O == null ? "" : O.Name,
                            SortCode = c.SortCode,
                        };
            var result = table
                .WhereIf(!inputDto.Name.IsNullOrEmpty(), p => p.Name.Contains(inputDto.Name))
                .WhereIf(!inputDto.OrganizeId.IsNullOrEmpty(), p => p.OrganizeId == new Guid(inputDto.OrganizeId))
                .WhereIf(!inputDto.OrganizeName.IsNullOrEmpty(), p => p.OrganizeName.Contains(inputDto.OrganizeName))
                .Page(inputDto, pagerWrapper)
                .ToList();
            pagerWrapper.Result = result;
            return pagerWrapper;
        }

        /// <summary>
        /// 获取角色用户
        /// </summary>
        /// <param name="roleId">角色</param>
        /// <returns></returns>
        public List<Dtos.GetRoleUserOutputDTO> GetRoleUser(string roleId)
        {
            var roleuser = _roleRepository.Get(new Guid(roleId)).Users;
            var result = from c in roleuser
                         select new Dtos.GetRoleUserOutputDTO
                        {
                            UserId = c.Id.ToString(),
                            LoginName = c.LoginName,
                            UserName = c.UserName,
                            DepartmentName = c.Departments.FirstOrDefault()==null ? "":c.Departments.FirstOrDefault().Name
                        };
            return result.ToList();
        }

        /// <summary>
        /// 保存角色用户信息
        /// </summary>
        /// <param name="dto"></param>
        public void SaveRoleUser(Dtos.SaveRoleUserInputDTO dto)
        {
            var role = _roleRepository.Get(new Guid(dto.RoleID));
            if (role != null)
            {
                if (role.Users != null)
                    role.Users.Clear();//先删除所有角色用户
                else
                    role.Users = new List<Entities.User>();
                if (dto.UserIds != null && dto.UserIds.Length>0)
                {
                    foreach(string id in dto.UserIds)
                    {
                        var user = _userRepository.Get(new Guid(id));
                        if (user != null)
                            role.Users.Add(user);
                    }
                }
                _roleRepository.Update(role);
            }
        }



        /// <summary>
        /// 获取角色模块和功能权限信息
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        public Dtos.GetRoleModuleAuthOutputDTO GetRoleModuleAuth(string roleId)
        {
            Dtos.GetRoleModuleAuthOutputDTO outDto = new Dtos.GetRoleModuleAuthOutputDTO();
            var role = _roleRepository.Get(new Guid(roleId));
            if (role != null)
            {
                if (role.Modules != null)
                {
                    List<string> ids = new List<string>();
                    foreach (var v in role.Modules)
                    {
                        ids.Add(v.Id.ToString());
                    }
                    outDto.ModuleIds = ids.ToArray();
                }
                if (role.ModuleAuths != null)
                {
                    List<string> ids = new List<string>();
                    foreach (var v in role.ModuleAuths)
                    {
                        ids.Add(v.Id.ToString());
                    }
                    outDto.ModuleAuthIds = ids.ToArray();
                }
            }
            return outDto;
        }

        /// <summary>
        /// 保存角色模块和模块权限信息
        /// </summary>
        /// <param name="dto"></param>
        public void SaveRoleModuleAuth(Dtos.SaveRoleModuleAuthInputDTO dto)
        {
            var role = _roleRepository.Get(Guid.Parse(dto.RoleID));
            role.Modules.Clear();
            role.ModuleAuths.Clear();
            foreach (var moduleId in dto.ModuleIds)
            {
                var module = _moduleRepository.Get(Guid.Parse(moduleId));
                role.Modules.Add(module);
            }
            foreach (var moduleAuthId in dto.ModuleAuthIds)
            {
                var moduleAuth = _moduleAuthRepository.Get(Guid.Parse(moduleAuthId));
                role.ModuleAuths.Add(moduleAuth);

                if(!role.Modules.Any(s=>s.Id == moduleAuth.ModuleId))
                {
                    role.Modules.Add(moduleAuth.AssignedModule);
                }
            }
            _roleRepository.Update(role);
           
        }
    }
}
