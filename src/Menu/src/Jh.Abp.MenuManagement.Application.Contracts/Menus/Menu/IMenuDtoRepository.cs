using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jh.Abp.MenuManagement.Menus
{
    public interface IMenuDtoRepository
    {
        Task<IEnumerable<MenuDto>> GetDtoDapperListAsync();
    }
}
