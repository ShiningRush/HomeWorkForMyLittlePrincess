using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Clear.Settlement.Domain.OperatorAggregate;
using PlatformService.BridgeComponent.Domain.Sequence;
using PlatformService.BridgeComponent.Domain.Sequence.Algorithm;
using ServiceAnt.Handler;
using ServiceAnt.Handler.Subscription.Handler;
using ServiceAnt.Subscription;
using ServiceAnt.Subscription.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.Settlement.EventHandler
{
    /// <summary>
    /// 
    /// </summary>
    public class OnCapitalBalanceChanged : IEventTrigger
    {
        /// <summary>
        /// 账号代码
        /// </summary> 
        public string AccountId { get; set; }

        /// <summary>
        /// 资金类型 对应数据字典表 CapitalType 
        /// </summary> 
        public string CapitalType { get; set; }
        /// <summary>
        /// 金额
        /// </summary> 
        public decimal Balance { get; set; }

        /// <summary>
        /// 变更金额
        /// </summary>
        public decimal ChangedAmount { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public string PayType { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 消费类型
        /// </summary>
        public string BillingType { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }

        public OnCapitalBalanceChanged() { }
    }

    public class OnCapitalBalanceChangedHandler : IEventHandler<OnCapitalBalanceChanged>, ITransientDependency
    {
        private readonly IRepository<BillingRecord,long> _billingRecordRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ISequenceGenerator _sequenceGenerator;

        public OnCapitalBalanceChangedHandler(IRepository<BillingRecord, long> billingRecordRepository, IUnitOfWorkManager unitOfWorkManager
            , ISequenceGenerator sequenceGenerator)
        {
            _billingRecordRepository = billingRecordRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _sequenceGenerator = sequenceGenerator;
        }

        public Task HandleAsync(OnCapitalBalanceChanged param)
        {

            BillingRecord billingRecord = new BillingRecord(
                   _sequenceGenerator.Next("BR", new DateTimeSequenceAlgorithm()),
                   param.AccountId,
                   param.CapitalType,
                   param.ChangedAmount,
                   param.PayType,
                   param.Desc,
                   param.CardNo,
                   param.BillingType,
                   param.Balance);
            _billingRecordRepository.Insert(billingRecord);
            return Task.FromResult(0);
        }
    }
}
