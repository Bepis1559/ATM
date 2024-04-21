using ATM.Bank.Interfaces;
using ATM.User.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Commands
{
    public interface ICommandReceiver
    {
        void CheckBalance();
        void TransferMoney();
        void WithdrawMoney();
    }
}
