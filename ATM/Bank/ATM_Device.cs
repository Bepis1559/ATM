using ATM.Bank.FeeCalculations;
using ATM.Bank.Interfaces;
using ATM.Bank.WithdrawCalculation;
using ATM.Helpers.interfaces;
using ATM.User.interfaces;
using ATM.User.Interfaces;
using ATM.User.userHandlers;
using ATM.User.UserTypes;

namespace ATM.Bank
{
    internal class ATM_Device(ILogger logger)
    {
        

        private readonly ILogger _logger = logger;

        public decimal CheckBalance(IUser user)
        {
            decimal balance = user.MoneyInAccount;
            _logger.LogInfo($"${user.Name} has {balance} $ in their account .");
            return balance;
        }


        public void TransferMoney(IUser sender, IUser receiver, decimal amount)
        {
            HandleNegativeAmount(amount);
            AreFundsSufficient(sender.MoneyInAccount, amount);
            sender.MoneyInAccount -= amount;
            receiver.MoneyInAccount += amount;
            _logger.LogInfo($"{sender.Name} sent ${amount} to {receiver.Name} .");
        }

        public decimal WithdrawMoney(IUser user, decimal amount, IStrategyRetriever strategyRetriever, IUserTypeObserver userTypeObserver)
        {
            HandleNegativeAmount(amount);
            BaseWithdrawCalculationStrategy strategy = strategyRetriever.GetStrategy(amount);
            decimal withdrawAmountAfterFees = strategy.CalculateAmountToWithdraw(amount);
            AreFundsSufficient(user.MoneyInAccount, withdrawAmountAfterFees);
            userTypeObserver.SubscribeUser(user);
            user.MoneyInAccount -= withdrawAmountAfterFees;
            AddWithdrawal(user);
            return withdrawAmountAfterFees;
        }



        private void AddWithdrawal(IUser user)
        {
            user.MonthlyWithdrawalsCount++;
            _logger.LogInfo($"{user.Name} has made a withdrawal . His current monthly withdrawals are : {user.MonthlyWithdrawalsCount}");
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
