using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank
{
    internal interface IATMOperations
    {
        decimal WithdrawMoney(decimal amount);
        double CheckBalance();
        void TransferMoney(decimal amount);
    }
}
