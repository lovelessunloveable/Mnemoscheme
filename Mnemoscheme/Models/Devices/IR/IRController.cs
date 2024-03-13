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
using System.IO;

namespace Mnemoscheme.Models.Devices.IR
{
    internal class IRController
    {
        private SerialPort _serialPort;
        private IRConsolidator _consolidator;

        const int MaxZeroDataCount = 10;

        public IRController(IRConsolidator consolidator)
        {
            _consolidator = consolidator;
        }

        public void StartReading(SerialPort Port, int interval)
        {
            _serialPort = Port;
            try
            {
                _serialPort.Open();
            }
            catch (IOException)
            {
                _consolidator?.SendToLogger($"Port is unavailable: ${Port.PortName}", LogType.ERROR);
                return;
            }

            var IRDataReadingThread = new Thread(() => Reading(Port, interval));
            IRDataReadingThread.Start();
        }

        //OPEN BEFORE CALLING!!!!!
        private void Reading(SerialPort Port, int interval)
        {
            Port?.Write(IRCommands.GetInitialCommand(), 0, 3);

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            _consolidator?.ChangeState(IRState.WaitInterval);

            int ZeroDataCount = 0;

            while (Port.IsOpen)
            {
                try
                {
                    if(ZeroDataCount == MaxZeroDataCount)
                    {
                        _consolidator.SendToLogger("A lot of zero data. Check the IR and port baud rate or reload it", LogType.ERROR);
                        StopReading();
                        break;
                    }

                    Thread.Sleep(interval);
                    int temperature = -1;
                    do
                    {
                        Port?.Write(IRCommands.GetReadDataCommand(), 0, 3);
                        string Data = Port?.ReadExisting();
                        temperature = IRDataParser.GetTemperature(Data);

                        if (temperature == -1)
                            _consolidator.SendToLogger("IR got corrupted data", LogType.WARNING);
                    }
                    while (temperature == -1 && Port != null);

                    if (temperature == 0)
                    {
                        _consolidator.SendToLogger("IR got zero data", LogType.WARNING);
                        ZeroDataCount++;
                        continue;
                    }

                    long time = stopwatch.ElapsedMilliseconds;

                    _consolidator?.SaveData(temperature, time);
                }
                catch(IOException)
                {
                    _consolidator?.SendToLogger($"Port is unavailable: ${Port.PortName}", LogType.ERROR);
                    StopReading();
                    break;
                }
                catch (InvalidOperationException ex) 
                {
                    if (!Port.IsOpen)
                        break;

                    _consolidator?.SendToLogger($"{ex.Message} was on port ${Port.PortName}", LogType.ERROR);

                }
                //TODO Add all possible cathces 
            }

            stopwatch.Stop();
        }

        public void StopReading()
        {
            if (_serialPort != null)
            {
                _serialPort?.Write(IRCommands.GetStopCommand(), 0, 3);
                _serialPort?.Close();
            }
            _consolidator?.SendToLogger($"Port closed: {_serialPort.PortName}, {_serialPort.BaudRate}", Logs.LogType.INFO);
            _consolidator?.ChangeState(IRState.WaitForStart);
        }
    }
}
