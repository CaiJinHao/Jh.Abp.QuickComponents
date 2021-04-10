using Jh.Abp.Application.Contracts.Extensions;
using Jh.Abp.MenuManagement.Menus;
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
        [HttpDelete]
        public async Task DeleteAsync([FromQuery]AuditLoggingDeleteInputDto deleteInputDto)
        {
            await auditLoggingAppService.DeleteAsync(deleteInputDto);
        }

        /// <summary>
        /// 根据主键删除多条
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        [Route("keys")]
        [HttpDelete]
        public async Task DeleteAsync([FromBody] Guid[] keys)
        {
            await auditLoggingAppService.DeleteAsync(keys);
        }

        /// <summary>
        /// 根据条件分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<AuditLog>> GetListAsync([FromQuery] AuditLoggingRetrieveInputDto input)
        {
            return await auditLoggingAppService.GetListAsync(input,true);
        }

        /// <summary>
        /// 根据条件查询(不分页)
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [Route("all")]
        [HttpGet]
        public async Task<ListResultDto<AuditLog>> GetEntitysAsync([FromQuery] AuditLoggingRetrieveInputDto inputDto)
        {
            return await auditLoggingAppService.GetEntitysAsync(inputDto, true);
        }

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await auditLoggingAppService.DeleteAsync(id);
        }
    }
}
