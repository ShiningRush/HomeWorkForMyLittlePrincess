using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Clear.AccountManage.Domain.Accounts;
using Clear.AccountManage.Domain.Accounts.DataItem;
using Clear.AccountManage.Domain.Blacklist;
using PlatformService.BridgeComponent.CustomException;
using System;
using System.Collections.Generic;
using System.Linq;
using YiBan.Common.Extensions;
using Abp.Linq.Extensions;
using YiBan.Common.Pages;

namespace Clear.AccountManage.Application
{
    public class BlacklistAppService:ApplicationService, IBlacklistAppService
    {
        private readonly IRepository<Blacklist, Guid> _blacklistRepo;
        private readonly IAccountRepository _accountRepo;

        public BlacklistAppService(
            IRepository<Blacklist, Guid> blacklistRepo,
            IAccountRepository accountRepo)
        {
            _blacklistRepo = blacklistRepo;
            _accountRepo = accountRepo;
        }
        /// <summary>
        /// 获取黑白名单（分页）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public YiBan.Common.Pages.PagerResult<OutputGetBlacklistDto> GetBlacklist(InputGetBlacklistDto input)
        {
            var pagerWrapper = new PagerResult<OutputGetBlacklistDto>();
            var table = from black in _blacklistRepo.GetAll()
                        join card in _accountRepo.GetAll().SelectMany(cs => cs.Cards)
                        on black.CardId equals card.Id into cards
                         from Card in cards.DefaultIfEmpty()
                        join account in _accountRepo.GetAll()
                        on Card.AccountId equals account.Id 
                        select new OutputGetBlacklistDto
                        {
                            Id = black.Id,
                            BeginValidDate = black.BeginValidDate,
                            EndValidDate = black.EndValidDate,
                            IDCardNo = account.IDCardNo,
                            Remark = black.Remark,
                            Type = black.Type,
                            Name = account.Name,
                            CardNo = Card.CardNo,
                            CreateTime = black.CreateTime,
                            CardStatus = Card.Status == CardStatus.Normal ? "正常":"挂失",
                        };
            var result = table
                .WhereIf(!input.Name.IsNullOrEmpty(), p => p.Name.Contains(input.Name))
                .WhereIf(!input.Type.IsNullOrEmpty(), p => p.Type == input.Type)
                .WhereIf(!input.CardNo.IsNullOrEmpty(), p => p.CardNo.Contains(input.CardNo))
                .WhereIf(!input.IDCardNo.IsNullOrEmpty(), p => p.IDCardNo.Contains(input.IDCardNo))
                .WhereIf(input.BeginTime.HasValue && input.DateType ==0, p => p.BeginValidDate>= input.BeginTime.Value)
                .WhereIf(input.EndTime.HasValue && input.DateType == 0, p => p.BeginValidDate <= input.EndTime.Value)
                .WhereIf(input.BeginTime.HasValue && input.DateType == 1, p => p.EndValidDate >= input.BeginTime.Value)
                .WhereIf(input.EndTime.HasValue && input.DateType == 1, p => p.EndValidDate <= input.EndTime.Value)
                .Page(input, pagerWrapper)
                .ToList();
            pagerWrapper.Result = result;
            return pagerWrapper;
        }
        /// <summary>
        /// 添加黑白名单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string AddBlacklist(InputAddBlacklistDto input)
        {
            var card = _accountRepo.GetAll().SelectMany(cs => cs.Cards).FirstOrDefault(s => s.CardNo == input.CardNo && s.CardType == CardType.Default.Key);
            if (card == null)
            {
                throw new CustomHttpException("没有找到该卡号！");
            }
            var insertEntity = input.MapTo<Blacklist>();
            insertEntity.Id = Guid.NewGuid();
            insertEntity.CardId = card.Id;
            insertEntity.Card = card;
            _blacklistRepo.Insert(insertEntity);
            return insertEntity.Id.ToString();
        }
        /// <summary>
        /// 修改黑白名单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void UpdateBlacklist(InputUpdateBlacklistDto input)
        {
            var card = _accountRepo.GetAll().SelectMany(cs => cs.Cards).FirstOrDefault(s => s.CardNo == input.CardNo && s.CardType == CardType.Default.Key);
            if (card == null)
            {
                throw new CustomHttpException("没有找到该卡号！");
            }
            var oldEntity = _blacklistRepo.Get(input.Id);
            
            var newEntity = input.MapTo(oldEntity);
            newEntity.CardId = card.Id;
            newEntity.Card = card;
            _blacklistRepo.Update(newEntity);
        }
        /// <summary>
        /// 删除黑白名单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void DeleteBlacklist(Guid id)
        {
            _blacklistRepo.Delete(id);
        }
    }
}
