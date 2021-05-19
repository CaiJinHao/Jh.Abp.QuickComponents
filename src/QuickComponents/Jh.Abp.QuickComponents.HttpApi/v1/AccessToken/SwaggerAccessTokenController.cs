using Jh.Abp.QuickComponents.AccessToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Jh.Abp.QuickComponents.HttpApi.v1.AccessToken
{
    [Route("api/v{apiVersion:apiVersion}/[controller]")]
    public class SwaggerAccessTokenController : JhAbpQuickComponentsController
    {
        private readonly SwaggerClientOptions _swaggerClientOptions;
        public SwaggerAccessTokenController(
            IOptions<SwaggerClientOptions> swaggerClientOptions
            )
        { 
            _swaggerClientOptions = swaggerClientOptions.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        public virtual AccessTokenResponseDto GetSwaggerAccessTokenAsync(AccessTokenRequestDto requestDto)
        {
            if (string.Equals(requestDto.UserNameOrEmailAddress, _swaggerClientOptions.UserNameOrEmailAddress)
                && string.Equals(requestDto.Password, _swaggerClientOptions.Password))
            {
                return new AccessTokenResponseDto()
                {
                    AccessToken = Guid.NewGuid().ToString("n"),
                    ExpiresIn = DateTime.Now.Millisecond
                };
            }
            else
            {
                //2者实现是一样的，一个需要DI注入异步不需要
                throw new BusinessException("JhAbpQuickComponents:000001").WithData("UserNameOrEmailAddress", requestDto.UserNameOrEmailAddress);
                //throw new UserFriendlyException(_stringLocalizer["JhAbpQuickComponents:000001", requestDto.UserNameOrEmailAddress], "000001");
            }
        }

        [Route("claims")]
        [HttpGet]
        public dynamic GetClaimsAsync()
        {
            return CurrentUser.GetAllClaims().Select(a => new { a.Type, a.Value });
        }
    }
}
