using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRLibriary;

namespace IRLibriary
{
    internal static class IRCommands
    {
        //Инициализация обмен данных
        private static readonly byte[] atq =
        {
            65, 84, 81
        };

        //Получить данные
        private static readonly byte[] atr =
        {
            65, 84, 82
        };

        //Остановка обмена данных
        private static readonly byte[] atu =
        {
            65, 84, 85
        };

        public static byte[] GetInitialCommand() => atq;
        public static byte[] GetReadDataCommand() => atr;
        public static byte[] GetStopCommand() => atu;
    }
}
