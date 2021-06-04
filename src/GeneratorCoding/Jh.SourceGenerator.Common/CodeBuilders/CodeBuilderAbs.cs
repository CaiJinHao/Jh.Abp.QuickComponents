using Jh.SourceGenerator.Common.GeneratorDtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Jh.SourceGenerator.Common.CodeBuilders
{
    public abstract class CodeBuilderAbs
    {
        public string FilePath { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        public string Suffix { get; set; } = ".cs";
        protected TableDto table { get; set; }
        protected IEnumerable<TableDto> tables { get; }
        public CodeBuilderAbs(TableDto tableDto,string filePath)
        {
            //用构造函数传值
            table = tableDto;
            if (!string.IsNullOrEmpty(filePath))
            {
                FilePath = Path.Combine(filePath, table.Name);//以表名称为上级文件名创建路径
            }
        }

        public CodeBuilderAbs(IEnumerable<TableDto> tableDto, string filePath)
        {
            //用构造函数传值
            tables = tableDto;
            table = tables.FirstOrDefault();
            FilePath = filePath;
        }
    }
}
