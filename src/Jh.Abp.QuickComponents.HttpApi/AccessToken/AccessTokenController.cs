using Jh.Abp.QuickComponents.AccessToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;

namespace Jh.Abp.QuickComponents.HttpApi.AccessToken
{
    /*
     前端传的时候直接写死，SwaggerApi 参数化了
    [MapToApiVersion("1.0")] 对Action进行版本标记
     */
    [RemoteService(Name = JhAbpQuickComponentsRemoteServiceConsts.RemoteServiceName)]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
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
        public async Task<dynamic> GetAccessTokenAsync(AccessTokenRequestDto requestDto)
        {
            return await _accessTokenAppService.GetAccessTokenAsync(requestDto);
        }

        [HttpPost("Refresh")]
        public async Task<dynamic> GetRefreshAccessTokenAsync(string refreshToken)
        {
            return await _accessTokenAppService.GetRefreshAccessTokenAsync(refreshToken);
        }

        [AllowAnonymous]
        [HttpPost("Swagger")]
        public async Task<dynamic> GetSwaggerAccessTokenAsyncV1(AccessTokenRequestDto requestDto)
        {
            return await _accessTokenAppService.GetSwaggerAccessTokenAsync(requestDto);
        }

       /*
        Swagger文档不支持展示
        [HttpPost]
        [MapToApiVersion("1.0")]
        public Task<string> PostAsyncV1()
        {
            return PostAsync();
        }

        [HttpPost]
        [MapToApiVersion("2.0")]
        public Task<string> PostAsyncV2()
        {
            return PostAsync();
        }

        private Task<string> PostAsync()
        {
            return Task.FromResult($"Post-{HttpContext.GetRequestedApiVersion().ToString()}");
        }
       */
    }
}
