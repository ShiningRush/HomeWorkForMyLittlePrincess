using Abp.Domain.Repositories;
using PlatformService.BridgeComponent.Service;
using ServiceAnt.Handler.Subscription.Handler;
using ServiceAnt.Subscription.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Domain.Accounts
{
    /// <summary>
    /// 处理卡更改记录卡务日志
    /// </summary>
    public class OnCardChangedHandler : IEventHandler<OnCardChanged>,Abp.Dependency.ITransientDependency
    {
        private readonly IRepository<CardLog, Guid> _cardLogRepository;
        private readonly IServiceContext _serviceContext;

        public OnCardChangedHandler(IRepository<CardLog, Guid> cardLogRepository, IServiceContext serviceContext)
        {
            _cardLogRepository = cardLogRepository;
            _serviceContext = serviceContext ;
        }
        
        public virtual Task HandleAsync(OnCardChanged param)
        {
            var cardLog = new CardLog() {
                AccountId = param.AccountId,
                CardNo = param.CardNo,
                OperationType = param.CardOperationType,
                Remark = param.Remark,
                AppId = _serviceContext.ClientID,
            };
            _cardLogRepository.Insert(cardLog);
            return Task.FromResult(0);
        }
    }
}
