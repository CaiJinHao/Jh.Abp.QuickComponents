using Jh.Abp.Application.Contracts.Extensions;
using Jh.Abp.MenuManagement.Menus;
using Jh.Abp.MenuManagement.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.AuditLogging;
using Volo.Abp.Data;
namespace Jh.Abp.MenuManagement.v1
{
    [DisableAuditing]
    [RemoteService]
    [Route("api/v{apiVersion:apiVersion}/[controller]")]
    public class AuditLoggingController : MenuManagementController
    {
        public IAuditLoggingAppService auditLoggingAppService { get; set; }
        /// <summary>
        /// 根据条件删除多条
        /// </summary>
        /// <param name="deleteInputDto"></param>
        /// <returns></returns>
		[Authorize(MenuManagementPermissions.AuditLoggings.Delete)]
        [HttpDelete]
        public virtual async Task DeleteAsync([FromQuery]AuditLoggingDeleteInputDto deleteInputDto)
        {
            await auditLoggingAppService.DeleteAsync(deleteInputDto);
        }

        /// <summary>
        /// 根据主键删除多条
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
		[Authorize(MenuManagementPermissions.AuditLoggings.Delete)]
        [Route("keys")]
        [HttpDelete]
        public virtual async Task DeleteAsync([FromBody] Guid[] keys)
        {
            await auditLoggingAppService.DeleteAsync(keys);
        }

        /// <summary>
        /// 根据条件分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[Authorize(MenuManagementPermissions.AuditLoggings.Default)]
        [HttpGet]
        public virtual async Task<PagedResultDto<AuditLog>> GetListAsync([FromQuery] AuditLoggingRetrieveInputDto input)
        {
            return await auditLoggingAppService.GetListAsync(input,true);
        }

        /// <summary>
        /// 根据条件查询(不分页)
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
		[Authorize(MenuManagementPermissions.AuditLoggings.Default)]
        [Route("all")]
        [HttpGet]
        public virtual async Task<ListResultDto<AuditLog>> GetEntitysAsync([FromQuery] AuditLoggingRetrieveInputDto inputDto)
        {
            return await auditLoggingAppService.GetEntitysAsync(inputDto, true);
        }

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		[Authorize(MenuManagementPermissions.AuditLoggings.Delete)]
        [HttpDelete("{id}")]
        public virtual async Task DeleteAsync(Guid id)
        {
            await auditLoggingAppService.DeleteAsync(id);
        }
    }
}
