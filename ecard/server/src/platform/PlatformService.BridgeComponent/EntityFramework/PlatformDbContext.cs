using Abp.EntityFramework;
using PlatformService.BridgeComponent.Domain.Entities;
using EntityFramework.DynamicFilters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformService.BridgeComponent.Domain.Uow;
using PlatformService.BridgeComponent.Service.Session;
using Abp.Events.Bus.Entities;
using Abp.Domain.Entities;
using Abp.Extensions;
using Abp;
using PlatformService.BridgeComponent.CustomException;
using System.Data.Entity.Infrastructure;
using Abp.Timing;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using Abp.Runtime.Validation;
using System.Text.RegularExpressions;
using System.Threading;
using Abp.Collections.Extensions;
using ServiceAnt;
using ServiceAnt.Handler;
using PlatformService.BridgeComponent.Domain;
using PlatformService.BridgeComponent.Domain.Events;
using System.ComponentModel;
using ServiceAnt.Subscription;

namespace PlatformService.BridgeComponent.EntityFramework
{
    public abstract class PlatformDbContext : AbpDbContext
    {
        public ISessionManager SessionManager { get; set; }

        public IDomainEventPublisher DomainEventPublisher { get; set; }

        /// <summary>
        /// Constructor.
        /// Uses <see cref="IAbpStartupConfiguration.DefaultNameOrConnectionString"/> as connection string.
        /// </summary>
        protected PlatformDbContext() : base("Default")
        {
            InitializeDbContext();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected PlatformDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            InitializeDbContext();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected PlatformDbContext(DbCompiledModel model)
            : base(model)
        {
            InitializeDbContext();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected PlatformDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            InitializeDbContext();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected PlatformDbContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
            InitializeDbContext();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected PlatformDbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
            : base(objectContext, dbContextOwnsObjectContext)
        {
            InitializeDbContext();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected PlatformDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
            InitializeDbContext();
        }

        private void InitializeDbContext()
        {
            SessionManager = NullSessionManager.Instance;
            DomainEventPublisher = NullDomainEventPublisher.Instance;
#if DEBUG
            this.Database.Log = sql => Logger.Debug(sql);
#endif
        }


        public override void Initialize()
        {
            base.Initialize();
            if(SessionManager.UserId == null)
            {
                this.DisableFilter(PlatformDataFilters.MustHaveOrganize);
            }
            else
            {
                this.SetFilterScopedParameterValue(PlatformDataFilters.MustHaveOrganize, PlatformDataFilters.Parameters.OrganizeIds, SessionManager.SupportOrganizeIds);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Filter(PlatformDataFilters.MustHaveOrganize, 
                (IMustHaveOrganize t, List<Guid> organizeIds) => organizeIds.Contains(t.OrganizeId) , new List<Guid>());  
        }

        protected override void ObjectStateManager_ObjectStateManagerChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e)
        {
            var contextAdapter = (IObjectContextAdapter)this;
            if (e.Action != CollectionChangeAction.Add)
            {
                return;
            }

            var entry = contextAdapter.ObjectContext.ObjectStateManager.GetObjectStateEntry(e.Element);
            switch (entry.State)
            {
                case EntityState.Added:
                    CheckAndSetId(entry.Entity);
                    CheckAndSetMustHaveTenantIdProperty(entry.Entity);
                    SetCreationAuditProperties(entry.Entity, GetAuditUserId());
                    break;
                    //case EntityState.Deleted: //It's not going here at all
                    //    SetDeletionAuditProperties(entry.Entity, GetAuditUserId());
                    //    break;
            }
        }

        protected override EntityChangeReport ApplyAbpConcepts()
        {
            var changeReport = new EntityChangeReport();

            var userId = GetAuditUserId();

            var entries = ChangeTracker.Entries().ToList();
            foreach (var entry in entries)
              {
                switch (entry.State)
                {
                    case EntityState.Added:
                        CheckAndSetId(entry.Entity);
                        CheckAndSetMustHaveTenantIdProperty(entry.Entity);
                        CheckAndSetMayHaveTenantIdProperty(entry.Entity);
                        SetCreationAuditProperties(entry.Entity, userId);

                        CheckAndSetMustHaveOrganizeIdProperty(entry.Entity);
                        SetCreationAuditProperties(entry.Entity,SessionManager.UserId);
                        changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Created));
                        break;
                    case EntityState.Modified:
                        SetModificationAuditProperties(entry, userId);
                        SetModificationAuditProperties(entry, SessionManager.UserId);
                        if (entry.Entity is ISoftDelete && entry.Entity.As<ISoftDelete>().IsDeleted)
                        {
                            SetDeletionAuditProperties(entry.Entity, userId);
                            changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Deleted));
                        }
                        else
                        {
                            changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Updated));
                        }

