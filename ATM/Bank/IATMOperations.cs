using ATM.Bank.FeeCalculations;
using ATM.Bank.WithdrawCalculation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank
{
    internal interface IATMOperations
    {
        decimal WithdrawMoney(string userId, decimal amount, StrategyRetriever strategyRetriever);
        decimal CheckBalance(string userId);
        void TransferMoney(string senderId, string receiverId, decimal amount);
    }
}
