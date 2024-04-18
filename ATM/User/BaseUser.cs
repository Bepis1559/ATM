
namespace ATM.User
{
    internal abstract class BaseUser(string name, double moneyInAccount)
    {
        public string Id { get; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = name;
        public double MoneyInAccount { get; set; } = moneyInAccount;
    }
}
