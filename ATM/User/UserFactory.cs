using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.User
{
    internal class UserFactory
    {
        public static BaseUser CreateUser(UserType userType, string userName, double moneyInAccount)
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
