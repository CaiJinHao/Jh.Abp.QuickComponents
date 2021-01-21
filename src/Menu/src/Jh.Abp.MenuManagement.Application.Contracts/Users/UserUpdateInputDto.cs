using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.Abp.MenuManagement.Users
{
    public class UserUpdateInputDto
    {
        /// <summary>
        /// 老密码 用来验证是否正确
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        public string PasswordNew { get; set; }
    }
}
