using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mnemoscheme.Models.Ports
{
    abstract class PortBase
    {
        protected SerialPort _serialPort;
        public string PortName => _serialPort.PortName;
        public int BaudRate => _serialPort.BaudRate;
        public bool IsOpen => _serialPort.IsOpen;

        public PortBase()
        {
            _serialPort = new SerialPort();
        }

        public PortBase(string PortName)
        {
            _serialPort = new SerialPort();

            SetPortName(PortName);
        }

        public PortBase(string PortName, int BaudRate)
        {
            _serialPort = new SerialPort();

            SetPortName(PortName);
            SetBaudRate(BaudRate);
        }

        public virtual void SetPortName(string PortName)
        {
            _serialPort.PortName = PortName;
        }

        public virtual void SetBaudRate(int BaudRate)
        {
            _serialPort.BaudRate = BaudRate;
        }

        public virtual void Open()
        {
            _serialPort.Open();
        }

        public virtual void Close()
        {
            _serialPort.Close();
        }

    }
}
