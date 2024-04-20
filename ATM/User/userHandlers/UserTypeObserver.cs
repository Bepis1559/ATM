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
    internal class UserTypeObserver : BaseObserver
    {
        private readonly ITypeUpdater _typeUpdater;

        private readonly ILogger _logger;

        private static IObserver? _userTypeObserver;

        private static readonly object _lock = new();
        private UserTypeObserver(ITypeUpdater typeUpdater, ILogger logger)
        {
            _typeUpdater = typeUpdater;
            _logger = logger;
        }

        public static IObserver GetUserTypeObserver(ITypeUpdater typeUpdater, ILogger logger)
        {
            lock (_lock)
            {
                _userTypeObserver ??= new UserTypeObserver(typeUpdater, logger);
            }
            return _userTypeObserver;
        }
         protected override void ScheduleUpdate(IUser user,int initialDelay,int period)
        {
            _timer = new ((state) =>
            {
                _typeUpdater.UpdateUserType(user);
                _subscriptionStartDate.Remove(user);
                _logger.LogInfo($"{user.Name} has been updated to a {user.UserType} user !");
                user.MonthlyWithdrawalsCount = 0;
            }, null, TimeSpan.FromDays(initialDelay), TimeSpan.FromDays(period));
        }

        protected override void LogSubstrictionMessage(string message)
        {
            _logger.LogInfo(message);
        }
    }
}
