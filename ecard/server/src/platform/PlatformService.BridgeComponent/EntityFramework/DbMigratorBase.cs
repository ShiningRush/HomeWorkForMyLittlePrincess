using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PlatformService.BridgeComponent.EntityFramework
{
    public abstract class DbMigratorBase<TDbContext, TConfiguration> : IDbMigrator
        where TDbContext : DbContext
        where TConfiguration : DbMigrationsConfiguration<TDbContext>, new()
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAbpStartupConfiguration _abpStartupConfiguration;
        private readonly IIocResolver _iocResolver;

        public DbMigratorBase(IUnitOfWorkManager unitOfWorkManager
            , IAbpStartupConfiguration abpStartupConfiguration
            , IIocResolver iocResolver)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _abpStartupConfiguration = abpStartupConfiguration;
            _iocResolver = iocResolver;
        }

        public void CreateOrMigrate()
        {
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.Suppress))
            {
                var nameOrConnectionString = GetConnectionString(_abpStartupConfiguration.DefaultNameOrConnectionString);
                using (var dbContext = _iocResolver.ResolveAsDisposable<TDbContext>(new { nameOrConnectionString = nameOrConnectionString }))
                {
                    var dbInitializer = new MigrateDatabaseToLatestVersion<TDbContext, TConfiguration>(true);

                    dbInitializer.InitializeDatabase(dbContext.Object);

                    _unitOfWorkManager.Current.SaveChanges();
                    uow.Complete();
                }
            }
        }

        public bool IsModleChanged()
        {
            Database.SetInitializer(new NullDatabaseInitializer<TDbContext>());
            var nameOrConnectionString = GetConnectionString(_abpStartupConfiguration.DefaultNameOrConnectionString);
            using (var dbContext = _iocResolver.ResolveAsDisposable<TDbContext>(new { nameOrConnectionString = nameOrConnectionString }))
            {
                if (!dbContext.Object.Database.Exists())
                    return true;
                return !dbContext.Object.Database.CompatibleWithModel(true);
            }
        }

        private string GetConnectionString(string nameOrConnectionString)
        {
            var connStrSection = ConfigurationManager.ConnectionStrings[nameOrConnectionString];
            if (connStrSection != null)
            {
                return connStrSection.ConnectionString;
            }

            return nameOrConnectionString;
        }
    }
}
