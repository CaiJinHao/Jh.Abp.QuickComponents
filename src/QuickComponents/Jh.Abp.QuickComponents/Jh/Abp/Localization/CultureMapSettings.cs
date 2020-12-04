using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jh.Abp.QuickComponents.Localization
{
    public class CultureMapSettings
    {
        public string TargetCulture { get; set; }
        public List<string> SourceCultures { get; set; }
    }
}
