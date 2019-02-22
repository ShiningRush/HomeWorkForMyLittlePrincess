using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.AccountManage.Domain.Accounts
{
    public interface IAccountRepository: IRepository<Account, string>
    {
        /// <summary>
        /// 生成唯一标识
        /// </summary>
        /// <returns></returns>
        string GenerateIdentify();

        /// <summary>
        /// 根据卡类型、卡号查找账户
        /// </summary>
        /// <param name="cardType"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        Account GetAccountByCard(string cardType,string cardNo);
    }
}
