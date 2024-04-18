using ATM.User.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.User
{
    internal class UsersStateManager
    {
        private readonly IUserFactory _userFactory;
        private readonly List<BaseUser> _users = [];

        // singleton pattern
        private static UsersStateManager? _usersStateManager;
        private static readonly object _lock = new();

        private UsersStateManager(IUserFactory userFactory)
        {
            _userFactory = userFactory;
        }

        public static UsersStateManager GetUsersStateManager(IUserFactory userFactory)
        {
            lock (_lock)
            {
                _usersStateManager ??= new UsersStateManager(userFactory);
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

        public void AddUser(UserType userType, string userName, double moneyInAccount)
        {
            BaseUser user = _userFactory.CreateUser(userType, userName, moneyInAccount);
            _users.Add(user);
        }

    }
}
