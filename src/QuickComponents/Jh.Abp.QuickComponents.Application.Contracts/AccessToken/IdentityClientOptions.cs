using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.Abp.QuickComponents.AccessToken
{
    public class IdentityClientOptions
    {
        public string Authority { get; set; }
        public string Scope { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public bool RequireHttps { get; set; }
    }
}
