using ATM.User.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.User.userHandlers
{
    internal abstract class BaseObserver : IObserver
    {
        protected readonly Dictionary<IUser, DateTime> _subscriptionStartDate = [];

        protected Timer? _timer;


        public void SubscribeUser(IUser user, string subscriptionMessage, int initialDelay, int period)
        {
            if (!_subscriptionStartDate.ContainsKey(user))
            {
                _subscriptionStartDate[user] = DateTime.Now;
                LogSubstrictionMessage(subscriptionMessage);
                ScheduleUpdate(user, initialDelay, period);
            }
        }

        protected abstract void ScheduleUpdate(IUser user, int initialDelay, int period);

        protected abstract void LogSubstrictionMessage(string message);
    }
}
