using ATM.Bank.FeeCalculations;
using ATM.Bank.Interfaces;
using ATM.Bank.WithdrawCalculation;
using ATM.Helpers.interfaces;
using ATM.User.interfaces;
using ATM.User.Interfaces;
using ATM.User.userHandlers;
using ATM.User.UserTypes;
using System;

namespace ATM.Bank
{
    internal class ATM_Device(ILogger logger) : IATM
    {


        private readonly ILogger _logger = logger;

        public decimal CheckBalance(IUser user)
        {
            decimal balance = user.MoneyInAccount;
            //_logger.LogInfo($"{user.Name} has {balance}$ in their account .");
            _logger.LogInfo($"You have {balance}$ in your account .");
            return balance;
        }


        public void TransferMoney(IUser sender, IUser receiver, decimal amount)
        {
            HandleNegativeAmount(amount);
            AreFundsSufficient(sender.MoneyInAccount, amount);
            sender.MoneyInAccount -= amount;
            receiver.MoneyInAccount += amount;
            _logger.LogInfo($"{sender.Name} sent {amount}$ to {receiver.Name} .");
        }

        public decimal WithdrawMoney(IUser user, decimal amount, IStrategyRetriever strategyRetriever, IObserver userTypeObserver)
        {
            HandleNegativeAmount(amount);
            decimal withdrawAmountAfterFees = GetWithdrawalAmountAfterFees(strategyRetriever, amount);
            AreFundsSufficient(user.MoneyInAccount, withdrawAmountAfterFees);
            userTypeObserver.SubscribeUser(user, $"{user.Name} is now subscribed to the withdrawals count .", 30, 30);
            user.MoneyInAccount -= withdrawAmountAfterFees;
            _logger.LogInfo($"{user.Name} has withdrawn {amount} $ . Amount deducted from {user.Name}'s account after fees : {withdrawAmountAfterFees} ");
            AddWithdrawal(user);
            return withdrawAmountAfterFees;
        }



        private static decimal GetWithdrawalAmountAfterFees(IStrategyRetriever strategyRetriever, decimal amount)
        {
            BaseWithdrawCalculationStrategy strategy = strategyRetriever.GetStrategy(amount);
            decimal withdrwalAmountAfterFees = strategy.CalculateAmountToWithdraw(amount);
            return withdrwalAmountAfterFees;
        }

        private void AddWithdrawal(IUser user)
        {
            user.MonthlyWithdrawalsCount++;
            _logger.LogInfo($"{user.Name} current monthly withdrawals are : {user.MonthlyWithdrawalsCount}");
        }

        private static void HandleNegativeAmount(decimal amount)
        {
            if (amount <= 0)
            {
                throw new InvalidOperationException("Please enter a positive amount , got : " + amount);
            }
        }

        private static void AreFundsSufficient(decimal moneyInAccount, decimal amountToWithdraw)
        {
            if (moneyInAccount < amountToWithdraw) throw new InvalidOperationException($"Insufficient funds . Money in account : {moneyInAccount} . Amount to be deducted from account : {amountToWithdraw} ");
        }


    }
}
