using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Jh.Abp.MenuManagement
{
    /// <summary>
    /// 可用类型(是or否)
    /// </summary>
    public enum UseType
    {
        None = 0,
        [Description("是")]
        Yes = 1,
        [Description("否")]
        No = 2
    }
}
