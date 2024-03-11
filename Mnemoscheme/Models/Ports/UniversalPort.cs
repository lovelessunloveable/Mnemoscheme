using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mnemoscheme.Models.Ports
{
    internal class UniversalPort : IPort, IPortWritable, IPortReadable
    {
        public override void Close()
        {
            base.Close();

            //TODO: Close Thread
        }

        public string ReadLine()
        {
            throw new NotImplementedException();
        }

        public void WriteLine(string Line)
        {
            throw new NotImplementedException();
        }
    }
}
