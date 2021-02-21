using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.Abp.Common
{
    public class AbpConsts
    {
        public const string SerilogOutputTemplate = "{NewLine}{NewLine}Date：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}LogLevel：{Level}{NewLine}Message：{Message}{NewLine}{Exception}";
    }
}
