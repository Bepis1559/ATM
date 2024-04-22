using ATM.Helpers.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Helpers.classes
{
    internal class Reader : IReader
    {
        public string? ReadInfo()
        {
            string? info = Console.ReadLine();
            return info;
        }
    }
}
