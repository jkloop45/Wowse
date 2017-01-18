using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using

namespace WPluginSupport
{
    public static class WSDK
    {
        /// <summary>
        /// 没有返回值没有参数的 Delegate.
        /// </summary>
        public delegate void NoRetNoParHandle();
        /// <summary>
        /// 返回 string 类型的 Delegate.
        /// </summary>
        public delegate string StringNoParHandle();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageType">所要添加的Page</param>
        public delegate void AddPageHandle(Page pageType);

        public static StringNoParHandle GetUserID;
        public static StringNoParHandle GetUserName;
        public static NoRetNoParHandle AddPage;
    }
}
