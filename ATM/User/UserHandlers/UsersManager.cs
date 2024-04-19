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
        private readonly List<BaseUser> _users = [];

        // singleton pattern
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

        public BaseUser? GetUser(string userId) => _users.FirstOrDefault(user => user.Id == userId);

        public List<BaseUser> GetUsers() => _users;

        public BaseUser? RemoveUser(string userId)
        {
            BaseUser? user = _users.FirstOrDefault(user => user.Id == userId);
            if (user != null) _users.Remove(user);
            return user;
        }

        public BaseUser AddUser(UserType userType, string userName, decimal moneyInAccount)
        {
            BaseUser user = _userFactory.CreateUser(userType, userName, moneyInAccount);
            _users.Add(user);
            return user;
        }

    }
}
