using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.SourceGenerator.Common.GeneratorAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CreateInputDtoAttribute : Attribute
    {
        public CreateInputDtoAttribute() { }
    }
}
