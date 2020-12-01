using System;
using System.Collections.Generic;
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
        /// 创建多条数据
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        Task<int> CreateAsync(TEntity[] entitys, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken));
    }
}
