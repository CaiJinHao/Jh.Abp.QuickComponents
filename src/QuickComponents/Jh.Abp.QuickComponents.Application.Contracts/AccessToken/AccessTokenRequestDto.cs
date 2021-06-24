using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Auditing;

namespace Jh.Abp.QuickComponents.AccessToken
{
    public class AccessTokenRequestDto
    {
        /// <summary>
        /// 用户名或者邮箱地址
        /// </summary>
        [Required]
        [StringLength(255)]
        public string UserNameOrEmailAddress { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [StringLength(32)]
        [DataType(DataType.Password)]
        [DisableAuditing]
        public string Password { get; set; }

        public string OrganizationName { get; set; }
    }
}
