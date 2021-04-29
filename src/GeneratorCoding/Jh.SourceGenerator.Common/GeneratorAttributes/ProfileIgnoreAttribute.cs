using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.SourceGenerator.Common.GeneratorAttributes
{
    /// <summary>
    /// Profile 中Ignore  
    /// 只在DeleteInputDto和RetrieveInputDto中忽略
    /// 只要不包含RetrieveDtoAttribute就会自动Ignore
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ProfileIgnoreAttribute: Attribute
    {
    }
}
