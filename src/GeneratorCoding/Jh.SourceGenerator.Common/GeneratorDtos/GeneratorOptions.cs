using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.SourceGenerator.Common.GeneratorDtos
{
    public class GeneratorOptions
    {
        /// <summary>
        /// domain 的dll文件路径用于加载该程序集
        /// </summary>
        public string DomainAssemblyDllFilePath { get; set; }
        /// <summary>
        /// Ef数据上下文类名
        /// </summary>
        public string DbContext { get; set; } = "MenuManagementDbContext";
        /// <summary>
        /// 项目顶级命名空间
        /// </summary>
        public string Namespace { get; set; } = "Jh.Abp.MenuManagement";
        /// <summary>
        /// 控制器基类
        /// </summary>
        public string ControllerBase { get; set; } = "MenuManagementController";

        //每个文件的生成路径
        /// <summary>
        /// Dots路径  上级文件夹表名称 CreateIAppService,CreateDomainDto,CreateCreateInputDto,CreateRetrieveInputDto,CreateDeleteInputDto，CreateUpdateInputDto
        /// </summary>
        public string CreateContractsPath { get; set; }
        public string CreateContractsPermissionsPath { get; set; }
        /// <summary>
        /// Domain路径 上级文件夹表名称 CreateIRepository
        /// </summary>
        public string CreateDomainPath { get; set; }
        /// <summary>
        /// EFCore 上级文件夹表名称 CreateRepository
        /// </summary>
        public string CreateEfCorePath { get; set; }
        /// <summary>
        /// Application 上级文件夹表名称 CreateProfile,CreateAppService
        /// </summary>
        public string CreateApplicationPath { get; set; }
        /// <summary>
        /// HttpApi 上级文件夹v1  CreateController
        /// </summary>
        public string CreateHttpApiPath { get; set; }
        /// <summary>
        /// Html 模板路径
        /// </summary>
        public string CreateHtmlTemplatePath { get; set; }
        /// <summary>
        /// 创建Html路径
        /// </summary>
        public string CreateHtmlPath { get; set; }
    }
}
