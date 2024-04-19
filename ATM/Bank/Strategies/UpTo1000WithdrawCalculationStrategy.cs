using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.FeeCalculations
{
    internal class UpTo1000WithdrawCalculationStrategy : BaseWithdrawCalculationStrategy
    {
        protected override decimal Fee { get; set; } = 0.05m ;
      
    }
}
