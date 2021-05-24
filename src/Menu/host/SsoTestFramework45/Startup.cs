using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System.IdentityModel.Tokens.Jwt;

namespace SsoTestFramework45
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            /*
            问题原因：
Asp.net 的安全和表示下的机密管理有一项是强制执行https，
            A批客户端可能不理解活遵循从HTTP到HTTPS的重定向。此类客户端可以通过HTTP发送信息，Web Api 应：不侦听HTTP。关闭状态代码为400的连接（错误请求）并且不处理请求。
             chrome 内核需要金庸SameSite 
            Chrome浏览器 => chrome://flags  =>  Cookies without SameSite must be secure => disabled=》重启浏览器
             */

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            //app.UseKentorOwinCookieSaver();
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = CookieAuthenticationDefaults.AuthenticationType,
            });
            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                Authority = "http://localhost:6102", //ID Server
                ClientId = "MenuManagement_Web",
                ResponseType = "id_token code",
                SignInAsAuthenticationType = "Cookies",
                RedirectUri = "http://localhost:44309/signin-oidc", //URL of website
                Scope = "email openid profile role phone address MenuManagement offline_access",
                RequireHttpsMetadata = false,
            });
        }
    }
}