using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanCloudInteraction
{
    public enum ErrorCodes
    {
        /// <summary>
        /// 连接失败.
        /// </summary>
        CantConnect = 100,
        /// <summary>
        /// 账号或密码错误.
        /// </summary>
        UnPwError = 210,
        /// <summary>
        /// 邮箱格式错误.
        /// </summary>
        EmailFormatErr = 125,
        /// <summary>
        /// 邮箱已被注册.
        /// </summary>
        EmailToken = 203,
    }
}
