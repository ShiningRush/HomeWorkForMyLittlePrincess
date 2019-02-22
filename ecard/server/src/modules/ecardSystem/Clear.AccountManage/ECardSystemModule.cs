using Abp.AutoMapper;
using Abp.Modules;
using PlatformService.BridgeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage
{
    [DependsOn(
        typeof(BridgeComponentModule),
        typeof(AbpAutoMapperModule),
        typeof(Abp.EntityFramework.AbpEntityFrameworkModule))]
    public class ECardSystemModule : AbpModule
    {
        public override void PreInitialize()
        {
            //using (var context = new Infrastructure.EntityFramework.AccountManageDbContext())
            //{
            //    var account = context.Accounts.FirstOrDefault();
            //    account.Cards.Clear();
            //    context.SaveChanges();
            //}
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
        public override void PostInitialize()
        {
           
        }
    }
}
