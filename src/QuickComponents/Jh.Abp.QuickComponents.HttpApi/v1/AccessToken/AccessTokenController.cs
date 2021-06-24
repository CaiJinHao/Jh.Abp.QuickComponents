using Jh.Abp.QuickComponents.AccessToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using System.Linq;
using Volo.Abp.Users;
using Volo.Abp.Auditing;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using IdentityModel.Client;

namespace Jh.Abp.QuickComponents.HttpApi.v1.AccessToken
{
    /*
     前端传的时候直接写死，SwaggerApi 参数化了
    [MapToApiVersion("1.0")] 对Action进行版本标记
    默认1.0,方法有几个版本写几个版本
     */
    //[ApiVersion("1.0")]
    //[ApiVersion("2.0")]
    //[ApiVersion("3.0")]
    [RemoteService(Name = JhAbpQuickComponentsRemoteServiceConsts.RemoteServiceName)]
    [Route("api/v{apiVersion:apiVersion}/[controller]")]
    public class AccessTokenController : JhAbpQuickComponentsController
    {
        private readonly IConfiguration _configuration;
        public AccessTokenController(
            IConfiguration configuration
            )
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<AccessTokenResponseDto> GetAccessTokenAsync([FromBody]AccessTokenRequestDto requestDto)
        {
            // discover endpoints from metadata
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(_configuration["AuthServer:Authority"]);
            if (disco.IsError)
            {
                throw new System.Exception("Discovery Error");
            }

            // request token
            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = _configuration["AuthServer:AppClientId"],
                ClientSecret = _configuration["AuthServer:AppClientSecret"],
                UserName = requestDto.UserNameOrEmailAddress,
                Password = requestDto.Password,
                Scope = _configuration["AuthServer:Scope"]
            });
            if (tokenResponse.IsError)
            {
                throw new System.Exception("Error");
            }
            return new AccessTokenResponseDto()
            {
                AccessToken = tokenResponse.AccessToken,
                ExpiresIn = tokenResponse.ExpiresIn,
                RefreshToken = tokenResponse.RefreshToken,
                TokenType = tokenResponse.TokenType
            };
        }

        [Route("claims")]
        [HttpGet]
        public dynamic GetClaimsAsync()
        {
            return CurrentUser.GetAllClaims().Select(a => new { a.Type, a.Value });
        }

        //[HttpPost("Refresh")]
        //public async Task<AccessTokenResponseDto> GetRefreshAccessTokenAsync(string refreshToken)
        //{
        //    return await _accessTokenAppService.GetRefreshAccessTokenAsync(refreshToken);
        //}

        /*
         [HttpPost]
        [MapToApiVersion("1.0")]
        public Task<string> PostAsyncV1()
        {
            return PostAsync("v1");
        }

        [HttpPost]
        [MapToApiVersion("2.0")]
        [MapToApiVersion("3.0")]
        public Task<string> PostAsyncV2()
        {
            return PostAsync("v2v3");
        }

        private Task<string> PostAsync(string msg)
        {
            return Task.FromResult($"{msg}-Post-{HttpContext.GetRequestedApiVersion().ToString()}");
        }
        */
    }
}
