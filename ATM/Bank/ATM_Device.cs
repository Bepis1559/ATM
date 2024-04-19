using ATM.Bank.FeeCalculations;
using ATM.Bank.Interfaces;
using ATM.Bank.WithdrawCalculation;
using ATM.Helpers.interfaces;
using ATM.User.Interfaces;
using ATM.User.UserTypes;

namespace ATM.Bank
{
    internal class ATM_Device(IWithdrawalsObserver withdrawsObserver, ILogger logger)
    {
        private readonly IWithdrawalsObserver _withdrawsObserver = withdrawsObserver;

        private readonly ILogger _logger = logger;
        public decimal CheckBalance(BaseUser user)
        {
            decimal balance = user.MoneyInAccount;
            _logger.LogInfo($"${user.Name} has {balance} $ in their account .");
            return balance;
        }


        public void TransferMoney(BaseUser sender, BaseUser receiver, decimal amount)
        {
            HandleNegativeAmount(amount);
            AreFundsSufficient(sender.MoneyInAccount, amount);
            sender.MoneyInAccount -= amount;
            receiver.MoneyInAccount += amount;
        }

        public decimal WithdrawMoney(BaseUser user, decimal amount, IStrategyRetriever strategyRetriever)
        {
            HandleNegativeAmount(amount);
            BaseWithdrawCalculationStrategy strategy = strategyRetriever.GetStrategy(amount);
            decimal withdrawAmountAfterFees = strategy.CalculateAmountToWithdraw(amount);
            AreFundsSufficient(user.MoneyInAccount, withdrawAmountAfterFees);
            user.MoneyInAccount -= withdrawAmountAfterFees;
            _withdrawsObserver.AddWithdrawal(user);
            _logger.LogInfo($"{user.Name} has withdrawn {amount} $ .");
            return withdrawAmountAfterFees;
        }



        private static void HandleNegativeAmount(decimal amount)
        {
            if (amount < 0) throw new ArgumentException("Amount can't be less than 0 , got : " + amount + " in HandleNegativeAmount method. ");
        }

        private static void AreFundsSufficient(decimal moneyInAccount, decimal amountToWithdraw)
        {
            if (moneyInAccount < amountToWithdraw) throw new InvalidOperationException($"Insufficient funds . Money in account : {moneyInAccount} . Amount to be deducted from account : {amountToWithdraw} ");
        }


    }
}
