using ATM.User.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.User.userHandlers
{
    internal class TypeUpdater : ITypeUpdater
    {
        public void UpdateUserType(IUser user)
        {

            ArgumentNullException.ThrowIfNull(user);
            int withdrawalsCount = user.MonthlyWithdrawalsCount;
            ArgumentOutOfRangeException.ThrowIfNegative(withdrawalsCount);
            if (withdrawalsCount <= 10)
            {
                user.UserType = UserType.Standard;
            }
            else if (withdrawalsCount <= 20)
            {
                user.UserType = UserType.Premium;
            }
            else
            {
                user.UserType = UserType.Platinum;
            }
        }
    }
}
