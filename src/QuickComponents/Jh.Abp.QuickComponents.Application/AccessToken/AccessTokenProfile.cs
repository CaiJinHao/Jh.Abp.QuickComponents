using AutoMapper;
using IdentityModel.Client;
using Jh.Abp.QuickComponents.AccessToken;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AutoMapper;

namespace YourProjectName.AccessToken
{
    public class AccessTokenProfile:Profile
    {
        public AccessTokenProfile()
        { 
            //身份验证服务器响应
            CreateMap<TokenResponse, AccessTokenResponseDto>();
        }
    }
}
