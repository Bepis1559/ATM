using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.User.interfaces
{
    internal interface IUserTypeObserver
    {
        void SubscribeUser(IUser user);
    }
}
