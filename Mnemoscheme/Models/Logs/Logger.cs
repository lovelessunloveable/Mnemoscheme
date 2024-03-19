using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace Mnemoscheme.Models.Logs
{
    public enum LogType
    {
        INFO,
        WARNING,
        ERROR,
    }

    public static class Logger
    {
        public void SaveInfo(string message)
        {

        }
        public static void ExportLog(string message)
        {
            string logPath = ConfigurationManager.AppSettings["logPath"];
            
            using(StreamWriter writer = new StreamWriter(logPath, true))
            {
                writer.WriteLine($"{ DateTime.Now} : {message}");
            }
        }
    }
}
