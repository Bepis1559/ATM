using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank
{
    internal interface IBankOperations
    {
        void WithdrawMoney(double amount);
        double CheckBalance();
        void TransferMoney(double amount);
    }
}
