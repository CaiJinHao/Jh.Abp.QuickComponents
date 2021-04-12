using Jh.Abp.Application.Contracts.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.Abp.Application.Contracts.Dtos
{
    public interface IMethodDto<TEntity>
    {
        /// <summary>
        /// app service method query dto
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        MethodDto<TEntity> MethodInput { get; set; }
    }
}
