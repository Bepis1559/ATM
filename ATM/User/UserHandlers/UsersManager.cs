using ATM.Helpers.interfaces;
using ATM.User.interfaces;
using ATM.User.Interfaces;
using ATM.User.UserTypes;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.User.UserHandlers
{
    internal class UsersManager : IGetUser
    {
        private readonly List<IUser> _users = [];

        private static UsersManager? _usersStateManager;
        private static readonly object _lock = new();

        private UsersManager()
        {
        }

        public static UsersManager GetUsersStateManager()
        {
            lock (_lock)
            {
                _usersStateManager ??= new UsersManager();
            }
            return _usersStateManager;
        }

        public IUser? GetUser(string userId) => _users.FirstOrDefault(user => user.Id == userId);

        public List<IUser> GetUsers() => _users;

        public IUser? RemoveUser(string userId, IObserver dividendObserver, ILogger logger)
        {
            IUser? user = _users.FirstOrDefault(user => user.Id == userId);
            ArgumentNullException.ThrowIfNull(user);
            dividendObserver.UnSubscribeUser(user, $"{user.Name} has been unsubscribed from receiving dividends .");
            _users.Remove(user);
            logger.LogInfo($"{user.Name} was removed .");

            return user;
        }

        public IUser AddUser(string userName, decimal moneyInAccount, IObserver dividendObserver, ILogger logger, IUserFactory userFactory, UserType userType = UserType.Standard)
        {

            IUser user = userFactory.CreateUser(userType, userName, moneyInAccount);
            _users.Add(user);
            dividendObserver.SubscribeUser(user, $"{user.Name} will now receive dividends on monthly basis ! ", 30, 30);
            logger.LogInfo($"{user.Name} is now a registered user !");
            return user;
        }

    }
}
