using ATM.Helpers.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Commands
{
    internal class Invoker
    {

        public void HandleCustomerInteraction(ILogger logger, ICommandReceiver receiver,IReader reader)
        {
            const string optionsMessage = "\nPlease choose:\n0 to end interaction.\n1 to check your current balance.\n2 to transfer money to another person.\n3 to withdraw money.\n4 to see the options again.";
            const string invalidOptionMessage = "Invalid option selected. Please choose a valid option (0, 1, 2, 3, or 4).";
            bool continueInteraction = true;

            while (continueInteraction)
            {
                logger.LogInfo(optionsMessage);

                if (!int.TryParse(reader.ReadInfo(), out int option))
                {
                    logger.LogInfo(invalidOptionMessage);
                    continue;
                }

                switch (option)
                {
                    case 0:
                        logger.LogInfo("Ending interaction...");
                        continueInteraction = false;
                        break;
                    case 1:
                        logger.LogInfo("Checking current balance...");
                        receiver.CheckBalance();
                        break;
                    case 2:
                        logger.LogInfo("Initiating money transfer...");
                        receiver.TransferMoney();
                        break;
                    case 3:
                        logger.LogInfo("Withdrawing money...");
                        receiver.WithdrawMoney();
                        break;
                    case 4:
                        continue;
                    default:
                        logger.LogInfo(invalidOptionMessage);
                        break;
                }
            }
        }


    }
}
