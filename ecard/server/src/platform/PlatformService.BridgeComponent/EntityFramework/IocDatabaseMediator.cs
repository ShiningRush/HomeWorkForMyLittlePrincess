using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using PlatformService.BridgeComponent.Domain.Repositories;
using PlatformService.BridgeComponent.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Clear.HSPS.CloudAdapter.Infrastructure.Repositories
{
    public class IocDatabaseMediator<TEntity, TPrimary> : IDatabaseMediator<TEntity, TPrimary>, ITransientDependency where TEntity : Entity<TPrimary>
    {
        private readonly IRepositoryWithDbContext _dbContextProvider;

        private DbContext DbContext => _dbContextProvider.GetDbContext();

        public IocDatabaseMediator(IRepository<TEntity, TPrimary> dbContextProvider)
        {
            _dbContextProvider = (IRepositoryWithDbContext)dbContextProvider;
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return DbContext.Set<T>().AsQueryable();
        }

        public async Task<int> CountSqlQueryAsync<T>(string sql, params object[] objs)
        {
            var result = await DbContext.Database.SqlQuery<T>(sql, objs).CountAsync();
            return result;
        }

        public async Task<List<T>> ExcuteSqlQueryAsync<T>(string sql, params object[] objs)
        {
            var result = await DbContext.Database.SqlQuery<T>(sql, objs).ToListAsync().ConfigureAwait(false);
            return result;
        }

        public IDbSet<T> Set<T>() where T : class
        {
            return DbContext.Set<T>();
        }
    }
}
