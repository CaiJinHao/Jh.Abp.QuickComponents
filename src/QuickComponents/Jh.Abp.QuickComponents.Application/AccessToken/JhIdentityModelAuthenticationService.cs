using IdentityModel;
using IdentityModel.Client;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Threading;
using Volo.Abp.IdentityModel;
using Volo.Abp;
using Jh.Abp.QuickComponents.AccessToken;

namespace Jh.Abp.QuickComponents.Application.AccessToken
{
    public class JhIdentityModelAuthenticationService : IdentityModelAuthenticationService, IJhIdentityModelAuthenticationService, ITransientDependency
    {
        public JhIdentityModelAuthenticationService(IOptions<AbpIdentityClientOptions> options, ICancellationTokenProvider cancellationTokenProvider, IHttpClientFactory httpClientFactory, ICurrentTenant currentTenant, IOptions<IdentityModelHttpRequestMessageOptions> identityModelHttpRequestMessageOptions, IDistributedCache<IdentityModelTokenCacheItem> tokenCache, IDistributedCache<IdentityModelDiscoveryDocumentCacheItem> discoveryDocumentCache) : base(options, cancellationTokenProvider, httpClientFactory, currentTenant, identityModelHttpRequestMessageOptions, tokenCache, discoveryDocumentCache)
        {
        }

        public virtual async Task<TokenResponse> GetAccessTokenResponseAsync(IdentityClientConfiguration configuration, string refreshToken = null)
        {
            TokenResponse tokenResponse;
            switch (configuration.GrantType)
            {
                case OidcConstants.GrantTypes.RefreshToken:
                    tokenResponse = await GetRefreshAccessTokenAsync(configuration, refreshToken);
                    break;
                default:
                    tokenResponse = await GetTokenResponse(configuration);
                    break;
            }

            if (tokenResponse.IsError)
            {
                if (tokenResponse.ErrorDescription != null)
                {
                    throw new UserFriendlyException(tokenResponse.ErrorDescription);
                }
                if (tokenResponse.Error!=null)
                {
                    throw new UserFriendlyException(tokenResponse.Error);
                }
                var rawError = tokenResponse.Raw;
                if (rawError != null)
                {
                    var withoutInnerException = rawError.Split(new string[] { "<eof/>" }, StringSplitOptions.RemoveEmptyEntries);
                    throw new AbpException(withoutInnerException[0]);
                }

                throw new BusinessException("JhAbpQuickComponents:000001");
            }
            return tokenResponse;
        }

        protected virtual async Task<TokenResponse> GetRefreshAccessTokenAsync(IdentityClientConfiguration configuration, string refreshToken)
        {
            var tokenEndpoint = await GetTokenEndpoint(configuration);

            using (var httpClient = HttpClientFactory.CreateClient(HttpClientName))
            {
                AddHeaders(httpClient);

                return await httpClient.RequestRefreshTokenAsync(
                            await CreateRefreshTokenRequestAsync(tokenEndpoint, configuration, refreshToken),
                            CancellationTokenProvider.Token
                        );
            }
        }

        protected virtual Task<RefreshTokenRequest> CreateRefreshTokenRequestAsync(string tokenEndpoint, IdentityClientConfiguration configuration,string refreshToken)
        {
            var request = new RefreshTokenRequest
            {
                Address = tokenEndpoint,
                Scope = configuration.Scope,
                ClientId = configuration.ClientId,
                ClientSecret = configuration.ClientSecret,
                GrantType = OidcConstants.GrantTypes.RefreshToken,
                RefreshToken = refreshToken
            };
            IdentityModelHttpRequestMessageOptions.ConfigureHttpRequestMessage?.Invoke(request);

            AddParametersToRequestAsync(configuration, request);

            return Task.FromResult(request);
        }
    }
}