                        break;
                    case EntityState.Deleted:
                        CancelDeletionForSoftDelete(entry);
                        SetDeletionAuditProperties(entry.Entity, userId);
                        changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Deleted));
                        break;
                }
            }

            return changeReport;
        }

        protected virtual List<IEventTrigger> GetDomainEvents()
        {
            List<IEventTrigger> domainEvents = new List<IEventTrigger>();
            var entries = ChangeTracker.Entries().ToList();
            foreach (var entrie in entries)
            {
                var generatesDomainEventsEntity = entrie.Entity as Domain.Entities.IGeneratesDomainEvents;
                if (generatesDomainEventsEntity != null && !generatesDomainEventsEntity.DomainEvents.IsNullOrEmpty())
                {
                    domainEvents.AddRange(generatesDomainEventsEntity.DomainEvents);
                    generatesDomainEventsEntity.DomainEvents.Clear();
                }
            }
            return domainEvents;
        }

        protected virtual void SetCreationAuditProperties(object entityAsObj, Guid? userId)
        {
            if (entityAsObj is IHasCreatorTime)
            {
                var entityWithCreationTime = entityAsObj.As<IHasCreatorTime>();
                entityWithCreationTime.CreateTime = Clock.Now;
            }

            if (entityAsObj is IMustHaveCreator)
            {
                var entity = entityAsObj.As<IMustHaveCreator>();

                if (entity.Creator == null || entity.Creator == Guid.Empty)
                {
                    if (!userId.HasValue)
                    {
                        throw new CustomHttpException("创建人不能为空");
                    }
                    entity.Creator = userId.Value;
                }
            }
            else if(entityAsObj is IMayHaveCreator)
            {
                var entity = entityAsObj.As<IMayHaveCreator>();

                if (entity.Creator == null || entity.Creator == Guid.Empty)
                {
                    entity.Creator = userId;
                }
            }
        }

        protected virtual void SetModificationAuditProperties(DbEntityEntry entry, Guid? userId)
        {
            if (entry.Entity is IHasModifierTime)
            {
                var entityWithModifierTime = entry.Cast<IHasModifierTime>().Entity;
                entityWithModifierTime.LastModifyTime = Clock.Now;
            }

            if (entry.Entity is IHasModifier)
            {
                var entity = entry.Cast<IHasModifier>().Entity;
                entity.LastModifier = userId;
            }
        }


        protected virtual void CheckAndSetMustHaveOrganizeIdProperty(object entityAsObj)
        {
            if (!(entityAsObj is IMustHaveOrganize))
            {
                return;
            }

            var entity = entityAsObj.As<IMustHaveOrganize>();

            if (entity.OrganizeId != null && entity.OrganizeId != Guid.Empty)
            {
                return;
            }


            if (SessionManager.CurrentOrganizeId.HasValue && SessionManager.CurrentOrganizeId.Value != Guid.Empty)
            {
                entity.OrganizeId = SessionManager.CurrentOrganizeId.Value;
            }
            else
            {
                throw new CustomHttpException("机构ID不能为空！");
            }
        }

        public override int SaveChanges()
        {
            try
            {
                var domainEvents = GetDomainEvents();
                int result = base.SaveChanges();
                DomainEventPublisher.Publish(domainEvents);
                return result;
            }
            catch (DbEntityValidationException ex)
            {
                HandleDbEntityValidationException(ex);
                throw ex;
            }
            catch (DbUpdateException ex)//更新数据异常
            {
                HandleDbUpdateException(ex);
                throw ex;
            }
        }

        public override async Task<int> SaveChangesAsync()
        {
            try
            {
                var domainEvents = GetDomainEvents();
                var result = await base.SaveChangesAsync();
                await DomainEventPublisher.PublishAsync(domainEvents);
                return result;
            }
            catch (DbEntityValidationException ex)
            {
                HandleDbEntityValidationException(ex);
                throw ex;
            }
            catch (DbUpdateException ex)//更新数据异常
            {
                HandleDbUpdateException(ex);
                throw ex;
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                var domainEvents = GetDomainEvents();
                var result = await base.SaveChangesAsync(cancellationToken);
                await DomainEventPublisher.PublishAsync(domainEvents);
                return result;
            }
            catch (DbEntityValidationException ex)
            {
                HandleDbEntityValidationException(ex);
                throw ex;
            }
            catch (DbUpdateException ex)//更新数据异常
            {
                HandleDbUpdateException(ex);
                throw ex;
            }
        }


        private void HandleDbEntityValidationException(DbEntityValidationException ex)
        {
            var abpValidationException = new AbpValidationException();
            foreach (var validationResult in ex.EntityValidationErrors)
            {
                foreach (var error in validationResult.ValidationErrors)
                {
                    abpValidationException.ValidationErrors.Add(new System.ComponentModel.DataAnnotations.ValidationResult(error.ErrorMessage));
                }
            }
            throw abpValidationException;
        }

        private void HandleDbUpdateException(DbUpdateException ex)
        {
            if (ex.InnerException is System.Data.Entity.Core.UpdateException)
            {
                var updateException = ex.InnerException as System.Data.Entity.Core.UpdateException;
                if (updateException.InnerException is MySql.Data.MySqlClient.MySqlException)
                {
                    var mySqlException = updateException.InnerException as MySql.Data.MySqlClient.MySqlException;
                    if (mySqlException.Number==1062)//唯一约束错误
                    {
                        var group = Regex.Matches(mySqlException.Message, "'(.*?)'");
                        if (group.Count >= 2)
                            throw new CustomHttpException(string.Format("字段 {0} 已经存在", group[0]));
                    }
                    else if (mySqlException.Number == 1452)//外键索引错误
                    {
                        throw new CustomHttpException(string.Format("发生外键约束错误"));
                    }
                    else if (mySqlException.Number == 1451)//外键索引错误
                    {
                        throw new CustomHttpException(string.Format("该记录已经被使用"));
                    }
                    else 
                    {
                        throw new CustomHttpException(string.Format("数据操作失败,错误代码:{0}", mySqlException.Number));
                    }
                }
            }
            throw ex;
        }

    }
}
