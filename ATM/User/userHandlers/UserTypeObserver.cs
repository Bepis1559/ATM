using ATM.Helpers.interfaces;
using ATM.User.interfaces;
using ATM.User.Interfaces;
using ATM.User.UserHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.User.userHandlers
{
    internal class UserTypeObserver : IUserTypeObserver
    {
        private readonly ITypeUpdater _typeUpdater;

        private readonly ILogger _logger;

        private readonly Dictionary<IUser, DateTime> _subscriptionStartDate = [];

        private static IUserTypeObserver? _userTypeObserver;
        private static readonly object _lock = new();
        private UserTypeObserver(ITypeUpdater typeUpdater, ILogger logger)
        {
            _typeUpdater = typeUpdater;
            _logger = logger;
        }

        public static IUserTypeObserver GetUserTypeObserver(ITypeUpdater typeUpdater, ILogger logger)
        {
            lock (_lock)
            {
                _userTypeObserver ??= new UserTypeObserver(typeUpdater, logger);
            }
            return _userTypeObserver;
        }

        public void SubscribeUser(IUser user)
        {
            if (!_subscriptionStartDate.ContainsKey(user))
            {
                _subscriptionStartDate[user] = DateTime.Now;
                ScheduleUpdate(user);
            }
            else
            {
                _logger.LogInfo("User is already subscribed.");
            }
        }


        private void ScheduleUpdate(IUser user)
        {
            Timer timer = new((state) =>
            {
                _typeUpdater.UpdateUserType(user);
                _subscriptionStartDate.Remove(user);
                user.MonthlyWithdrawalsCount = 0;
                ((Timer)state).Dispose();
            }, null, TimeSpan.FromDays(30), TimeSpan.FromMilliseconds(-1));
        }
    }
}
