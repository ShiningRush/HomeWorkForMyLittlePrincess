using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clear.UserPermission.Application.Dtos.Modules;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Clear.UserPermission.Entities;
using Abp.AutoMapper;
using PlatformService.BridgeComponent.CustomException;

namespace Clear.UserPermission.Application
{
    /// <summary>
    /// 模块服务
    /// </summary>
    public class ModuleAppService : ApplicationService, IModuleAppService
    {
        private readonly IRepository<Module, Guid> _moduleRepo;
        private readonly IRepository<ModuleAuth, Guid> _moduleAuthRepo;

        public ModuleAppService(
            IRepository<Module, Guid> moduleRepo,
            IRepository<ModuleAuth, Guid> moduleAuthRepo)
        {
            _moduleRepo = moduleRepo;
            _moduleAuthRepo = moduleAuthRepo;
        }

        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="deleteList">待删除模块的id</param>
        public void DeleteModule(List<Guid> deleteList)
        {
            foreach (var aDeleteId in deleteList)
            {
                var module = _moduleRepo.Get(aDeleteId);
                if (!module.AllowDelete)
                {
                    throw new CustomHttpException("模块“" + module.Name + "”不允许删除！");
                }
                _moduleRepo.Delete(module);
            }
        }

        /// <summary>
        /// 获取模块权限列表
        /// </summary>
        /// <param name="inputDto">入参</param>
        /// <returns></returns>
        public void DeleteModuleAuth(List<Guid> deleteList)
        {
            foreach (var aDeleteId in deleteList)
            {
                _moduleAuthRepo.Delete(aDeleteId);
            }
        }

        /// <summary>
        /// 获取所有模块列表
        /// </summary>
        /// <returns></returns>
        public List<ModuleDto> GetAllModules()
        {
            return _moduleRepo.GetAllList().OrderBy(s => s.ParentId).ThenBy(s => s.SortCode).MapTo<List<ModuleDto>>();
        }

        /// <summary>
        /// 获取模块权限列表
        /// </summary>
        /// <param name="inputDto">入参</param>
        /// <returns></returns>
        public List<GetModuleAuthsOutput> GetModuleAuths(GetModuleAuthsInput inputDto)
        {
            var modules = new List<GetModuleAuthsOutput>();
            if (inputDto.IsIncludeModule)
            {
                modules.AddRange(_moduleRepo.GetAll().Where(p => inputDto.ModuleIds.Contains(p.Id)).MapTo<List<GetModuleAuthsOutput>>());
            }
            var moduleAuths = _moduleAuthRepo.GetAll().Where(p => inputDto.ModuleIds.Contains(p.ModuleId)).Select(p=> new GetModuleAuthsOutput
            {
                Id = p.Id,
                ParentId = p.ModuleId,
                Code = p.Code,
                Name = p.Name,
                WebAPI = p.WebAPI,
            }).ToList();
            modules.AddRange(moduleAuths);
            return modules;
        }

        /// <summary>
        /// 插入模块权限
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        public Guid InsertModuleAuth(InsertModuleAuthInput inputDto)
        {
            Module entity = inputDto.MapTo<Module>();
            entity.AllowEdit = true;
            entity.AllowDelete = true;
            return _moduleRepo.InsertAndGetId(entity);
        }

        /// <summary>
        /// 更新模块权限
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        public void UpdateModuleAuth(UpdateModuleAuthInput inputDto)
        {
            var existedModule = _moduleRepo.Get(inputDto.Id);
            if (!existedModule.AllowEdit)
            {
                throw new CustomHttpException("模块“" + existedModule.Name + "”不允许修改！");
            }
            var childModuleAuths = existedModule.ModuleAuths.Select(p => p.Id).ToList();
            foreach (var aModuleAuth in childModuleAuths)
                _moduleAuthRepo.Delete(aModuleAuth);

            inputDto.MapTo(existedModule);
            _moduleRepo.Update(existedModule);
        }
    }
}
