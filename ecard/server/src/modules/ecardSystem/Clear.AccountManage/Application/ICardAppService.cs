using PlatformService.BridgeComponent.WebApiConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Application
{
    /// <summary>
    /// 卡管理
    /// </summary>
    public interface ICardAppService: IOpenWebApi
    {
        /// <summary>
        /// 挂失
        /// </summary>
        /// <param name="input"></param>
        void Loss(CardOperationDto input);

        /// <summary>
        /// 解除挂失
        /// </summary>
        /// <param name="input"></param>
        void ReleaseLoss(CardOperationDto input);

        /// <summary>
        /// 获取卡信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        YiBan.Common.Pages.PagerResult<GetCardsOutput> GetCards(GetCardsInput input);

        /// <summary>
        /// 绑定卡
        /// </summary>
        /// <param name="input"></param>
        void BindingCard(BindingCardInput input);

        /// <summary>
        /// 注销卡
        /// </summary>
        /// <param name="input"></param>
        void CancellationCard(CardOperationDto input);

        /// <summary>
        /// 补换卡
        /// </summary>
        /// <param name="input"></param>
        void ReplaceCard(ReplaceCardInput input);
    }
}
