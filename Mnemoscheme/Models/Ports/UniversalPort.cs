using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mnemoscheme.Models.Ports
{
    internal class UniversalPort : PortBase, IPortWritable, IPortReadable
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
            _serialPort.WriteLine(Line);
        }

        public void Write(string Text)
        {
            _serialPort.Write(Text);
        }
    }
}
