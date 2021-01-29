using Jh.SourceGenerator.Common.GeneratorDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.SourceGenerator.Common.CodeBuilders
{
    public abstract class CodeBuilderAbs
    {
        protected TableDto table { get; }
        public CodeBuilderAbs(TableDto tableDto)
        {
            //用构造函数传值
            table = tableDto;
        }
    }
}
