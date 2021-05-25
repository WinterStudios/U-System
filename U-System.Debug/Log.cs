using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U_System.Debug
{
    
    public class Log
    {
        public static string LogFileLocation { get {
                string fileLocation = string.Format("{0}\\Logs\\log_{1}.txt",
                    Directory.GetCurrentDirectory(),
                    DateTime.Today.ToShortDateString().Replace('/','-'));
                if(!Directory.Exists(Directory.GetCurrentDirectory() + "\\Logs\\"))
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Logs\\");
                return fileLocation;
            } }


        public static void LogMessage(string message, Type type, LogMessageType messageType = LogMessageType.Info, [System.Runtime.CompilerServices.CallerMemberName] string methouCall = "")
        {
            using (StreamWriter sw = File.AppendText(LogFileLocation))
            {
                string date = string.Format("{0}/{1}/{2}",
                    DateTime.Today.Year.ToString("0000"),
                    DateTime.Today.Month.ToString("00"),
                    DateTime.Today.Day.ToString("00"));

                string time = string.Format("{0}:{1}:{2}:{3}",
                    DateTime.Now.Hour.ToString("00"),
                    DateTime.Now.Minute.ToString("00"),
                    DateTime.Now.Second.ToString("00"),
                    DateTime.Now.Millisecond.ToString("000"));

                string text = string.Format("{0} | {1} | {2,-9} | {3} | {4} .: {5}",
                    date,
                    time,
                    messageType.ToString().ToUpper(),
                    type.FullName,
                    methouCall,
                    message);

                
                sw.WriteLine(text);
            }
        }

    }
}
