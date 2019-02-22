using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using PlatformService.BridgeComponent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Domain.Repositories
{

    public interface ITreeRepository<TTreeEntity, TPrimaryKey> : IRepository<TTreeEntity, TPrimaryKey>
        where TTreeEntity : class, ITreeEntity<TPrimaryKey>
        where TPrimaryKey : struct
    {
        /// <summary>
        /// 获取所有父节点
        /// </summary>
        /// <param name="treeId"></param>
        /// <returns></returns>
        List<TTreeEntity> GetAllParents(TPrimaryKey treeId);

        /// <summary>
        /// 获取所有子节点
        /// </summary>
        /// <param name="treeId"></param>
        /// <returns></returns>
        List<TTreeEntity> GetAllChildren(TPrimaryKey treeId);
    }
}
