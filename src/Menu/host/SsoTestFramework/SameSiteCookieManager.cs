using Microsoft.Owin.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;

namespace SsoTestFramework
{
    public class SameSiteCookieManager : ICookieManager
    {
        private readonly ICookieManager _innerManager;

        public SameSiteCookieManager() : this(new CookieManager())
        {
        }

        public SameSiteCookieManager(ICookieManager innerManager)
        {
            _innerManager = innerManager;
        }

        public void AppendResponseCookie(IOwinContext context, string key, string value,
                                         CookieOptions options)
        {
            CheckSameSite(context, options);
            _innerManager.AppendResponseCookie(context, key, value, options);
        }

        public void DeleteCookie(IOwinContext context, string key, CookieOptions options)
        {
            CheckSameSite(context, options);
            _innerManager.DeleteCookie(context, key, options);
        }

        public string GetRequestCookie(IOwinContext context, string key)
        {
            return _innerManager.GetRequestCookie(context, key);
        }

        private void CheckSameSite(IOwinContext context, CookieOptions options)
        {
            if (options.SameSite == Microsoft.Owin.SameSiteMode.None )
            {
                //&&SameSite.BrowserDetection.DisallowsSameSiteNone(context.Request.Headers["User-Agent"])
                options.SameSite = null;
            }
        }
    }
}