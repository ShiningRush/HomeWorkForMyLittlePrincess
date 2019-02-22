using Clear.AccountManage.Domain.Accounts.DataItem;
using PlatformService.BridgeComponent.CustomException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Domain.Accounts
{
    /// <summary>
    /// 账户行为
    /// </summary>
    public partial class Account
    {

        /// <summary>
        /// 冻结账户
        /// </summary>
        public void Freeze()
        {
            if (Status == AccountStatus.Normal)
            {
                Status = AccountStatus.Freeze;
            }
        }

        /// <summary>
        /// 解冻账户
        /// </summary>
        public void Thaw()
        {
            if (Status == AccountStatus.Freeze)
            {
                Status = AccountStatus.Normal;
            }
        }

        /// <summary>
        ///  充值（充值指定类型的资金费用，如果没有该类型资金则创建该类型资金并充值）
        /// </summary>
        /// <param name="amount">金额</param>
        /// <param name="payType">支付类型</param>
        /// <param name="capitalType">资金类型</param>
        /// <param name="desc"></param>
        public void Recharge(decimal amount, string payType, string capitalType)
        {
            var capital = Capitals.SingleOrDefault(s => s.CapitalType == capitalType);
            if (capital == null)
            {
                capital = new Capital(Id, capitalType);
                Capitals.Add(capital);
            }
            capital.ChangeBalance(amount,BillingType.Recharge, payType);
        }

        /// <summary>
        /// 扣费（扣除指定类型的资金费用，如果没有该资金类型则扣除通用资金里面的钱）
        /// </summary>
        /// <param name="amount">金额</param>
        /// <param name="desc">消费描述</param>
        /// <param name="capitalType">资金类型</param>
        public void DeductionFee(decimal amount, string desc, string capitalType)
        {
            var capital = GetCapital(capitalType);
            capital.ChangeBalance(amount * -1,BillingType.Consumption, null, desc);
        }

        private Capital GetCapital(string capitalType)
        {
            var capital = Capitals.SingleOrDefault(s => s.CapitalType == capitalType);
            if (capital == null)
            {
                capital = GetGeneralCapital();
            }

            if (capital == null)
            {
                throw new CustomHttpException("没有资金，扣费失败");
            }

            return capital;
        }

        /// <summary>
        /// 退费
        /// </summary>
        /// <param name="payType">支付方式</param>
        /// <param name="amount">金额</param>
        /// <param name="capitalType">资金类型</param>
        public void Refund(string payType, decimal amount, string capitalType)
        {
            var capital = GetCapital(capitalType);
            if(capital.Balance == 0)
            {
                throw new CustomHttpException("没有余额无需退费");
            }
            capital.ChangeBalance(amount * -1, BillingType.Refund, payType, "退费");
        }

        /// <summary>
        /// 绑定卡（只能绑定没有绑定过的卡类型）
        /// </summary>
        /// <param name="cardType"></param>
        /// <param name="cardNo"></param>
        /// <param name="isPasswordAuth"></param>
        public void BindingCard(string cardType, string cardNo, bool isPasswordAuth = false)
        {
            var card = GetOrDefaultCard(cardType);
            if (card != null)
            {
                throw new CustomHttpException($"该账户已经绑定过卡类型【{cardType}】");
            }
            card = new Card(Id, cardType, cardNo, isPasswordAuth);
            Cards.Add(card);
            DomainEvents.Add(new OnCardChanged(card, CardOperationType.BindingCard));
        }

        /// <summary>
        /// 注销卡（只能注销已经绑定过的卡类型）
        /// </summary>
        /// <param name="cardType"></param>
        /// <param name="reason"></param>
        public void CancellationCard(string cardType,string reason)
        {
            var card = GetCard(cardType);
            Cards.Remove(card);
            DomainEvents.Add(new OnCardChanged(card, CardOperationType.CancellationCard, reason));
        }

        /// <summary>
        /// 更换卡（只能更换已经绑定过的卡类型）
        /// </summary>
        /// <param name="cardType"></param>
        /// <param name="oldCardNo"></param>
        /// <param name="newCardNo"></param>
        public void ReplaceCard(string cardType, string oldCardNo, string newCardNo, string reason)
        {
            CancellationCard(cardType, reason);
            BindingCard(cardType, newCardNo);
        }

        /// <summary>
        /// 获取余额
        /// </summary>
        /// <param name="capitalType"></param>
        /// <returns></returns>
        public decimal GetBalance(string capitalType = null)
        {
            decimal balance = 0;

            var generalCapital = GetGeneralCapital();
            var capital = Capitals.SingleOrDefault(s => s.CapitalType == capitalType);

            balance += generalCapital == null ? 0 : generalCapital.Balance;
            balance += capital == null ? 0 : capital.Balance;

            return balance;
        }

        /// <summary>
        /// 获取通用资金
        /// </summary>
        /// <returns></returns>
        private Capital GetGeneralCapital()
        {
            return Capitals.SingleOrDefault(s => s.CapitalType == null);
        }

        /// <summary>
        /// 获取指定类型的卡
        /// </summary>
        /// <param name="cardType"></param>
        /// <returns></returns>
        public Card GetOrDefaultCard(string cardType)
        {
            return Cards.SingleOrDefault(s => s.CardType == cardType);
        }

        /// <summary>
        /// 获取指定类型的卡
        /// </summary>
        /// <param name="cardType"></param>
        /// <returns></returns>
        public Card GetCard(string cardType)
        {
            var card = Cards.SingleOrDefault(s => s.CardType == cardType);
            if(card == null)
            {
                throw new CustomHttpException($"该账户不存在卡类型【{cardType}】");
            }
            return card;
        }
    }
}
