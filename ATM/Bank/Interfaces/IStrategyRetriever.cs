using ATM.Bank.FeeCalculations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Interfaces
{
    public interface IStrategyRetriever
    {
        BaseWithdrawCalculationStrategy GetStrategy(decimal amount);
    }
}
