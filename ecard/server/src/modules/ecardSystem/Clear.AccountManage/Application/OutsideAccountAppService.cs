using Abp.Application.Services;
using Abp.AutoMapper;
using Clear.AccountManage.Domain.Accounts;
using Clear.AccountManage.Domain.Accounts.DataItem;
using PlatformService.BridgeComponent.CustomException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Application
{
    public class OutsideAccountAppService : ApplicationService, IOutsideAccountAppService
    {
        private readonly IAccountRepository _accountRepository;
        public OutsideAccountAppService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void Recharge(OutsideRechargeInput input)
        {
            var account = GetAccount(input);
            account.Recharge(input.Amount, input.PayType,  input.CapitalType);
            _accountRepository.Update(account);
        }

        public string CreditCard(OutsideCreditCardInput input)
        {
            var account = input.MapTo<Account>();
            account.Id = _accountRepository.GenerateIdentify();
            account.BindingCard(CardType.Default.Key, input.CardNo);
            return _accountRepository.InsertAndGetId(account);
        }

        public void DeductionFee(OutsideDeductionFeeInput input)
        {
            var account = GetAccount(input);
            account.DeductionFee( input.Amount,input.Desc, input.CapitalType);
            _accountRepository.Update(account);
        }

        public OutsideAccountDto GetAccountByCard(OutsideCardInput input)
        {
            var account = GetAccount(input);
            return account.MapTo<OutsideAccountDto>();
        }

        public void Refund(OutsideRefundInput input)
        {
            var account = GetAccount(input);
            account.Refund(input.PayType, input.Amount, input.CapitalType);
            _accountRepository.Update(account);
        }

        private Account GetAccount(OutsideCardInput card)
        {
            var account = _accountRepository.GetAccountByCard(card.CardType, card.CardNo);
            if (account == null)
            {
                throw new CustomHttpException($"卡类型【{card.CardType}】+卡号【{card.CardNo}】找不到对应的账户");
            }
            return account;
        }
    }
}
