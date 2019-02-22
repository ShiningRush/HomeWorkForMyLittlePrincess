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
using Clear.UserPermission.Domain;
using PlatformService.BridgeComponent.WebApiConfig;
using AutoMapper;
using PlatformService.BridgeComponent.CustomException;
using PlatformService.BridgeComponent.Domain.Repositories;
using PlatformService.BridgeComponent.Service.Session;

namespace Clear.UserPermission.Application
{
    /// <summary>
    /// 机构应用服务
    /// </summary>
    public class OrganizeAppService : ApplicationService, IOrganizeAppService
    {
        public ISessionManager SessionManager { get; set; }


        private readonly ITreeRepository<Entities.Organize, Guid> _organizeRepository = null;
        private readonly IRepository<Entities.User, Guid> _userRepository = null;
        public OrganizeAppService(ITreeRepository<Entities.Organize, Guid> organizeRepository,
            IRepository<Entities.User, Guid> userRepository)
        {
            _organizeRepository = organizeRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 添加机构
        /// </summary>
        /// <param name="entity">添加实体</param>
        /// <returns>返回新增记录的主键Id</returns>
        public string AddOrganize(Dtos.OrganizeDTO entity)
        {
            var insertEntity = entity.MapTo<Entities.Organize>();
            return _organizeRepository.InsertAndGetId(insertEntity).ToString();
        }

        /// <summary>
        /// 更新机构信息
        /// </summary>
        /// <param name="entity">更新实体</param>
        /// <returns></returns>
        public void UpdateOrganize(Dtos.UpdateOrganizeInputDTO entity)
        {
            var oldEntity = _organizeRepository.Get(Guid.Parse(entity.Id));
            var newEntity = entity.MapTo(oldEntity);
            _organizeRepository.Update(newEntity);
        }
        /// <summary>
        /// 删除机构信息
        /// </summary>
        /// <param name="ids">删除id列表</param>
        /// <returns></returns>
        public void DeleteOrganize(List<string> ids)
        {
            foreach (var id in ids)
            {
                //var orgDelete = _organizeRepository.Get(Guid.Parse(id));
                //var childrenOrgs = _organizeRepository.GetAll().Where((o) => o.ParentId == new Guid(id)).ToList();
                //bool ischildAllDelete = true;
                //foreach(var org in childrenOrgs)
                //{
                //    if (!ids.Contains(org.Id.ToString()))
                //    {
                //        ischildAllDelete = false;
                //        break;
                //    }
                //}
                //if (childrenOrgs.Count>0 && !ischildAllDelete)
                //{
                //    throw new CustomHttpException("机构“"+ orgDelete.Name + "”存在子机构，不能删除！");
                //}
                //orgDelete.Code += $"-{Guid.NewGuid().ToString().Substring(0, 8)}";
                //orgDelete.Name += "(已删除)";
                _organizeRepository.Delete(_organizeRepository.Get(Guid.Parse(id)));
            }
        }
        /// <summary>
        /// 查询机构信息
        /// </summary>
        /// <param name="inputDto">查询实体</param>
        /// <returns></returns>
        public PagerResult<Dtos.GetOrganizeOutputDTO> GetOrganize(Dtos.GetOrganizeInputDTO inputDto)
        {
            var pagerWrapper = new PagerResult<Dtos.GetOrganizeOutputDTO>();
            var table = from c in _organizeRepository.GetAll()
                        join p in _organizeRepository.GetAll()
                        on c.ParentId equals p.Id  into parent 
                        from P in parent.DefaultIfEmpty()
                        join u in _userRepository.GetAll()
                        on c.ManagerId equals u.Id into Manager
                        from U in Manager.DefaultIfEmpty()
                        join cu in _userRepository.GetAll()
                        on c.Creator equals cu.Id into Creator
                        from CU in Creator.DefaultIfEmpty()
                        join uu in _userRepository.GetAll()
                    on c.LastModifier equals uu.Id into Modifier
                        from UU in Modifier.DefaultIfEmpty()
                        select new  Dtos.GetOrganizeOutputDTO
                        {
                            Id = c.Id.ToString(),
                            Code = c.Code,
                            Name = c.Name,
                            Nature = c.Nature,
                            Address = c.Address,
                            CreateTime = c.CreateTime,
                            Creator = CU == null? "" : CU.UserName,
                            Description = c.Description,
                            Email = c.Email,
                            Fax = c.Fax,
                            FoundedTime = c.FoundedTime,
                            LastModifier = UU == null ? "" : UU.UserName,
                            LastModifyTime = c.LastModifyTime,
                            ManagerId = c.ManagerId.ToString(),
                            Manager = U == null ? "" : U.UserName,
                            ParentId = c.ParentId == null ?"": c.ParentId.ToString(),
                            ParentName = P == null ? "" : P.Name,
                            Phone = c.Phone,
                            Postalcode = c.Postalcode,
                            SortCode = c.SortCode,
                            WebAddress = c.WebAddress,
                        };
            var result = table
                .WhereIf(!inputDto.Name.IsNullOrEmpty(), p => p.Name.Contains(inputDto.Name))
                .WhereIf(!inputDto.Manager.IsNullOrEmpty(), p => p.Manager.Contains(inputDto.Manager))
                .WhereIf(!inputDto.Code.IsNullOrEmpty(), p => p.Code.Contains(inputDto.Code))
                .Page(inputDto, pagerWrapper).ToList();
            pagerWrapper.Result = result;

            //pagerWrapper.Result = result.Select(c => new Dtos.GetOrganizeOutputDTO
            //   {
            //    Id = c.Id,
            //    Code = c.Code,
            //    Name = c.Name,
            //    Nature = c.Nature,
            //    Address = c.Address,
            //    CreateTime = c.CreateTime,
            //    Creator = c.Creator,
            //    Description = c.Description,
            //    Email = c.Email,
            //    Fax = c.Fax,
            //    FoundedTime = c.FoundedTime.ToString("yyyy-MM-dd"),
            //    LastModifier = c.LastModifier,
            //    LastModifyTime = c.LastModifyTime,
            //    ManagerId = c.ManagerId,
            //    Manager = c.Manager,
            //    ParentId = c.ParentId,
            //    ParentName = c.ParentName,
            //    Phone = c.Phone,
            //    Postalcode = c.Postalcode,
            //    SortCode = c.SortCode,
            //    WebAddress = c.WebAddress
            //}).ToList();

            return pagerWrapper;
        }


        /// <summary>
        /// 获取当前登录用户的机构信息
        /// </summary>
        /// <returns></returns>
       
        public List<Dtos.GetUserOrganizeOutputDTO> GetCurrentUserOrganizes()
        {
            var orgs = _organizeRepository.GetAllChildren(SessionManager.CurrentOrganizeId.Value);
            return orgs.MapTo<List<Dtos.GetUserOrganizeOutputDTO>>();
        }
    }
}
