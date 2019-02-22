using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Clear.AccountManage.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Extensions;
using YiBan.Common.Pages;
using YiBan.Common.Extensions;
using PlatformService.BridgeComponent.Domain.Entities;

namespace Clear.AccountManage.Application.Dtos
{
    public class CardLogAppService:ApplicationService, ICardLogAppService
    {
        private readonly IRepository<CardLog, Guid> _cardLogRepository;
        private readonly IRepository<Clear.AccountManage.Domain.Accounts.Account, string> _accountRepository;
        private readonly IRepository<UserBase, Guid> _userRepository;

        public CardLogAppService(IRepository<CardLog, Guid> cardLogRepository, IRepository<Clear.AccountManage.Domain.Accounts.Account, string> accountRepository
            , IRepository<UserBase, Guid> userRepository)
        {
            _cardLogRepository = cardLogRepository;
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }
        public YiBan.Common.Pages.PagerResult<OutputGetCardLogListDto> GetCardLogList(InputGetCardLogListDto input)
        {
            var pagerWrapper = new PagerResult<OutputGetCardLogListDto>();
            var table = from c in _cardLogRepository.GetAll()
                        join a in _accountRepository.GetAll()
                        on c.AccountId equals a.Id into Account
                        from O in Account.DefaultIfEmpty()
                        select new OutputGetCardLogListDto
                        {
                            Id = c.Id.ToString(),
                            IDCardType = O == null ? "" : O.IDCardType,
                            IDCardNo = O == null ? "" : O.IDCardNo,
                            Balance = O == null ? 0 : O.Capitals.Sum(s=>s.Balance),
                            OperationType = c.OperationType,
                            CardNo = c.CardNo,
                            CreateTime = c.CreateTime,
                            Creator = c.Creator,
                            Name = O == null ? "" : O.Name,
                            Remark = c.Remark
                        };
            var result = table
                .WhereIf(!input.CardNo.IsNullOrEmpty(), s => s.CardNo.Contains(input.CardNo))
                .WhereIf(!input.OperationType.IsNullOrEmpty(), s => s.OperationType == input.OperationType)
                .WhereIf(!input.Name.IsNullOrEmpty(), s => s.Name.Contains(input.Name))
                .WhereIf(!input.IDNO.IsNullOrEmpty(), s => s.IDCardNo.Contains(input.IDNO))
    .Page(input, pagerWrapper)
    .ToList();
            var userIds = result.Where(s => s.Creator != null).Select(u => u.Creator.Value).Distinct().ToArray();
            var users = _userRepository.GetAll().Where(s => userIds.Contains(s.Id)).ToList();
            result.ForEach((item) => {
                if (!item.Creator.HasValue) return;
                item.CreatorName = users.FirstOrDefault(s => s.Id == item.Creator)?.UserName;
            });

            pagerWrapper.Result = result;
            return pagerWrapper;
        }
    }
}
