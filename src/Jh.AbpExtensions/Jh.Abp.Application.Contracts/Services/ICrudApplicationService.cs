using Jh.Abp.Application.Contracts.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Jh.Abp.Extensions
{
    /// <summary>
    /// 应用程序服务继承
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TPagedRetrieveOutputDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TRetrieveInputDto"></typeparam>
    /// <typeparam name="TCreateInputDto"></typeparam>
    /// <typeparam name="TUpdateInputDto"></typeparam>
    /// <typeparam name="TDeleteInputDto"></typeparam>
    public interface ICrudApplicationService<TEntity, TEntityDto, TPagedRetrieveOutputDto, in TKey, in TRetrieveInputDto, in TCreateInputDto, in TUpdateInputDto, in TDeleteInputDto>
    : ICrudAppService<TEntityDto, TPagedRetrieveOutputDto, TKey, TRetrieveInputDto, TCreateInputDto, TUpdateInputDto>
    {
        /// <summary>
        /// 创建一个实体[HttpPost]
        /// </summary>
        /// <param name="inputDto"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> CreateAsync(TCreateInputDto inputDto, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 创建多个实体[Route("list")][HttpPost]
        /// </summary>
        /// <param name="inputDtos"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity[]> CreateAsync(TCreateInputDto[] inputDtos, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 根据条件删除多条[HttpDelete]
        /// </summary>
        /// <param name="deleteInputDto"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity[]> DeleteAsync(TDeleteInputDto deleteInputDto, string methodStringType = ObjectMethodConsts.EqualsMethod, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 根据主键删除多条[Route("keys")][HttpDelete]
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity[]> DeleteAsync(TKey[] keys, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 根据主键更新部分[HttpPatch]
        /// </summary>
        /// <param name="key"></param>
        /// <param name="inputDto">更新无法自动赋值的字段时可传null</param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> UpdatePortionAsync(TKey key, TUpdateInputDto inputDto, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 根据条件查询(不分页)[Route("list")][HttpGet]
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        Task<ListResultDto<TEntityDto>> GetEntitysAsync(TRetrieveInputDto inputDto, string methodStringType = ObjectMethodConsts.ContainsMethod, bool includeDetails = false, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 重写分页列表 methodStringType
        /// </summary>
        /// <param name="input"></param>
        /// <param name="methodStringType"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PagedResultDto<TPagedRetrieveOutputDto>> GetListAsync(TRetrieveInputDto input, string methodStringType = ObjectMethodConsts.ContainsMethod, bool includeDetails = false, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 获取entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntityDto> GetAsync(TKey id, bool includeDetails = false, CancellationToken cancellationToken = default(CancellationToken));
    }
}
