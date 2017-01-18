using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Logcat
{
    static public class Loger
    {
        static private StreamWriter mStream;
        static public void Log(string info)
        {
            Log(info, LogLevel.Infomation);
        }
        static public void Log(string info, LogLevel level)
        {
            if (mStream == null)
            {
                try
                {
                    mStream = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.LocalApp‌​licationData) + @"\Wows!\wowse.log", true);
                    mStream.WriteLine();
                } catch
                {
                    System.Windows.Forms.MessageBox.Show("Can not open log file.");
                    Environment.Exit(1);
                }
            }
            var date = "[" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "]\t";
            var logLevel ="["+ Enum.GetName(typeof(LogLevel), level)+"]\t\t";
            var log = date + logLevel + info;
            mStream.WriteLine(log);
            mStream.Flush();
        }
        static public void Clear()
        {
            mStream = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.LocalApp‌​licationData) + @"\Wows!\wowse.log", false);
            mStream.WriteLine();
            mStream = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.LocalApp‌​licationData) + @"\Wows!\wowse.log", true);
        }
    }
    public enum LogLevel
    {
        Infomation,
        Worning,
        Error,
    }
}
