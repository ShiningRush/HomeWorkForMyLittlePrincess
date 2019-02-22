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
using PlatformService.BridgeComponent.WebApiConfig;
using PlatformService.BridgeComponent.CustomException;

namespace Clear.UserPermission.Application
{
    /// <summary>
    /// 部门管理服务
    /// </summary>
    public class DepartmentAppService: ApplicationService,  IDepartmentAppService
    {
        private readonly IRepository<Entities.Department, Guid> _deptRepository = null;
        private readonly IRepository<Entities.User, Guid> _userRepository = null;
        private readonly IRepository<Entities.Organize, Guid> _organizeRepository = null;
        public DepartmentAppService(IRepository<Entities.Department, Guid> deptRepository,
            IRepository<Entities.User, Guid> userRepository, IRepository<Entities.Organize, Guid> organizeRepository)
        {
            _deptRepository = deptRepository;
            _userRepository = userRepository;
            _organizeRepository = organizeRepository;
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="entity">添加实体</param>
        /// <returns>返回新增记录的主键Id</returns>
        public string AddDepartment(Dtos.DepartmentDTO entity)
        {
            if (!entity.ManagerId.IsNullOrWhiteSpace())
            {
                Guid ManagerId;
                if (!Guid.TryParse(entity.ManagerId, out ManagerId))
                {
                    throw new CustomHttpException("机构负责人Id格式不合法！");
                }
                var userMgr = _userRepository.GetAll().FirstOrDefault(o => o.Id == ManagerId);
                if (userMgr == null)
                {
                    throw new CustomHttpException("机构负责人Id不合法！");
                }
            }
            var insertEntity = entity.MapTo<Entities.Department>();
            insertEntity.Id = Guid.NewGuid();
            _deptRepository.Insert(insertEntity);
            return insertEntity.Id.ToString();
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="entity">更新实体</param>
        /// <returns></returns>
        public void UpdateDepartment(Dtos.UpdateDepartmentInputDTO entity)
        {
            if (entity.ParentId.HasValue && entity.ParentId == Guid.Parse(entity.Id))
            {
                throw new CustomHttpException("父部门不能是自己！");
            }
            if (!entity.ManagerId.IsNullOrWhiteSpace())
            {
                Guid ManagerId;
                if (!Guid.TryParse(entity.ManagerId, out ManagerId))
                {
                    throw new CustomHttpException("机构负责人Id格式不合法！");
                }
                var userMgr = _userRepository.GetAll().FirstOrDefault(o => o.Id == ManagerId);
                if (userMgr == null)
                {
                    throw new CustomHttpException("机构负责人Id不合法！");
                }
            }
            var departmnent = _deptRepository.Get(Guid.Parse(entity.Id));

            var updateEntity = entity.MapTo(departmnent);
            _deptRepository.Update(updateEntity);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="ids">删除id列表</param>
        /// <returns></returns>
        public void DeleteDepartment(List<string> ids)
        {
            foreach (var id in ids)
            {
                var childrens = _deptRepository.GetAll().Where((o) => o.ParentId == new Guid(id)).ToList();
                bool ischildAllDelete = true;
                foreach (var c in childrens)
                {
                    if (!ids.Contains(c.Id.ToString()))
                    {
                        ischildAllDelete = false;
                        break;
                    }
                }
                if (childrens.Count > 0 && !ischildAllDelete)
                {
                    var dep = _deptRepository.Get(Guid.Parse(id));
                    throw new CustomHttpException("部门“" + dep.Name + "”存在子部门，不能删除！");
                }
                _deptRepository.Delete(new Guid(id));
            }
        }
        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="inputDto">查询实体</param>
        /// <returns></returns>
       
        public PagerResult<Dtos.GetDepartmentOutputDTO> GetDepartment(Dtos.GetDepartmentInputDTO inputDto)
        {
            var pagerWrapper = new PagerResult<Dtos.GetDepartmentOutputDTO>();
            var table = from c in _deptRepository.GetAll()
                        join p in _deptRepository.GetAll()
                        on  c.ParentId equals p.Id into Parent
                        from P in Parent.DefaultIfEmpty()
                        join o in _organizeRepository.GetAll()
                        on c.OrganizeId equals o.Id into Organize
                        from O in Organize.DefaultIfEmpty()
                        join mu in _userRepository.GetAll()
                        on c.ManagerId equals mu.Id into Manager
                        from MU in Manager.DefaultIfEmpty()
                        join cu in _userRepository.GetAll()
                        on c.Creator equals cu.Id into Creator
                        from CU in Creator.DefaultIfEmpty()
                        join uu in _userRepository.GetAll()
                        on c.LastModifier equals uu.Id into LastModifier
                        from UU in LastModifier.DefaultIfEmpty()

                        select new Dtos.GetDepartmentOutputDTO
                        {
                            Id = c.Id.ToString(),
                            Name = c.Name,
                            Email = c.Email,
                            ManagerId = c.ManagerId.ToString(),
                            Fax = c.Fax,
                            Layer = c.Layer,
                            ParentId = c.ParentId ,
                            ParentName = P.Name,
                            OrganizeName =  O.Name,
                            Phone = c.Phone,
                            CreateTime = c.CreateTime,
                            Creator = CU == null ? "" : CU.UserName,
                            Description = c.Description,
                            OrganizeId = c.OrganizeId,
                            LastModifier = UU == null ? "" : UU.UserName,
                            LastModifyTime = c.LastModifyTime,
                            SortCode = c.SortCode,
                            Manager = MU == null ? "" : MU.UserName,
                        };
            var result = table
                .WhereIf(!inputDto.DepartmentName.IsNullOrEmpty(), p => p.Name.Contains(inputDto.DepartmentName))
                .WhereIf(inputDto.OrganizeId.HasValue, p => p.OrganizeId == inputDto.OrganizeId)
                .WhereIf(!inputDto.Manager.IsNullOrEmpty(), p => p.Manager.Contains(inputDto.Manager))
                .WhereIf(inputDto.ParentId.HasValue, p => p.ParentId == inputDto.ParentId)
                .Page(inputDto, pagerWrapper)
                .ToList();
            pagerWrapper.Result = result;
            return pagerWrapper;
        }

        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="organizeId">查询实体</param>
        /// <returns></returns>
        
        public List<Dtos.GetDepartmentByOrgOutputDTO> GetDepartmentByOrganizeId(Guid organizeId)
        {
            var table = from c in _deptRepository.GetAll()
                        where c.OrganizeId == organizeId
                        select new Dtos.GetDepartmentByOrgOutputDTO
                        {
                            Id = c.Id,
                            Name = c.Name,
                            ParentId = c.ParentId,
                            OrganizeId = c.OrganizeId,
                        };
            return table.ToList();
        }
    }
}
