using Mnemoscheme.Models.Devices.Base;
using Mnemoscheme.Models.Logs;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mnemoscheme.Models.Devices.IR
{
    internal class IRConsolidator : BaseConsolidator
    {
        private IRDevice _device;
        private IRDeviceState _deviceState;
        //TODO: add DataBase

        public IRConsolidator(IRDevice Device, Logger Logger) 
        { 
            _device = Device;
            _deviceState = Device.DeviceState;
            _logger = Logger;
        }

        public void ChangeState(IRState State)
        {
            _deviceState.IRState = State;

            string LoggerInfo;
            switch(State)
            {
                case IRState.WaitForStart:
                    LoggerInfo = "IR is waiting for starting";
                    break;

                case IRState.WaitInterval:
                    LoggerInfo = "IR is waiting for interval";
                    break;

                case IRState.Measuring:
                    LoggerInfo = "IR is measuring";
                    break;

                default:
                    throw new NotImplementedException("Uncased IRState");
            }

            SendToLogger(LoggerInfo, LogType.INFO);
        }

        public void SaveData(int Temperature, long Time)
        {
            //TODO: Call DataBase to Save

            SendToLogger($"Got IR Data: Temperature: {Temperature}, Time: {Time}", LogType.INFO);
        }
    }
}
