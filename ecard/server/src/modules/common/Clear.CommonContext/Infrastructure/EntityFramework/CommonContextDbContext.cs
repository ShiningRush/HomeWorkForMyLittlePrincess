using Clear.CommonContext.Domain.DataItemAggregate;
using Clear.CommonContext.Domain.OperationLogAggregate;
using Clear.CommonContext.Domain.SequenceModle;
using PlatformService.BridgeComponent.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.CommonContext.Infrastructure.EntityFramework
{
    public class CommonContextDbContext : PlatformDbContext
    {
        //TODO: Define an IDbSet for each Entity...
        public virtual IDbSet<DataItem> DataItems { get; set; }
        public virtual IDbSet<DataitemDetail> DataitemDetails { get; set; }
        public virtual IDbSet<OpeartionlogConfig> OpeartionlogConfigs { get; set; }
        public virtual IDbSet<Operationlog> Operationlogs { get; set; }

        public virtual IDbSet<SequenceModle> SequenceModles { get; set; }

        //Example:
        //public virtual IDbSet<User> Users { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public CommonContextDbContext()
            : base()
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in WebApiGatewayDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of WebApiGatewayDbContext since ABP automatically handles it.
         */
        public CommonContextDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();


        }
    }
}
