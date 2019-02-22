using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Abp.Extensions;
using Clear.AccountManage.Domain.Accounts;
using Clear.AccountManage.Domain.Accounts.DataItem;
using Clear.AccountManage.Infrastructure.EntityFramework;
using PlatformService.BridgeComponent.CustomException;
using PlatformService.BridgeComponent.Domain.Sequence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Infrastructure.Repositories
{
    public class AccountRepository:EfRepositoryBase<AccountManageDbContext, Account,string>, IAccountRepository,Abp.Dependency.ITransientDependency
    {
        /// <summary>
        /// 序列名称
        /// </summary>
        private const string SEQUENCE_NAME = "AC";

        private readonly ISequenceGenerator _sequenceGenerator; 

        public AccountRepository(IDbContextProvider<AccountManageDbContext> dbContextProvider, ISequenceGenerator sequenceGenerator)
            : base(dbContextProvider)
        {
            _sequenceGenerator = sequenceGenerator;
        }

        public string GenerateIdentify()
        {
            return _sequenceGenerator.Next(SEQUENCE_NAME);
        }

        public Account GetAccountByCard(string cardType, string cardNo)
        {
            return this.GetAll().SingleOrDefault(s => s.Cards.Any(c => c.CardType == cardType && c.CardNo == cardNo)); 
        }

        public override Account Insert(Account entity)
        {
            IDCardTypeWithIDCardNoCanNotRepeat(entity);
            return base.Insert(entity);
        }

        public override Account Update(Account entity)
        {
            if (Context.Entry(entity).State != System.Data.Entity.EntityState.Unchanged)
               IDCardTypeWithIDCardNoCanNotRepeat(entity);
            foreach (var card in Context.ChangeTracker.Entries<Card>().ToList())
            {
                if(card.Entity.AccountId.IsNullOrEmpty())
                {
                    Context.Entry(card.Entity).State = System.Data.Entity.EntityState.Deleted;
                }
            }

            return base.Update(entity);
        }

        private void IDCardTypeWithIDCardNoCanNotRepeat(Account entity)
        {
            if (!entity.IDCardType.IsNullOrEmpty())
            {
                if (this.GetAll().Any(s => s.IDCardType == entity.IDCardType && s.IDCardNo == entity.IDCardNo && s.Id != entity.Id))
                {
                    throw new CustomHttpException($"{entity.IDCardType}：{entity.IDCardNo}已经被使用");
                }
            }
        }

    }
}
