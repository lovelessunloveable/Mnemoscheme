using Mnemoscheme.Models.Ports;
using Mnemoscheme.Models.Logs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Mnemoscheme.Models.Devices.IR
{
    internal class IRController
    {
        private IRDevice _irDevice;
        private SerialPort _serialPort;
        private IRConsolidator _consolidator;

        public IRController(IRDevice irDevice, IRConsolidator consolidator)
        {
            _irDevice = irDevice;
            _consolidator = consolidator;
        }

        public void StartReading(SerialPort Port, int interval)
        {
            _serialPort = Port;
            try
            {
                _serialPort.Open();
            }
            catch (UnauthorizedAccessException)
            {
                _consolidator?.SendToLogger($"Port is unavailable: ${Port.PortName}", LogType.ERROR);
                return;
            }

            var IRDataReadingThread = new Thread(() => Reading(Port, interval));
            IRDataReadingThread.IsBackground = true;
            IRDataReadingThread.Priority = ThreadPriority.Highest;
            IRDataReadingThread.Start();
        }

        //OPEN BEFORE CALLING!!!!!
        private void Reading(SerialPort Port, int interval)
        {
            Port?.Write(IRCommands.GetInitialCommand(), 0, 3);

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            _consolidator?.ChangeState(IRState.WaitInterval);

            while (Port.IsOpen)
            {
                try
                {
                    Thread.Sleep(interval);
                    int temperature = -1;
                    do
                    {
                        Port?.Write(IRCommands.GetInitialCommand(), 0, 3);
                        string Data = Port?.ReadExisting();
                        temperature = IRDataParser.GetTemperature(Data);

                        if (temperature == -1)
                            _consolidator.SendToLogger("IR got corrupted data", LogType.WARNING);
                    }
                    while (temperature == -1 && Port != null);

                    if (temperature == 0)
                        continue;

                    long time = stopwatch.ElapsedMilliseconds;

                    _consolidator?.SaveData(temperature, time);
                }
                catch(UnauthorizedAccessException)
                {
                    _consolidator?.SendToLogger($"Port is unavailable: ${Port.PortName}", LogType.ERROR);
                    StopReading();
                    break;
                }
                //TODO Add all possible cathces 
            }

            stopwatch.Stop();
        }

        public void StopReading()
        {
            _serialPort?.Close();
            _consolidator?.SendToLogger($"Port closed: {_serialPort.PortName}, {_serialPort.BaudRate}", Logs.LogType.INFO);
            _consolidator?.ChangeState(IRState.WaitForStart);
        }
    }
}
