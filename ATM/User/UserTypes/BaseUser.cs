using ATM.User.interfaces;

namespace ATM.User.UserTypes
{
    internal class BaseUser : IUser
    {
        public string Id { get; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public UserType UserType { get; set; }
        public int MonthlyWithdrawalsCount { get; set; } = 0;

        private decimal _moneyInAccount;
        public decimal MoneyInAccount
        {
            get => _moneyInAccount;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Money in account cannot be negative.");
                }
                _moneyInAccount = value;
            }
        }

        public BaseUser(string name, decimal moneyInAccount,UserType userType)
        {
            Name = name;
            MoneyInAccount = moneyInAccount;
            UserType = userType;
            
        }
    }
}
