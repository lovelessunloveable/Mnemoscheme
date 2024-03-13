using Mnemoscheme.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mnemoscheme.Models.Devices.IR
{
    public class IRDevice : OnPropertyChangedBase
    {
        private IRDeviceState _deviceState;
        private IRConsolidator _consolidator;

        public IRDeviceState DeviceState 
        {
            get { return _deviceState; } 
        }

        public IRDevice() 
        { 
            _deviceState = new IRDeviceState();
            _consolidator = new IRConsolidator(this);
        }

        public void ChangeDeviceState(IRState state) => _deviceState.IRState = state;
    }
}
