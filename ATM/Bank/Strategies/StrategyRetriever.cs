using ATM.Bank.FeeCalculations;
using ATM.Bank.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.WithdrawCalculation
{
    internal class StrategyRetriever : IStrategyRetriever
    {
        public BaseWithdrawCalculationStrategy GetStrategy(decimal amount)
        {
            if (amount < 0) throw new ArgumentException("Amount is less than 0 in GetStrategy method , StrategyRetriever class , got : " + amount);
            if (amount <= 100) return new UpTo100WithdrawCalculationStrategy();
            if(amount <= 1000) return new UpTo1000WithdrawCalculationStrategy();
            return new Over1000WithdrawCalculationStrategy();
        }
    }
}
