using ATM.Helpers.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Helpers.classes
{
    internal class Logger : ILogger
    {

        public void LogInfo(string message)
        {
            Console.WriteLine(message);
        }
        public void AtmOptions()
        {
            LogInfo("\nPlease choose:\n0 to end interaction.\n1 to check your current balance.\n2 to transfer money to another person.\n3 to withdraw money.\n4 to see the options again.");
        }

        public void CheckBalance()
        {
            LogInfo("Checking current balance...");
        }

        public void EndInteraction()
        {
            LogInfo("Ending interaction...");
        }

        public void InvalidOptionSelected()
        {
            LogInfo("Invalid option selected. Please choose a valid option (0, 1, 2, 3, or 4).");
        }



        public void TransferMoney()
        {
            LogInfo("Initiating money transfer...");
        }

        public void WithdrawMoney()
        {
            LogInfo("Withdrawing money...");
        }
    }
}
