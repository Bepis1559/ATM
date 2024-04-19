using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.User.interfaces
{
    internal interface IUser
    {
        string Id { get; }
        string Name { get; set; }
        UserType UserType { get; set; }
        decimal MoneyInAccount { get; set; }
        int MonthlyWithdrawalsCount { get; set; }

    }
}
