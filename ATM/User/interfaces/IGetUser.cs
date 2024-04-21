using ATM.Helpers.interfaces;
using ATM.User.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.User.interfaces
{
    public interface IGetUser
    {
        IUser? GetUser(string userId);
       
    }
}
