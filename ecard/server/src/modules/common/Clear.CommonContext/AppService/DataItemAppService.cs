using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clear.CommonContext.Domain.DataItemAggregate;
using PlatformService.BridgeComponent.WebApiConfig;
using Abp.Extensions;
using Abp.Collections.Extensions;
using PlatformService.BridgeComponent.CustomException;

namespace Clear.CommonContext.AppService
{
    /// <summary>
    /// 字典管理服务接口
    /// </summary>
    public class DataItemAppService : ApplicationService, IDataItemAppService
    {
        private readonly IRepository<DataItem, Guid> _dataItemRepo = null;
        private readonly IRepository<DataitemDetail, Guid> _dataItemDetailRepo = null;
        public DataItemAppService(
           IRepository<DataItem, Guid> dataItemRepo,
           IRepository<DataitemDetail, Guid> dataItemDetailRepo
           )
        {
            _dataItemRepo = dataItemRepo;
            _dataItemDetailRepo = dataItemDetailRepo;
        }
        /// <summary>
        /// 添加字典分类信息
        /// </summary>
        /// <param name="item">添加实体</param>
        /// <returns>返回新增记录的主键Id</returns>
        public string AddDataItem(Dtos.DataItemDto item)
        {
            item.AllowDelete = true;
            item.AllowEdit = true;
            var insertEntity = item.MapTo<DataItem>();
            _dataItemRepo.Insert(insertEntity);
            return insertEntity.Id.ToString();
        }
        /// <summary>
        /// 更新字典分类信息
        /// </summary>
        /// <param name="item">更新实体</param>
        /// <returns></returns>
        public void UpdateDataItem(Dtos.UpdateDataItemInput item)
        {
            if (!item.ParentId.IsNullOrWhiteSpace() && item.ParentId == item.Id)
            {
                throw new CustomHttpException("父分类不能是自己！");
            }
            var oldEntity = _dataItemRepo.Get(Guid.Parse(item.Id));
            if (!oldEntity.AllowEdit)
            {
                throw new CustomHttpException("该分类不允许修改！");
            }
            item.AllowDelete = oldEntity.AllowDelete;
            item.AllowEdit = oldEntity.AllowEdit;
            var updateEntity = item.MapTo(oldEntity);
            _dataItemRepo.Update(updateEntity);
        }
        /// <summary>
        /// 删除字典分类信息
        /// </summary>
        /// <param name="itemIds">删除id列表</param>
        /// <returns></returns>
        public void DeleteDataItem(List<string> itemIds)
        {
            foreach (var id in itemIds)
            {
                var childrens = _dataItemRepo.GetAll().Where((o) => o.ParentId == new Guid(id)).ToList();
                bool ischildAllDelete = true;
                foreach (var c in childrens)
                {
                    if (!itemIds.Contains(c.Id.ToString()))
                    {
                        ischildAllDelete = false;
                        break;
                    }
                }
                if (childrens.Count > 0 && !ischildAllDelete)
                {
                    var item = _dataItemRepo.Get(Guid.Parse(id));
                    throw new CustomHttpException("字典分类“" + item.ItemName + "”存在子分类，不能删除！");
                }
                var oldEntity = _dataItemRepo.Get(new Guid(id));
                if (!oldEntity.AllowDelete)
                {
                    throw new CustomHttpException("字典分类“" + oldEntity.ItemName + "”不允许删除！");
                }
                _dataItemRepo.Delete(new Guid(id));
            }
        }
        /// <summary>
        /// 获取字典分类信息
        /// </summary>
        /// <param name="getDataItem">查询实体</param>
        /// <returns></returns>
        public List<Dtos.GetDataItemOutput> GetDataItem(Dtos.GetDataItemInput getDataItem)
        {
            var result = _dataItemRepo.GetAll().WhereIf(getDataItem.Id.HasValue, p => p.Id == getDataItem.Id)
                                  .WhereIf(!getDataItem.ItemName.IsNullOrWhiteSpace(), p => p.ItemName.Contains(getDataItem.ItemName))
                                  .WhereIf(getDataItem.ParentId.HasValue, p => p.ParentId  == getDataItem.ParentId).ToList();
            return result.MapTo<List<Dtos.GetDataItemOutput>>();
        }

        /// <summary>
        /// 添加字典明细信息
        /// </summary>
        /// <param name="itemDetail">添加实体</param>
        /// <returns>返回新增记录的主键Id</returns>
        public string AddDataItemDetail(Dtos.DataItemDetailDto itemDetail)
        {
            itemDetail.AllowEdit = true;
            itemDetail.AllowDelete = true;
            var insertEntity = itemDetail.MapTo<DataitemDetail>();
            _dataItemDetailRepo.Insert(insertEntity);
            if (insertEntity.IsDefault) //刷新是否默认字段
            {
                var items = _dataItemDetailRepo.GetAll().Where(p => p.ItemId == insertEntity.ItemId && p.IsDefault);
                foreach(var item in items)
                {
                    if (item.Id != insertEntity.Id)
                    {
                        item.IsDefault = false;
                        _dataItemDetailRepo.Update(item);
                    }
                }
            }
            return insertEntity.Id.ToString();
        }

