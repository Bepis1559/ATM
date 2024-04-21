using ATM.Bank.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.User.interfaces
{
    public interface IATM
    {
        decimal CheckBalance(IUser user);
        void TransferMoney(IUser sender, IUser receiver, decimal amount);
        decimal WithdrawMoney(IUser user, decimal amount, IStrategyRetriever strategyRetriever, IObserver userTypeObserver);
    }
}
