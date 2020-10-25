using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jh.Abp.QuickComponents.Localization
{
    public class CultureMapOptions
    {
        public List<CultureMapSettings> CultureMaps { get; set; }
        public List<CultureMapSettings> UiCultureMaps { get; set; }
    }
}
