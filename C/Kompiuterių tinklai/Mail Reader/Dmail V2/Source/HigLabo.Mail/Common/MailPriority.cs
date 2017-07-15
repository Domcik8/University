using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HigLabo.Net.Mail
{
    /// <summary>
    /// 重要度
    /// </summary>
    public enum MailPriority : byte
    {
        /// <summary>
        /// 最高重要度
        /// </summary>
        High = 1,
        /// <summary>
        /// 普通
        /// </summary>
        Normal = 3,
        /// <summary>
        /// 最低重要度
        /// </summary>
        Row = 5
    }
}
