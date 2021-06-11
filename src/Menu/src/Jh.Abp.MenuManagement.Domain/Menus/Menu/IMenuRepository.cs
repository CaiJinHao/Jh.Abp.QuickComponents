using Jh.Abp.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jh.Abp.MenuManagement
{
    public interface IMenuRepository : ICrudRepository<Menu, Guid>
    {
        Task<IEnumerable<Menu>> GetDapperListAsync();
    }
}
