using ATM.Bank.Interfaces;
using ATM.Helpers.interfaces;
using ATM.User.interfaces;
using ATM.User.UserHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Commands
{
    public class Receiver(IATM atm, ILogger logger, IGetUser usersManager, IStrategyRetriever strategyRetriever, IObserver userTypeObserver) : ICommandReceiver
    {
        private readonly IATM _atm = atm;

        private readonly ILogger _logger = logger;

        private readonly IGetUser _usersManager = usersManager;

        private readonly IStrategyRetriever _strategyRetriever = strategyRetriever;

        private readonly IObserver _userTypeObserver = userTypeObserver;


        public void CheckBalance()
        {
            IUser? user = GetUserFromConsole("Please enter your id : ");
            if (user is null) return;
            _atm.CheckBalance(user);
        }


        public void TransferMoney()
        {
            IUser? sender = GetUserFromConsole("Please enter sender (your) id : ");
            if (sender is null) return;
            IUser? receiver = GetUserFromConsole("Please enter receiver id : ");
            if (receiver is null) return;
            decimal amount = GetAmountFromConsole();
            try
            {
                _atm.TransferMoney(sender, receiver, amount);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogInfo(ex.Message);
            }

        }

        public void WithdrawMoney()
        {
            IUser? user = GetUserFromConsole("Please enter your id : ");
            if (user is null) return;
            decimal amount = GetAmountFromConsole();
            try
            {
                _atm.WithdrawMoney(user, amount, _strategyRetriever, _userTypeObserver);
            }
            catch (Exception ex)
            {
                _logger.LogInfo(ex.Message);
            }
        }


        private decimal GetAmountFromConsole()
        {
            _logger.LogInfo("Please enter amount : ");
            if (decimal.TryParse(_logger.ReadInfo(), out decimal amount))
            {

                return amount;
            }
            else
            {
                _logger.LogInfo("Invalid input. Please enter a valid decimal number.");
                return 0;
            }
        }


        private IUser? GetUserFromConsole(string requestMessage)
        {
            _logger.LogInfo(requestMessage);
            string? id = _logger.ReadInfo();
            IUser? user = _usersManager.GetUser(id);
            if (user is null) _logger.LogInfo($"There is no user with an id of : {id} ");
            return user;
        }
    }
}
