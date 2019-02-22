using Abp.Dependency;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using PlatformService.BridgeComponent.CustomException;
using PlatformService.BridgeComponent.Domain.Entities;
using PlatformService.BridgeComponent.Domain.Repositories;
using PlatformService.BridgeComponent.Service.Session;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.EntityFramework.Repositories
{
    public class TreeRepositoryBase<TDbContext,TTreeEntity, TPrimaryKey> : EfRepositoryBase<TDbContext, TTreeEntity, TPrimaryKey>
        , ITreeRepository<TTreeEntity, TPrimaryKey>
        where TTreeEntity : class, ITreeEntity<TPrimaryKey>
        where TDbContext : DbContext
        where TPrimaryKey : struct
    {


        public TreeRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public override TTreeEntity Update(TTreeEntity entity)
        {
            ParentIdCanNotEqualsCurrentId(entity);
            return base.Update(entity);
        }

        public override void Delete(TTreeEntity entity)
        {
            CanNotDeleteHaveChildren(entity);
            base.Delete(entity);
        }

        /// <summary>
        /// ParentId不能是当前实体的Id
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void ParentIdCanNotEqualsCurrentId(TTreeEntity entity)
        {
            if (entity.Id.Equals(entity.ParentId))
            {
                throw new CustomHttpException("ParentId不能是当前实体的Id");
            }
            List<TTreeEntity> trees = GetAllChildren(entity.Id);
            foreach(var node in trees)
            {
                if (node.Id.Equals(entity.ParentId))
                {
                    throw new CustomHttpException("ParentId不能是子节点");
                }
            }
        }

        /// <summary>
        /// 不能删除拥有子节点的结构
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void CanNotDeleteHaveChildren(TTreeEntity entity)
        {
            if (this.FirstOrDefault(CreateEqualityExpressionForParentId(entity.Id)) != null)
            {
                throw new CustomHttpException($"当前节点包含子节点，不能删除！");
            }
        }

        protected static Expression<Func<TTreeEntity, bool>> CreateEqualityExpressionForParentId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TTreeEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "ParentId"),
                Expression.Constant(id, typeof(TPrimaryKey?))
                );

            return Expression.Lambda<Func<TTreeEntity, bool>>(lambdaBody, lambdaParam);
        }

        public List<TTreeEntity> GetAllChildren(TPrimaryKey treeId)
        {
            var treeList = new List<TTreeEntity>() { this.Get(treeId) };
            foreach (var tree in this.GetAllList(CreateEqualityExpressionForParentId(treeId)))
            {
                treeList.AddRange(GetAllChildren(tree.Id));
            }
            return treeList;
        }

        public List<TTreeEntity> GetAllParents(TPrimaryKey treeId)
        {
            var treeList = new List<TTreeEntity>();
            var tree = this.Get(treeId);
            treeList.Add(tree);
            if (tree.ParentId.HasValue)
            {
                treeList.AddRange(GetAllParents(tree.ParentId.Value));
            }
            return treeList;
        }
    }
}
