using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.SourceGenerator.Common
{
    public class GeneratorConsts
    {
        public static string DbContext { get; set; } = "MenuManagementDbContext";
        public static string Namespace { get; set; } = "Jh.Abp.MenuManagement";
        public static string ControllerBase { get; set; } = "MenuManagementController";
    }

    public enum GneratorType
    {
        /// <summary>
        /// 根据特性生成
        /// </summary>
        AttributeField,
        /// <summary>
        /// 所有字段
        /// </summary>
        AllField,
    }
}
