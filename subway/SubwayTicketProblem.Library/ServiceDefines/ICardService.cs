using SubwayTicketProblem.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayTicketProblem.Library.ServiceDefines
{
    public interface ICardService
    {
        void Charge(UserCard userCard, decimal amount);
        void Entry(UserCard userCard, Station entryStation);
        decimal GoOut(UserCard userCard, Station outStation);
    }
}
