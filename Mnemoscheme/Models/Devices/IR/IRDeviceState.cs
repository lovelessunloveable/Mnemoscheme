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

        #region Public Attributes
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

        public int CurrentTemperature
        {
            set
            {
                _currentTemperature = value;
                OnPropertyChanged();
            }

            get
            {
                return _currentTemperature;
            }
        }

        public long CurrentTime
        {
            set
            {
                _currentTime = value;
                OnPropertyChanged();
            }

            get
            {
                return _currentTime;
            }
        }
        #endregion


        private IRState _state;

        private int _currentTemperature = 0;
        private long _currentTime = 0;
    }
}
