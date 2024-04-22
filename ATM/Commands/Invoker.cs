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
            bool continueInteraction = true;

            while (continueInteraction)
            {
                logger.AtmOptions();

                if (!int.TryParse(reader.ReadInfo(), out int option))
                {
                    logger.InvalidOptionSelected();
                    continue;
                }

                switch (option)
                {
                    case 0:
                        logger.EndInteraction();
                        continueInteraction = false;
                        break;
                    case 1:
                        logger.CheckBalance();
                        receiver.CheckBalance();
                        break;
                    case 2:
                        logger.TransferMoney();
                        receiver.TransferMoney();
                        break;
                    case 3:
                        logger.WithdrawMoney();
                        receiver.WithdrawMoney();
                        break;
                    case 4:
                        continue;
                    default:
                        logger.InvalidOptionSelected();
                        break;
                }
            }
        }


    }
}