        /// <summary>
        /// 更新字典信息
        /// </summary>
        /// <param name="itemDetail">更新实体</param>
        /// <returns></returns>
        public void UpdateDataItemDetail(Dtos.UpdateDataItemDetailInput itemDetail)
        {
            var oldEntity = _dataItemDetailRepo.Get(Guid.Parse(itemDetail.Id));
            if (!oldEntity.AllowEdit)
            {
                throw new CustomHttpException("该字典不允许修改！");
            }
            itemDetail.AllowDelete = oldEntity.AllowDelete;
            itemDetail.AllowEdit = oldEntity.AllowEdit;
            var updateEntity = itemDetail.MapTo(oldEntity);
            _dataItemDetailRepo.Update(updateEntity);
            if (updateEntity.IsDefault) //刷新是否默认字段
            {
                var items = _dataItemDetailRepo.GetAll().Where(p => p.ItemId == updateEntity.ItemId && p.IsDefault);
                foreach (var item in items)
                {
                    if (item.Id != updateEntity.Id)
                    {
                        item.IsDefault = false;
                        _dataItemDetailRepo.Update(item);
                    }
                }
            }
        }
        /// <summary>
        /// 删除字典信息
        /// </summary>
        /// <param name="itemDetailIds">删除id列表</param>
        /// <returns></returns>
        public void DeleteDataItemDetail(List<string> itemDetailIds)
        {
            foreach (var id in itemDetailIds)
            {
                var oldEntity = _dataItemDetailRepo.Get(new Guid(id));
                if (!oldEntity.AllowDelete)
                {
                    throw new CustomHttpException("字典代码“" + oldEntity.ItemCode + "”不允许删除！");
                }
                _dataItemDetailRepo.Delete(new Guid(id));
            }
        }
        /// <summary>
        /// 获取字典信息
        /// </summary>
        /// <param name="getDataItemDetail">查询实体</param>
        /// <returns></returns>
        public List<Dtos.GetDataItemDetailOutput> GetDataItemDetail(Dtos.GetDataItemDetailInput getDataItemDetail)
        {
            List<Dtos.GetDataItemDetailOutput> outputDetails = new List<Dtos.GetDataItemDetailOutput>();
            if (!string.IsNullOrWhiteSpace(getDataItemDetail.Id))
            {
                var entity = _dataItemDetailRepo.Get(new Guid(getDataItemDetail.Id));
                if (entity != null)
                    outputDetails.Add(entity.MapTo<Dtos.GetDataItemDetailOutput>());
            }
            else if (getDataItemDetail.ItemId != null && getDataItemDetail.ItemId != Guid.Empty)
            {
                if (string.IsNullOrWhiteSpace(getDataItemDetail.Keyword))
                {
                    var entityList = _dataItemDetailRepo.GetAll().Where((o) => o.IsDelete == false && o.ItemId == getDataItemDetail.ItemId).ToList();
                    foreach (var entity in entityList)
                        outputDetails.Add(entity.MapTo<Dtos.GetDataItemDetailOutput>());
                }
                else
                {
                    var entityList = _dataItemDetailRepo.GetAll().Where((o) => o.IsDelete == false && o.ItemId == getDataItemDetail.ItemId && o.ItemValue.Contains(getDataItemDetail.Keyword)).ToList();
                    foreach (var entity in entityList)
                        outputDetails.Add(entity.MapTo<Dtos.GetDataItemDetailOutput>());
                }
            }
            else if (!string.IsNullOrWhiteSpace(getDataItemDetail.Keyword))
            {
                var entityList = _dataItemDetailRepo.GetAll().Where((o) => o.IsDelete == false && o.ItemValue.Contains(getDataItemDetail.Keyword)).ToList();
                foreach (var entity in entityList)
                    outputDetails.Add(entity.MapTo<Dtos.GetDataItemDetailOutput>());
            }
            return outputDetails;
        }

        /// <summary>
        /// 根据类别代码查询字典信息
        /// </summary>
        /// <param name="itemCode">类别代码</param>
        /// <returns>返回的字段列表</returns>
        
        public List<Dtos.GetDetailOutputDTO> GetDetailByCode(string itemCode)
        {
            var details = from c in _dataItemDetailRepo.GetAll()
                          where c.IsEnabled == true && c.IsDelete == false && c.DataItem.ItemCode == itemCode
                          orderby c.SortCode ascending
                          select new Dtos.GetDetailOutputDTO
                          {
                              Code = c.ItemCode,
                              Value = c.ItemValue,
                              IsDefault = c.IsDefault,
                              SortCode = c.SortCode,
                              Description = c.Description,
                          };
            return details.ToList();
        }
    }
}
