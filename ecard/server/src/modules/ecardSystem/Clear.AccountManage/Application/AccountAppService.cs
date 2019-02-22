using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Clear.AccountManage.Domain.Accounts;
using Clear.AccountManage.Domain.Accounts.DataItem;
using PlatformService.BridgeComponent.CustomException;
using PlatformService.BridgeComponent.Domain.Entities;
using PlatformService.BridgeComponent.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiBan.Common.Pages;

namespace Clear.AccountManage.Application
{
    public class AccountAppService : ApplicationService, IAccountAppService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRepository<UserBase, Guid> _userRepository;
        public AccountAppService(IAccountRepository accountRepository, IRepository<UserBase, Guid> userRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }
        public void Refund(AccountDeductionFeeInput input)
        {
            var account = _accountRepository.Get(input.AccountId);
            account.Refund(input.PayType,input.Amount, input.CapitalType);
            _accountRepository.Update(account);
        }

        public void AccountRecharge(AccountRechargeInput input)
        {
            var account = _accountRepository.Get(input.AccountId);
            account.Recharge(input.Amount, input.PayType, input.CapitalType);
            _accountRepository.Update(account);
        }


        public string CreateAccount(CreateAccountInput input)
        {
            var account = input.MapTo<Account>();
            account.Id = _accountRepository.GenerateIdentify();
            if(input.CreateCards != null)
            {
                foreach (var card in input.CreateCards)
                {
                    account.BindingCard(card.CardType, card.CardNo, card.IsPasswordAuth);
                }
            }
            return _accountRepository.InsertAndGetId(account);
        }

        public void FreezeAccount(string accountId)
        {
            var account = _accountRepository.Get(accountId);
            account.Freeze();
            _accountRepository.Update(account);
        }

        public AccountDto GetAccountByCard(string cardType, string cardNo)
        {
            if (cardType.IsNullOrEmpty())
            {
                cardType = CardType.Default.Key;
            }
            var account = _accountRepository.GetAccountByCard(cardType, cardNo);
            if (account== null)
            {
                string str = $"卡类型【{cardType}】+";
                if (!string.IsNullOrWhiteSpace(cardType))
                    str = "";
                throw new CustomHttpException($"{str}卡号【{cardNo}】找不到对应的账户");
            }
            var accountDto = account.MapTo<AccountDto>();
            accountDto.CreatorName = _userRepository.FirstOrDefault(accountDto.Creator)?.UserName;
            return accountDto;
        }

        public PagerResult<AccountDto> GetAccounts(GetAccountsInput  input)
        {
            input.OrderBy = "Id";//强制使用Id排序，提高查询性能
            var account = _accountRepository.GetAll()
                .WhereIf(!input.IDCardNo.IsNullOrEmpty(), s => s.IDCardNo == input.IDCardNo)
                .WhereIf(!input.Name.IsNullOrEmpty(), s => s.Name.Contains(input.Name))
                .WhereIf((!input.CardType.IsNullOrEmpty() && !input.CardNo.IsNullOrEmpty()), s => s.Cards.Any(c=>c.CardType ==  input.CardType && c.CardNo.Contains(input.CardNo)))
                .WhereIf(input.CreatorId.HasValue, s => s.Creator == input.CreatorId.Value)
                .OrderByDescending(s=>s.Id).Page(input,s=> s.MapTo<AccountDto>());

            var userIds = account.Result.Where(s => s.Creator != null).Select(u => u.Creator).Distinct().ToArray();
            var users = _userRepository.GetAll().Where(s => userIds.Contains(s.Id)).ToList();
            account.Result.ForEach((item) => {
                item.CreatorName = users.FirstOrDefault(s => s.Id == item.Creator)?.UserName;
            });

            return account;
        }

    
        public void ThawAccount(string accountId)
        {
            var account = _accountRepository.Get(accountId);
            account.Thaw();
            _accountRepository.Update(account);
        }

        public void UpdateAccount(UpdateAccountInput input)
        {
            var account = input.MapTo(_accountRepository.Get(input.Id));
            if (input.UpdateCards != null)
            {
                foreach (var cardDto in input.UpdateCards)
                {
                    var card = account.GetOrDefaultCard(cardDto.CardType);
                    if (card == null)
                    {
                        account.BindingCard(cardDto.CardType, cardDto.CardNo, cardDto.IsPasswordAuth);
                    }
                    else
                    {
                        card.IsPasswordAuth = cardDto.IsPasswordAuth;
                    }
                }
            }
           
            _accountRepository.Update(account);
        }
    }
}
