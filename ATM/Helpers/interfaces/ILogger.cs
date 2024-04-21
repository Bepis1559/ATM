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
        string? ReadInfo();
    }
}
