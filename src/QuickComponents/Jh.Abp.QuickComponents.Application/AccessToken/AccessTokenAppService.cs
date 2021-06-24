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


        public virtual async Task<AccessTokenResponseDto> GetAccessTokenAsync(AccessTokenRequestDto requestDto)
        {
            var configuration = CreateIdentityClientConfiguration(requestDto.OrganizationName);
            configuration.GrantType = OidcConstants.GrantTypes.Password;
            configuration.UserName = requestDto.UserNameOrEmailAddress;
            configuration.UserPassword = requestDto.Password;
            
            var tokenResponse= await _jhIdentityModelAuthenticationService.GetAccessTokenResponseAsync(configuration);
            return _objectMapper.Map<TokenResponse, AccessTokenResponseDto>(tokenResponse);
        }

        public virtual async Task<AccessTokenResponseDto> GetRefreshAccessTokenAsync(string refreshToken, string organizationName = null)
        {
            var configuration = CreateIdentityClientConfiguration(organizationName);
            configuration.GrantType = OidcConstants.GrantTypes.RefreshToken;

            var tokenResponse = await _jhIdentityModelAuthenticationService.GetAccessTokenResponseAsync(configuration, refreshToken);
            return _objectMapper.Map<TokenResponse, AccessTokenResponseDto>(tokenResponse);
        }

        protected virtual IdentityClientConfiguration CreateIdentityClientConfiguration(string organizationName = null)
        {
            var configuration= new IdentityClientConfiguration(
                _identityClientOptions.Authority,
                _identityClientOptions.Scope,
                _identityClientOptions.ClientId,
                _identityClientOptions.ClientSecret
            )
            {
                RequireHttps = _identityClientOptions.RequireHttps
            };
            if (!organizationName.IsNullOrWhiteSpace())
            {
                configuration["[o]abp-organization-name"] = organizationName;
            }
            return configuration;
        }
    }
}
