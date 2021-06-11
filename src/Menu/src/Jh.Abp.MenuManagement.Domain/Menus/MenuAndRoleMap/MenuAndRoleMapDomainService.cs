using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Volo.Abp.Domain.Services;
using System.Linq;
using System.Threading.Tasks;

namespace Jh.Abp.MenuManagement
{
    public class MenuAndRoleMapDomainService: IMenuAndRoleMapDomainService,IDomainService
    {
        private readonly IMenuAndRoleMapRepository menuAndRoleMapRepository;
        public MenuAndRoleMapDomainService(IMenuAndRoleMapRepository repository)
        {
            menuAndRoleMapRepository = repository;
        }
    }
}
