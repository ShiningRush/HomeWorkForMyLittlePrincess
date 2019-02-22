using PlatformService.BridgeComponent.WebApiConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.CommonContext.AppService
{
    /// <summary>
    /// 字典管理服务接口
    /// </summary>
    public interface IDataItemAppService : IOpenWebApi
    {
        /// <summary>
        /// 添加字典分类信息
        /// </summary>
        /// <param name="item">添加实体</param>
        /// <returns>返回新增记录的主键Id</returns>
        string AddDataItem(Dtos.DataItemDto item);
        /// <summary>
        /// 更新字典分类信息
        /// </summary>
        /// <param name="item">更新实体</param>
        /// <returns></returns>
        void UpdateDataItem(Dtos.UpdateDataItemInput item);
        /// <summary>
        /// 删除字典分类信息
        /// </summary>
        /// <param name="itemIds">删除id列表</param>
        /// <returns></returns>
        void DeleteDataItem(List<string> itemIds);
        /// <summary>
        /// 获取字典分类信息
        /// </summary>
        /// <param name="getDataItem">查询实体</param>
        /// <returns></returns>
        List<Dtos.GetDataItemOutput> GetDataItem(Dtos.GetDataItemInput getDataItem);

        /// <summary>
        /// 添加字典明细信息
        /// </summary>
        /// <param name="itemDetail">添加实体</param>
        /// <returns>返回新增记录的主键Id</returns>
        string AddDataItemDetail(Dtos.DataItemDetailDto itemDetail);

        /// <summary>
        /// 更新字典信息
        /// </summary>
        /// <param name="itemDetail">更新实体</param>
        /// <returns></returns>
        void UpdateDataItemDetail(Dtos.UpdateDataItemDetailInput itemDetail);
        /// <summary>
        /// 删除字典信息
        /// </summary>
        /// <param name="itemDetailIds">删除id列表</param>
        /// <returns></returns>
        void DeleteDataItemDetail(List<string> itemDetailIds);
        /// <summary>
        /// 获取字典信息
        /// </summary>
        /// <param name="getDataItemDetail">查询实体</param>
        /// <returns></returns>
        List<Dtos.GetDataItemDetailOutput> GetDataItemDetail(Dtos.GetDataItemDetailInput getDataItemDetail);

        /// <summary>
        /// 根据类别代码查询字典信息
        /// </summary>
        /// <param name="itemCode">类别代码</param>
        /// <returns></returns>
        [DontNeedAuth]
        List<Dtos.GetDetailOutputDTO> GetDetailByCode(string itemCode);
    }
}
