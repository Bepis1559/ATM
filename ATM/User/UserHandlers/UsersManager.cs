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
    internal class UsersManager
    {
        private readonly IUserFactory _userFactory;
        private readonly List<IUser> _users = [];

        private static UsersManager? _usersStateManager;
        private static readonly object _lock = new();

        private UsersManager(IUserFactory userFactory)
        {
            _userFactory = userFactory;
        }

        public static UsersManager GetUsersStateManager(IUserFactory userFactory)
        {
            lock (_lock)
            {
                _usersStateManager ??= new UsersManager(userFactory);
            }
            return _usersStateManager;
        }

        public IUser? GetUser(string userId) => _users.FirstOrDefault(user => user.Id == userId);

        public List<IUser> GetUsers() => _users;

        public IUser? RemoveUser(string userId,IObserver dividendObserver,ILogger logger)
        {
            IUser? user = _users.FirstOrDefault(user => user.Id == userId);
            ArgumentNullException.ThrowIfNull(user);
            dividendObserver.UnSubscribeUser(user,$"{user.Name} has been unsubscribed from receiving dividends .");
            _users.Remove(user);
            logger.LogInfo($"{user.Name} was removed .");

            return user;
        }

        public IUser AddUser(UserType userType, string userName, decimal moneyInAccount,IObserver dividendObserver, ILogger logger)
        {

            IUser user = _userFactory.CreateUser(userType, userName, moneyInAccount);
            _users.Add(user);
            dividendObserver.SubscribeUser(user,$"{user.Name} will now receive didvidents on monthly basis ! ",30,30);
            logger.LogInfo($"{user.Name} is now a registered user !");
            return user;
        }

    }
}
