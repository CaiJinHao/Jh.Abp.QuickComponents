using Jh.Abp.Domain.Extensions;
using System;

namespace Jh.Abp.MenuManagement.Menus
{
    public interface IMenuRepository : ICrudRepository<Menu, Guid>
    {
    }
}
