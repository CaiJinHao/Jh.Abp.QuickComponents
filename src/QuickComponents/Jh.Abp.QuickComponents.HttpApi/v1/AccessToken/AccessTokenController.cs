using Jh.Abp.QuickComponents.AccessToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using System.Linq;
using Volo.Abp.Users;
using Volo.Abp.Auditing;

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
        private readonly IAccessTokenAppService _accessTokenAppService;
        public AccessTokenController(
            IAccessTokenAppService accessTokenAppService
            )
        {
            _accessTokenAppService = accessTokenAppService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<AccessTokenResponseDto> GetAccessTokenAsync(AccessTokenRequestDto requestDto)
        {
            return await _accessTokenAppService.GetAccessTokenAsync(requestDto);
        }

        [HttpPost("Refresh")]
        public async Task<AccessTokenResponseDto> GetRefreshAccessTokenAsync(string refreshToken)
        {
            return await _accessTokenAppService.GetRefreshAccessTokenAsync(refreshToken);
        }

        [AllowAnonymous]
        [HttpPost("Swagger")]
        public async Task<AccessTokenResponseDto> GetSwaggerAccessTokenAsync(AccessTokenRequestDto requestDto)
        {
            return await _accessTokenAppService.GetSwaggerAccessTokenAsync(requestDto);
        }

        [Route("claims")]
        [HttpGet]
        public dynamic GetClaimsAsync()
        {
            return CurrentUser.GetAllClaims().Select(a => new { a.Type, a.Value });
        }

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
