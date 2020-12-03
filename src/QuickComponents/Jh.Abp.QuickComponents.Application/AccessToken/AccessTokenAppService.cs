using IdentityModel;
using IdentityModel.Client;
using Jh.Abp.QuickComponents.AccessToken;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.IdentityModel;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Threading;

namespace JhAbpQuickComponents
{
    public class AccessTokenAppService : IdentityModelAuthenticationService, IAccessTokenAppService
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IdentityClientOptions _identityClientOptions;
        private readonly SwaggerClientOptions _swaggerClientOptions;
        public AccessTokenAppService(IObjectMapper objectMapper,
            IOptions<IdentityClientOptions> identityClientOptions,
            IOptions<SwaggerClientOptions> swaggerClientOptions,
            IOptions<AbpIdentityClientOptions> options, 
            ICancellationTokenProvider cancellationTokenProvider, 
            IHttpClientFactory httpClientFactory, 
            ICurrentTenant currentTenant,
            IOptions<IdentityModelHttpRequestMessageOptions> identityModelHttpRequestMessageOptions,
            IDistributedCache<IdentityModelTokenCacheItem> tokenCache,
            IDistributedCache<IdentityModelDiscoveryDocumentCacheItem> discoveryDocumentCache
            ) 
            : base(options, cancellationTokenProvider, httpClientFactory, currentTenant, identityModelHttpRequestMessageOptions, tokenCache,discoveryDocumentCache)
        {
            _identityClientOptions = identityClientOptions.Value;
            _swaggerClientOptions = swaggerClientOptions.Value;
            _objectMapper = objectMapper;
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
            );
            var tokenResponse = await GetTokenResponse(configuration);
            if (tokenResponse.IsError)
            {
                throw new BusinessException("JhAbpQuickComponents:000001").WithData("UserNameOrEmailAddress",requestDto.UserNameOrEmailAddress);
            }
            return _objectMapper.Map<TokenResponse, AccessTokenResponseDto>(tokenResponse);
        }

        public async Task<AccessTokenResponseDto> GetRefreshAccessTokenAsync(string refreshToken)
        {
            using (var httpClient = HttpClientFactory.CreateClient(HttpClientName))
            {
                AddHeaders(httpClient);
                var disc = await httpClient.GetDiscoveryDocumentAsync(_identityClientOptions.Authority);
                var tokenResponse = await httpClient.RequestRefreshTokenAsync(new RefreshTokenRequest()
                {
                    Address = disc.TokenEndpoint,
                    Scope= _identityClientOptions.Scope,
                    ClientId = _identityClientOptions.ClientId,
                    ClientSecret = _identityClientOptions.ClientSecret,
                    GrantType = OidcConstants.GrantTypes.RefreshToken,
                    RefreshToken = refreshToken,
                });
                if (tokenResponse.IsError)
                {
                    if (tokenResponse.ErrorType == ResponseErrorType.Exception)
                    {
                        throw new BusinessException("JhAbpQuickComponents:000003");
                    }
                    else
                    {
                        throw new BusinessException("JhAbpQuickComponents:000002");
                    }
                }
                return _objectMapper.Map<TokenResponse, AccessTokenResponseDto>(tokenResponse);
            }
        }


        public Task<AccessTokenResponseDto> GetSwaggerAccessTokenAsync(AccessTokenRequestDto requestDto)
        {
            if (string.Equals(requestDto.UserNameOrEmailAddress, _swaggerClientOptions.UserNameOrEmailAddress) 
                && string.Equals(requestDto.Password, _swaggerClientOptions.Password))
            {
                return Task.FromResult(new AccessTokenResponseDto()
                {
                    AccessToken = Guid.NewGuid().ToString("n"),
                    ExpiresIn = DateTime.Now.Millisecond
                });
            }
            else
            {
                //2者实现是一样的，一个需要DI注入异步不需要
                throw new BusinessException("JhAbpQuickComponents:000001").WithData("UserNameOrEmailAddress",requestDto.UserNameOrEmailAddress);
                //throw new UserFriendlyException(_stringLocalizer["JhAbpQuickComponents:000001", requestDto.UserNameOrEmailAddress], "000001");
            }
        }
    }
}
