using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace ACME.Domain.Controllers
{
    class LogController
    {
        private string LogPath;

        public LogController(string logPath)
        {
            LogPath = logPath;
        }
        public void WriteLog(string text)
        {
            if (LogPath.Length > 0)
            {
                if (!File.Exists(LogPath))
                {
                    using (StreamWriter logFile = File.CreateText(LogPath))
                    {
                        logFile.WriteLine(DateTime.Now + " " + text);
                        logFile.Close();
                    }
                }
                else
                {
                    using (StreamWriter logFile = File.AppendText(LogPath))
                    {
                        logFile.WriteLine(DateTime.Now + " " + text);
                        logFile.Close();
                    }
                }
                
            }
            
        }
    }
}
