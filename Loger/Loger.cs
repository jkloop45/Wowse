using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Loger
{
    static public class Loger
    {
        static private StreamWriter mStream;
        static public void Log(string info)
        {
            if (mStream == null)
            {
                mStream = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.LocalApp‌​licationData) + @"\Wows!\wowse.log", true);
                mStream.WriteLine();
            }
            var log = "[" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "] " + info;
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
}
