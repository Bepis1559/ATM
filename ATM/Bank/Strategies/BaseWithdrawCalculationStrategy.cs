using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.FeeCalculations
{
    public abstract class BaseWithdrawCalculationStrategy
    {
        protected virtual decimal Fee {get;set;} = 0.03m;
        public decimal CalculateAmountToWithdraw(decimal amount) => amount + amount * Fee;
    }
}
