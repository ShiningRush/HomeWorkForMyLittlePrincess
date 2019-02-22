using Clear.UserPermission.Entities;
using PlatformService.BridgeComponent.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Configuration;
using PlatformService.BridgeComponent.CustomException;
using EntityFramework.DynamicFilters;
using Abp.Domain.Entities;

namespace Clear.UserPermission.EntityFramework
{
    public class UserPermissionDbContext: PlatformDbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<ModuleAuth> ModuleButton { get; set; }
        public DbSet<Organize> Organizes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }


        /// <summary>
        /// Constructor.
        /// Uses <see cref="IAbpStartupConfiguration.DefaultNameOrConnectionString"/> as connection string.
        /// </summary>
        public UserPermissionDbContext()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserPermissionDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserPermissionDbContext(DbCompiledModel model)
            : base(model)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserPermissionDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserPermissionDbContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserPermissionDbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
            : base(objectContext, dbContextOwnsObjectContext)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserPermissionDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<User>().HasMany(d => d.Roles).WithMany(x => x.Users).Map(m=>m.ToTable("sys_userRoles"));

            modelBuilder.Entity<User>().HasMany(d => d.Departments).WithMany(x => x.Users).Map(m => m.ToTable("sys_userDepartment"));

            modelBuilder.Entity<Role>().HasMany(d => d.Modules).WithMany(x => x.Roles).Map(m => m.ToTable("sys_roleModule"));

            modelBuilder.Entity<Role>().HasMany(d => d.ModuleAuths).WithMany(x => x.Roles).Map(m => m.ToTable("sys_roleModuleAuth"));

        }

        protected override void CancelDeletionForSoftDelete(DbEntityEntry entry)
        {
            if (entry.Entity is Organize)
            {
                Guid organizeId = (entry.Entity as Organize).Id;
                var depart = this.Departments.FirstOrDefault(o => o.OrganizeId == organizeId);
                if (depart != null)
                {
                    throw new CustomHttpException("该机构下存在部门不能删除！");
                }
                var user = this.Users.FirstOrDefault(o => o.OrganizeId == organizeId);
                if (user != null)
                {
                    throw new CustomHttpException("该机构下存在用户不能删除！");
                }
                var role = this.Roles.FirstOrDefault(o => o.OrganizeId == organizeId);
                if (role != null)
                {
                    throw new CustomHttpException("该机构下存在用户角色不能删除！");
                }
            }
            else if (entry.Entity is User && (entry.Entity as User).Id == Guid.Parse(User.SYSTEM_USERID))
            {
                throw new CustomHttpException("超级管理员不能删除！");
            }

            if (!(entry.Entity is ISoftDelete))
            {
                return;
            }

            var softDeleteEntry = entry.Cast<ISoftDelete>();
            softDeleteEntry.State = EntityState.Modified;
            softDeleteEntry.Entity.IsDeleted = true;
        }

        protected override void SetModificationAuditProperties(DbEntityEntry entry, Guid? userId)
        {
            if (entry.Entity is User && (entry.Entity as User).Id == Guid.Parse(User.SYSTEM_USERID) && (entry.Entity as User).LoginName != User.SYSTEM_USERNAME )
            {
                throw new CustomHttpException("超级管理员登录名不能修改！");
            }
            base.SetModificationAuditProperties(entry, userId);
        }

    }
}
