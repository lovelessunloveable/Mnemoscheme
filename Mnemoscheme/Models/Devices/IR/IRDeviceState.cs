using Mnemoscheme.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mnemoscheme.Models.Devices.IR
{
    public enum IRState
    {
        WaitForStart,
        WaitInterval,
        Measuring,
    }

    public class IRDeviceState : OnPropertyChangedBase
    {
        private IRState _state;
        private bool _isMeasuring = false;

        public IRState IRState
        {
            set 
            { 
                _state = value; 
                OnPropertyChanged();
            }
            
            get
            {
                return _state;
            }
        }

        public bool IsMeasuring
        {
            set { _isMeasuring = value; OnPropertyChanged(); }
            get => _isMeasuring;
        }
    }
}
