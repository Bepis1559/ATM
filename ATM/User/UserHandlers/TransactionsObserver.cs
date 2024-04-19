using ATM.User.UserTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.User.UserHandlers
{
    internal class TransactionsObserver
    {
        private readonly Dictionary<BaseUser,int> monthlyTransactionsCount = [];
    }
}
