using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Jh.Abp.Extensions
{
    /// <summary>
    /// 应用程序服务继承
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TPagedRetrieveInputDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TRetrieveInputDto"></typeparam>
    /// <typeparam name="TCreateInputDto"></typeparam>
    /// <typeparam name="TUpdateInputDto"></typeparam>
    /// <typeparam name="TDeleteInputDto"></typeparam>
    public interface ICrudApplicationService<TEntity, TEntityDto, TPagedRetrieveInputDto, in TKey, in TRetrieveInputDto, in TCreateInputDto, in TUpdateInputDto, in TDeleteInputDto>
    : ICrudAppService<TEntityDto, TPagedRetrieveInputDto, TKey, TRetrieveInputDto, TCreateInputDto, TUpdateInputDto>
    {
        /// <summary>
        /// 通过对象条件删除,TODO:还没有写完
        /// </summary>
        /// <param name="deleteInput"></param>
        /// <returns></returns>
        Task DeleteAsync(TDeleteInputDto deleteInputDto);

        //[Route("keys")]
        //[HttpDelete]
        /// <summary>
        /// 删除多条主键数据
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task DeleteAsync(TKey[] keys);

        /// <summary>
        /// 更新部分有值更新没有值则不更新
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        Task<TEntity> UpdatePortionAsync(TKey key, TUpdateInputDto inputDto);

        //[Route("1")]
        //[HttpGet]
        /// <summary>
        /// 检索当前表数据 TODO:还没有写完
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<TEntityDto>> GetModelsAsync(TRetrieveInputDto inputDto);
    }
}
