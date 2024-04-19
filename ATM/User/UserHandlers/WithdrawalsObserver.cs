using ATM.User.Interfaces;
using ATM.User.UserTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.User.UserHandlers
{
    internal class WithdrawalsObserver : IWithdrawalsObserver
    {
        public readonly Dictionary<BaseUser, int> monthlyWithdrawsCount = [];

        private static WithdrawalsObserver? withdrawsObserver;
        private static readonly object _lock = new();

        private WithdrawalsObserver() { }

        public static IWithdrawalsObserver GetWithdrawsObserver()
        {
            lock (_lock)
            {
                withdrawsObserver ??= new WithdrawalsObserver();
            }
            return withdrawsObserver;
        }
        public void AddWithdrawal(BaseUser user)
        {
            if (monthlyWithdrawsCount.TryGetValue(user, out int value))
            {
                monthlyWithdrawsCount[user] = ++value;
            }
            else
            {
                monthlyWithdrawsCount[user] = 1;
            }
        }

        public Dictionary<BaseUser, int> GetMonthlyWidrawals() => monthlyWithdrawsCount;

    }
}
