using ATM.Bank.FeeCalculations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank
{
    internal class ATM_Device : IATMOperations
    {
        private BaseWithdrawCalculationStrategy? _withdrawCalculationStrategy;

        public void SetWithdrawCalculationStrategy(BaseWithdrawCalculationStrategy strategy)
        {
            _withdrawCalculationStrategy = strategy;
        }
        public double CheckBalance()
        {
            throw new NotImplementedException();
        }

        public void TransferMoney(decimal amount)
        {
            throw new NotImplementedException();
        }

        public decimal WithdrawMoney(decimal amount)
        {
            if (_withdrawCalculationStrategy == null)
            {
                throw new InvalidOperationException("Fee calculation strategy is not set.");
            }

            decimal withdrawAmountAfterFees = _withdrawCalculationStrategy.CalculateAmountToWithdraw(amount);
            return withdrawAmountAfterFees;
        }
    }
}
