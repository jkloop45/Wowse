using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPluginSupport
{
    interface IWPlugin
    {
        /// <summary>
        /// 加载时调用.
        /// </summary>
        void OnLoad();
        /// <summary>
        /// 卸载时调用.
        /// </summary>
        void OnDestroy();
    }
}
