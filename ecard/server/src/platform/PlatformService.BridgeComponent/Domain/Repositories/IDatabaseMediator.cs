using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Domain.Repositories
{
    public interface IDatabaseMediator<TEntity, TPrimary> where TEntity : Entity<TPrimary>
    {
        IDbSet<T> Set<T>() where T : class;

        Task<List<T>> ExcuteSqlQueryAsync<T>(string sql, params object[] objs);

        Task<int> CountSqlQueryAsync<T>(string sql, params object[] objs);

        IQueryable<T> GetAll<T>() where T : class;
    }

    public interface IDatabaseMediator<TEntity> : IDatabaseMediator<TEntity, int> where TEntity : Entity
    {
    }
}
