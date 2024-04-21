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

        public string? ReadInfo()
        {
            string? info = Console.ReadLine();
            return info;
        }
    }
}
