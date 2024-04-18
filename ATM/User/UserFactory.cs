
using ATM.User.Interfaces;

namespace ATM.User
{
    internal class UserFactory : IUserFactory
    {
        public BaseUser CreateUser(UserType userType, string userName, double moneyInAccount)
        {
            return userType switch
            {
                UserType.Standard => new StandardUser(userName, moneyInAccount),
                UserType.Premium => new PremiumUser(userName, moneyInAccount),
                UserType.Platinum => new PlatinumUser(userName, moneyInAccount),
                _ => throw new NotSupportedException($"{userType} is not currently supported as a payment method."),
            };
        }
    }
}
