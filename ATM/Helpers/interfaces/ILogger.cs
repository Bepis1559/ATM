using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Helpers.interfaces
{
    public interface ILogger
    {
        void LogInfo(string message);
        void AtmOptions();
        void InvalidOptionSelected();
        void EndInteraction();
        void CheckBalance();
        void TransferMoney();
        void WithdrawMoney();


       
    }
}
