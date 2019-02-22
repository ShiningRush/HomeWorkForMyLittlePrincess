using Clear.AccountManage.Domain.Accounts;
using Clear.AccountManage.Domain.Blacklist;
using PlatformService.BridgeComponent.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Infrastructure.EntityFramework
{
    public class AccountManageDbContext : PlatformDbContext
    {
        public virtual IDbSet<Account> Accounts { get; set; }
        public virtual IDbSet<Capital> Capitals { get; set; }
        public virtual IDbSet<Card> Cards { get; set; }
        public virtual IDbSet<CardLog> CardLogs { get; set; }

        public virtual IDbSet<Blacklist> Blacklists { get; set; }


        public AccountManageDbContext()
            : base()
        {

        }

        public AccountManageDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
