using ATM.Helpers.classes;
using ATM.Helpers.interfaces;
using ATM.User.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.User.userHandlers
{
    internal class MonthlyDividendObserver : BaseObserver
    {
        private readonly ILogger _logger;

        private static IObserver? _monthlyDividendObserver;

        private static readonly object _lock = new();

        private MonthlyDividendObserver(ILogger logger)
        {
            _logger = logger;
        }

        public static IObserver GetMonthlyDividendObserver(ILogger logger)
        {
            lock (_lock)
            {
                _monthlyDividendObserver ??= new MonthlyDividendObserver(logger);
            }
            return _monthlyDividendObserver;
        }

        protected override void ScheduleUpdate(IUser user, int initialDelay, int period)
        {
                ArgumentOutOfRangeException.ThrowIfNegative(user.MoneyInAccount);
            _timer = new((state) =>
            {
                ArgumentOutOfRangeException.ThrowIfNegative(user.MoneyInAccount);
                const decimal dividendPercentage = 0.01m;
                decimal dividend = dividendPercentage * user.MoneyInAccount;
                user.MoneyInAccount = dividend + user.MoneyInAccount;
                _subscriptionStartDate.Remove(user);
                _logger.LogInfo($"{user.Name} has just received their dividend of {dividend}$ ! ");
            }, null, TimeSpan.FromDays(initialDelay), TimeSpan.FromDays(period));
        }

        protected override void LogSubstrictionMessage(string message)
        {
            _logger.LogInfo(message);
        }
    }
}
