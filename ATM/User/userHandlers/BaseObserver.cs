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

        protected readonly Dictionary<IUser, Timer> _userTimers = [];

        protected Timer? _timer;


        public void SubscribeUser(IUser user, string subscriptionMessage, int initialDelay, int period)
        {
            if (!_subscriptionStartDate.ContainsKey(user) && !_userTimers.ContainsKey(user))
            {
                _subscriptionStartDate[user] = DateTime.Now;
                LogSubscriptionMessage(subscriptionMessage);
                ScheduleUpdate(user, initialDelay, period);
            }
        }
        public void UnSubscribeUser(IUser user, string unSubscriptionMessage)
        {
            if (!_subscriptionStartDate.ContainsKey(user)) return;
            _subscriptionStartDate.Remove(user);
            if (_userTimers.TryGetValue(user, out var timer))
            {
                timer.Dispose();
                _userTimers.Remove(user);
            }
            LogSubscriptionMessage(unSubscriptionMessage);
        }

        protected abstract void ScheduleUpdate(IUser user, int initialDelay, int period);

        protected abstract void LogSubscriptionMessage(string message);

    }
}
