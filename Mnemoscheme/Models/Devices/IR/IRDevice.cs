using Mnemoscheme.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mnemoscheme.Models.Devices.IR;
using System.IO.Ports;

namespace Mnemoscheme.Models.Devices.IR
{
    public class IRDevice : OnPropertyChangedBase
    {
        private IRDeviceState _deviceState;
        private IRConsolidator _consolidator;
        private IRController _controller;

        public IRDeviceState DeviceState 
        {
            get { return _deviceState; } 
        }

        public IRDevice() 
        { 
            _deviceState = new IRDeviceState();
            _consolidator = new IRConsolidator(this, null);
            _controller = new IRController(_consolidator);
        }

        public void StartListenning(SerialPort Port, int interval)
        {
            _controller.StartReading(Port, interval);
        }

        public void StopListenning()
        {
            _controller.StopReading();
        }
    }
}
