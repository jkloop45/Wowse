using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WPluginSupport
{
    [AttributeUsage(AttributeTargets.Class |
            AttributeTargets.Interface,
            AllowMultiple = false,
            Inherited = false)]
    public class PluginInfo : Attribute
    {
        /// <summary>
        /// 插件名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 作者.
        /// </summary>
        public string Author;
        /// <summary>
        /// 内部版本号, 用于比较新版本以升级.
        /// </summary>
        public int[] InternalVersion;
        /// <summary>
        /// 外部版本号, 用于显示.
        /// </summary>
        public string PublicVersion;
        /// <summary>
        /// 作者邮箱.
        /// </summary>
        public string Email;
        /// <summary>
        /// 插件简介.
        /// </summary>
        public string Desc;
        /// <summary>
        /// 图标.
        /// </summary>
        public ImageSource Icon;
    }
}
