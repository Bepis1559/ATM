using ATM.User.UserTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.User.Interfaces
{
    internal interface IWithdrawalsObserver
    {
        void AddWithdrawal(BaseUser user);
        Dictionary<BaseUser, int> GetMonthlyWidrawals();
    }
}
