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

namespace Jh.Abp.QuickComponents.Application.AccessToken
{
    public class JhIdentityModelAuthenticationService : IdentityModelAuthenticationService, IJhIdentityModelAuthenticationService, ITransientDependency
    {
        public JhIdentityModelAuthenticationService(IOptions<AbpIdentityClientOptions> options, ICancellationTokenProvider cancellationTokenProvider, IHttpClientFactory httpClientFactory, ICurrentTenant currentTenant, IOptions<IdentityModelHttpRequestMessageOptions> identityModelHttpRequestMessageOptions, IDistributedCache<IdentityModelTokenCacheItem> tokenCache, IDistributedCache<IdentityModelDiscoveryDocumentCacheItem> discoveryDocumentCache) : base(options, cancellationTokenProvider, httpClientFactory, currentTenant, identityModelHttpRequestMessageOptions, tokenCache, discoveryDocumentCache)
        {
        }

        public virtual async Task<TokenResponse> GetAccessTokenResponseAsync(IdentityClientConfiguration configuration)
        {
            var tokenResponse = await GetTokenResponse(configuration);

            if (tokenResponse.IsError)
            {
                if (tokenResponse.ErrorDescription != null)
                {
                    throw new AbpException($"Could not get token from the OpenId Connect server! ErrorType: {tokenResponse.ErrorType}. " +
                                           $"Error: {tokenResponse.Error}. ErrorDescription: {tokenResponse.ErrorDescription}. HttpStatusCode: {tokenResponse.HttpStatusCode}");
                }

                var rawError = tokenResponse.Raw;
                var withoutInnerException = rawError.Split(new string[] { "<eof/>" }, StringSplitOptions.RemoveEmptyEntries);
                throw new AbpException(withoutInnerException[0]);
            }
            return tokenResponse;
        }
    }
}
