using ATM.User.Interfaces;
using ATM.User.UserTypes;

namespace ATM.User.UserHandlers
{
    internal class UserFactory : IUserFactory
    {
        public BaseUser CreateUser(UserType userType, string userName, decimal moneyInAccount)
        {
            return userType switch
            {
                UserType.Standard => new BaseUser(userName, moneyInAccount,UserType.Standard),
                UserType.Premium => new BaseUser(userName, moneyInAccount, UserType.Premium),
                UserType.Platinum => new BaseUser(userName, moneyInAccount,UserType.Platinum),
                _ => throw new NotSupportedException($"{userType} is not currently supported as a payment method."),
            };
        }
    }
}
