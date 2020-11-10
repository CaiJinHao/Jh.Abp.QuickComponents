using Jh.Abp.QuickComponents.AccessToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;

namespace Jh.Abp.QuickComponents.HttpApi.AccessToken
{
    [RemoteService]
    [Route("api/v1/AccessToken")]
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
        public async Task<dynamic> GetSwaggerAccessTokenAsync(AccessTokenRequestDto requestDto)
        {
            return await _accessTokenAppService.GetSwaggerAccessTokenAsync(requestDto);
        }
    }
}
