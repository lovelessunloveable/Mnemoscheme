using Mnemoscheme.Models.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mnemoscheme.Models.Devices.Base
{
    internal abstract class BaseConsolidator
    {
        protected Logger _logger;

        public virtual void SendToLogger(string message, LogType eventLogType)
        {
            //TODO - call logger function
            _logger?.ToString();
        }
    }
}
