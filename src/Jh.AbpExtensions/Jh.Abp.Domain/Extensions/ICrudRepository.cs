using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Jh.Abp.Domain.Extensions
{
    public interface ICrudRepository<TEntity, TKey> : IRepository<TEntity, TKey>
         where TEntity : class, IEntity<TKey>
    {
        /// <summary>
        /// 创建一条数据
        /// </summary>
        /// <param name="entitys"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> CreateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 创建多条数据
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        Task<TEntity[]> CreateAsync(TEntity[] entitys, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="predicate">linq表达式</param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity[]> DeleteListAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="query"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity[]> DeleteEntitysAsync(IQueryable<TEntity> query, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> propertyQuerys,
            int maxResultCount = int.MaxValue,
            int skipCount = 0, bool includeDetails = false, CancellationToken cancellationToken = default(CancellationToken));
    }
}
