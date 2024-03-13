using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mnemoscheme.Models.Devices.IR
{
    internal static class IRDataParser
    {
        public static int GetTemperature(string Data)
        {
            if (Data == "") 
                return 0;
            if (Data.Length != 9)
                return -1;
            //ATR03120Z - input data example

            string atr = Data.Substring(0, 3);
            string endline = Data.Substring(7);
            if (atr != "ATR" || endline != "0Z") return -1;

            return Convert.ToInt32(Data.Substring(3, 4));
        }
    }
}
