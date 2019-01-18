using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayTicketProblem.Library.ServiceDefines
{

    /// <summary>
    /// 你可以自由修改这个实现类, 不用在乎这些测试代码
    /// </summary>
    public class CardService : ICardService
    {
        public void Charge(UserCard userCard, decimal amount)
        {
            userCard.Balance += amount;
        }

        public void Entry(UserCard userCard, Station entryStation)
        {
        }

        public decimal GoOut(UserCard userCard, Station outStation)
        {
            return 10;
        }
    }
}
