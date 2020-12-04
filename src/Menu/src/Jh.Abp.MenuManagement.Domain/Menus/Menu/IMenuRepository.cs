using Jh.Abp.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.Abp.MenuManagement.Menus
{
    public interface IMenuRepository : ICrudRepository<Menu, Guid>
    {
    }
}
