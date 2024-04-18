using ATM.Bank.FeeCalculations;
using ATM.Bank.WithdrawCalculation;
using ATM.User;

namespace ATM.Bank
{
    internal class ATM_Device(List<BaseUser> allUsers) : IATMOperations
    {

        private readonly List<BaseUser> _allUsers = allUsers;


        public decimal CheckBalance(string userId)
        {
            BaseUser user = DoesUserExist(userId);
            return user.MoneyInAccount;

        }

        public void TransferMoney(string senderId, string receiverId, decimal amount)
        {

            if (senderId == receiverId) throw new ArgumentException($"Sender and receiver are the same , got : senderId  {senderId} and receiverId : {receiverId} ");
            HandleNegativeAmount(amount);
            BaseUser sender = DoesUserExist(senderId);
            BaseUser receiver = DoesUserExist(receiverId);
            AreFundsSufficient(sender.MoneyInAccount, amount);
            sender.MoneyInAccount -= amount;
            receiver.MoneyInAccount += amount;

        }

        public decimal WithdrawMoney(string userId, decimal amount, StrategyRetriever strategyRetriever)
        {
            BaseUser user = DoesUserExist(userId);
            HandleNegativeAmount(amount);
            BaseWithdrawCalculationStrategy strategy = strategyRetriever.GetStrategy(amount);
            decimal withdrawAmountAfterFees = strategy.CalculateAmountToWithdraw(amount);
            AreFundsSufficient(user.MoneyInAccount, withdrawAmountAfterFees);
            user.MoneyInAccount -= withdrawAmountAfterFees;
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

        private BaseUser DoesUserExist(string userId)
        {
            BaseUser? user = _allUsers.FirstOrDefault(user => user.Id == userId);
            return user is null ? throw new ArgumentException("No user with such id exists , got : " + userId) : user;
        }
    }
}
