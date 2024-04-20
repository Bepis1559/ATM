using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.User.interfaces
{
    internal interface IObserver
    {
        void SubscribeUser(IUser user, string subscriptionMessage, int initialDelay, int period);

        void UnSubscribeUser(IUser user, string subscriptionMessage);
    }
}
