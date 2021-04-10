using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Auditing;

namespace Jh.Abp.MenuManagement.v1
{
    [DisableAuditing]
    [RemoteService]
    [Route("api/v{apiVersion:apiVersion}/[controller]")]
    public class AppEnums : MenuManagementController
    {
        [Route("Use")]
        [HttpGet]
        public Task<IEnumerable<dynamic>> GetUseAsync()
        {
            return Task.FromResult(Common.Utils.UtilEnums.GetEnumListByDescription<UseType>());
        }
    }
}
