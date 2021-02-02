using Jh.SourceGenerator.Common.GeneratorDtos;
using System;
using System.Collections.Generic;
using System.Text;
using RazorEngine;
using RazorEngine.Templating;
using Jh.Abp.Common;
using System.IO;

namespace Jh.SourceGenerator.Common.CodeBuilders
{
    public class HtmlTemplateCodeBuilder : CodeBuilderAbs
    {
        protected string TemplateFilePath { get; set; }
        public HtmlTemplateCodeBuilder(string templateFilePath, TableDto tableDto, string filePath) : base(tableDto, filePath)
        {
            TemplateFilePath = templateFilePath;
            var file = new FileInfo(templateFilePath);
            this.FileName = file.Name.Replace(file.Extension,"");
            this.Suffix = ".html";
        }

        public override string ToString()
        {
            string razorTemplateContent = TemplateFilePath.ReadFile();
            return Engine.Razor.RunCompile(razorTemplateContent, FileName, null, table);
        }
    }
}
