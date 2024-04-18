
namespace ATM.User
{
    internal abstract class BaseUser
    {
        public string Id { get; } = Guid.NewGuid().ToString();
        public string Name { get; set; }

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

        protected BaseUser(string name, decimal moneyInAccount)
        {
            Name = name;
            MoneyInAccount = moneyInAccount;
        }
    }
}
