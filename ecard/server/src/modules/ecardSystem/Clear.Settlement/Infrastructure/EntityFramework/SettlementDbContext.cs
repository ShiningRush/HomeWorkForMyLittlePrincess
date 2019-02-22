using Clear.Settlement.Domain.OperatorAggregate;
using PlatformService.BridgeComponent.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.Settlement.Infrastructure.EntityFramework
{
    public class SettlementDbContext : PlatformDbContext
    {
        public IDbSet<Operator> Operators { get; set; }
        public IDbSet<BillingRecord> BillingRecords { get; set; }
        public IDbSet<SettlementRecord> SettlementRecords { get; set; }

        public SettlementDbContext()
            : base()
        {

        }

        public SettlementDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }


        public SettlementDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

       
    }
}
