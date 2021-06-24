using IdentityModel;
using IdentityModel.Client;
using Jh.Abp.QuickComponents.AccessToken;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.IdentityModel;
using Volo.Abp.ObjectMapping;

namespace Jh.Abp.QuickComponents.Application.AccessToken
{
    public class AccessTokenAppService : IAccessTokenAppService, ITransientDependency
    {
        public IObjectMapper _objectMapper { get; set; }
        protected IJhIdentityModelAuthenticationService _jhIdentityModelAuthenticationService { get; }
        protected readonly IdentityClientOptions _identityClientOptions;
        public AccessTokenAppService(
            IJhIdentityModelAuthenticationService jhIdentityModelAuthenticationService,
             IOptions<IdentityClientOptions> identityClientOptions
            )
        {
            _jhIdentityModelAuthenticationService = jhIdentityModelAuthenticationService;
            _identityClientOptions = identityClientOptions.Value;
        }


        public async Task<AccessTokenResponseDto> GetAccessTokenAsync(AccessTokenRequestDto requestDto)
        {
            var configuration = new IdentityClientConfiguration(
                _identityClientOptions.Authority,
                _identityClientOptions.Scope,
                _identityClientOptions.ClientId,
                _identityClientOptions.ClientSecret,
                OidcConstants.GrantTypes.Password,
                requestDto.UserNameOrEmailAddress,
                requestDto.Password
            )
            {
                RequireHttps = _identityClientOptions.RequireHttps
            };

            if (!requestDto.OrganizationName.IsNullOrWhiteSpace())
            {
                configuration["[o]abp-organization-name"] = requestDto.OrganizationName;
            }
            var tokenResponse= await _jhIdentityModelAuthenticationService.GetAccessTokenResponseAsync(configuration);
            return _objectMapper.Map<TokenResponse, AccessTokenResponseDto>(tokenResponse);
        }

        public Task<AccessTokenResponseDto> GetRefreshAccessTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
