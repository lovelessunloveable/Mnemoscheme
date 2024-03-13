using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mnemoscheme.Models.Ports
{
    //Дата-класс для хранения и обновления списка доступных портов
    internal class PortList
    {
        #region Public Attributes
        public static ObservableCollection<string> AvailablePorts { get; private set; } = new ObservableCollection<string>();

        public const string NoAvaiblePortsString = "Нет доступных";

        public static bool IsAnyPortAvailable => AvailablePorts[0] != NoAvaiblePortsString;
        #endregion

        public static List<int> ListPortSpeeds = new List<int>()
        {
            300,
            600,
            1200,
            2400,
            4800,
            9600,
            14400,
            19200,
            28800,
            31250,
            38400,
            57600,
            115200,
        };

        public static int GetStandartSpeed()
        {
            return 9600;
        }

        public static void UpdateAvailablePortList()
        {
            AvailablePorts.Clear();

            var serialPorts = SerialPort.GetPortNames();

            if (serialPorts.Length == 0)
            {
                AvailablePorts.Add(NoAvaiblePortsString);
                return;
            }

            foreach (var serialPort in serialPorts)
            {
                AvailablePorts.Add(serialPort);
            }
        }

        public static string GetFirstAvailablePort()
        {
            if (AvailablePorts == null)
                return null;

            if (AvailablePorts.Count == 0)
                return null;

            if (AvailablePorts[0] == NoAvaiblePortsString)
                return null;

            return AvailablePorts[0];
        }
    }
}
