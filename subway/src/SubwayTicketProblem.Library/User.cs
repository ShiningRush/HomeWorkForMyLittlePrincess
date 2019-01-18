using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayTicketProblem.Library
{
    public class UserCard
    {
        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// 用户入站口
        /// </summary>
        public Station EntryStation { get; set; }
    }
}
