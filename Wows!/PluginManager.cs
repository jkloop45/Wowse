using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Reflection;
using Logcat;
using static Logcat.Loger;
using WPluginSupport;

namespace WowsExclamation
{
    public class PluginManager
    {
        public MainWindow CurrentWindow { get; private set; }
        public PluginManager(MainWindow window)
        {
            CurrentWindow = window;
        }
        void LoadPlugins()
        {
            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + @"\plugin");
            var plugins = from p in files where p.EndsWith(".dll") select p;

            foreach (string pluPath in plugins)
            {
                // ---------Start Load Plugin----------
                try
                {
                    Assembly ab = Assembly.LoadFile(pluPath);
                    var types = ab.GetTypes();
                    foreach (Type t in types)
                    {
                        if (t.GetInterface("IPlugin") != null)
                        {

                        }
                    }
                } catch (Exception e)
                {
                    Log("Load plugin " + pluPath + " faild!\n" + e.ToString(), LogLevel.Error);
                }
            }
        }
        void Initiazation()
        {
            WSDK.GetUserName = GetUserName;
        }
        public string GetUserName()
        {
            return CurrentWindow.mUser.Username;
        }
    }
}
