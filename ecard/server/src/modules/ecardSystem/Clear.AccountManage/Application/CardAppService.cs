using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Clear.AccountManage.Domain.Accounts;
using Clear.AccountManage.Domain.Accounts.DataItem;
using PlatformService.BridgeComponent.CustomException;
using PlatformService.BridgeComponent.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiBan.Common.Pages;
using YiBan.Common.Extensions;

namespace Clear.AccountManage.Application
{
    public class CardAppService : ApplicationService, ICardAppService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRepository<Card, Guid> _cardRepository;

        public CardAppService(IAccountRepository accountRepository, IRepository<Card, Guid> cardRepository)
        {
            _accountRepository = accountRepository;
            _cardRepository = cardRepository;
        }

        public PagerResult<GetCardsOutput> GetCards(GetCardsInput input)
        {
            //if( input.CardType.IsNullOrEmpty())
            //{
            //    input.CardType = CardType.Default.Key;
            //}
            input.OrderBy = "AccountId";
            var pagerWrapper = new PagerResult<GetCardsOutput>();
            var table = from a in _cardRepository.GetAll()
                        join b in _accountRepository.GetAll()
                        on a.AccountId equals b.Id into temp
                        from B in temp.DefaultIfEmpty()
                        select new GetCardsOutput
                        {
                            AccountId = a.AccountId,
                            CardNo = a.CardNo,
                            CardType = a.CardType,
                            IsPasswordAuth = a.IsPasswordAuth,
                            Status = a.Status,
                            AccountName = B == null ? "": B.Name,
                            AccountIDCardNo = B == null ? "" : B.IDCardNo,
                            IsOnlyCancellation = a.CardType != CardType.Default.Key
                        };
      
            var result = table
              .WhereIf(!input.Name.IsNullOrEmpty(), a => a.AccountName.StartsWith(input.Name))
              .WhereIf(!input.CardType.IsNullOrEmpty(), c => c.CardType == input.CardType)
              .WhereIf(!input.CardNo.IsNullOrEmpty(), c => c.CardNo.StartsWith(input.CardNo))
              .Page(input, pagerWrapper)
              .ToList();
            pagerWrapper.Result = result;
            return pagerWrapper;
           
        }

        public void Loss(CardOperationDto input)
        {
            var account = _accountRepository.Get(input.AccountId);
            account.GetCard(input.CardType).Loss(input.Reason);
            _accountRepository.Update(account);
        }

        public void ReleaseLoss(CardOperationDto input)
        {
            var account = _accountRepository.Get(input.AccountId);
            account.GetCard(input.CardType).ReleaseLoss(input.Reason);
            _accountRepository.Update(account);
        }


        public void BindingCard(BindingCardInput input)
        {
            var account = _accountRepository.Get(input.AccountId);
            account.BindingCard(input.CardType, input.CardNo);
            _accountRepository.Update(account);
        }

        public void CancellationCard(CardOperationDto input)
        {
            var account = _accountRepository.Get(input.AccountId);
            account.CancellationCard(input.CardType, input.Reason);
            _accountRepository.Update(account);
        }
        public void ReplaceCard(ReplaceCardInput input)
        {
            var account = _accountRepository.Get(input.AccountId);
            account.ReplaceCard(input.CardType, input.OldCardNo, input.NewCardNo, input.Reason);
            _accountRepository.Update(account);
        }

    }
}
