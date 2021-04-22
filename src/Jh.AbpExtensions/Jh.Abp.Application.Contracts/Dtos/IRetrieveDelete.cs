using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.Abp.Application.Contracts.Dtos
{
    public interface IRetrieveDelete
    {
        /// <summary>
        /// 0:all,1:true,2:false
        /// </summary>
        int? Deleted { get; set; }
    }
}
