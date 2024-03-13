using Mnemoscheme.Commands;
using Mnemoscheme.Models.Devices.IR;
using Mnemoscheme.Models.Ports;
using Mnemoscheme.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mnemoscheme.ViewModels
{
    internal class IRViewModel : ViewModelBase
    {
        #region Public Attributes
        public ObservableCollection<string> Ports => PortList.AvailablePorts;
        public List<int> Speeds => PortList.ListPortSpeeds;

        public string PortName
        {
            get => port.PortName;
            set
            {
                port.PortName = value;
                OnPropertyChanged();
            }
        }

        public int PortSpeed
        {
            get => port.BaudRate;
            set
            {
                port.BaudRate = value;
            }
        }

        public string IsMeasuring => _deviceState.IsMeasuring.ToString();
        public string IRState => _deviceState.IRState.ToString();

        public string CurrentTemperature => _deviceState.CurrentTemperature.ToString();
        public string CurrentTime => _deviceState.CurrentTime.ToString();

        public int Interval
        {
            set
            {
                interval = value;
                OnPropertyChanged();
            }

            get { return interval; }
        }
        #endregion

        #region Commands
        public ICommand StartListenningCommand { get; }

        private void StartListenningCommandHandler()
        {
            _device.StartListenning(port, interval);
        }


        public ICommand StopListenningCommand { get; }
        private void StopListenningCommandHandler()
        {
            _device.StopListenning();
        }
        #endregion

        private IRDevice _device;
        private IRDeviceState _deviceState;
        private SerialPort port;
        private int interval;

        public IRViewModel() 
        {
            port = new SerialPort();
            var device = new IRDevice();
            _device = device;
            _deviceState = device.DeviceState;

            _deviceState.PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName);

            StartListenningCommand = new RelayCommand(StartListenningCommandHandler);
            StopListenningCommand = new RelayCommand(StopListenningCommandHandler);
        }
    }
}