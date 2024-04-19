using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.User.UserTypes;

namespace ATM.User.Interfaces
{
    internal interface IUserFactory
    {
        BaseUser CreateUser(UserType userType, string userName, decimal moneyInAccount);
    }
}
