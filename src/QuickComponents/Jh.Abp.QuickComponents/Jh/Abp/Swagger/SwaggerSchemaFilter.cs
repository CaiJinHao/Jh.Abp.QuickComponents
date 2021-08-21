using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jh.Abp.QuickComponents.Swagger
{
    public class SwaggerSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
        }
    }

    public class HideOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.MethodInfo == null) return;

            var controllerAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true);
            var actionAttributes = context.MethodInfo.GetCustomAttributes(true);

            // NOTE: When controller and action attributes are applicable, action attributes should take precendence.
            // Hence why they're at the end of the list (i.e. last one wins)
            var controllerAndActionAttributes = controllerAttributes.Union(actionAttributes);
            var list = controllerAndActionAttributes.OfType<RouteAttribute>().FirstOrDefault();
            if (controllerAndActionAttributes.OfType<RouteAttribute>().Any(a=>a.Template.Contains("Error")))
            {

            }

        }
    }

    public class HideDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // var controllerNamesAndAttributes = context.ApiDescriptions
            //.Select(apiDesc => apiDesc.ActionDescriptor as ControllerActionDescriptor)
            //.Where(a=>a.ControllerName.Contains("Error"))
            //.SkipWhile(actionDesc => actionDesc == null)
            //.GroupBy(actionDesc => actionDesc.ControllerName)
            //.Select(group => new KeyValuePair<string, IEnumerable<object>>(group.Key, group.First().ControllerTypeInfo.GetCustomAttributes(true)));

            // var error = context.ApiDescriptions
            //.Where(apiDesc => (apiDesc.ActionDescriptor as ControllerActionDescriptor).ControllerName.Contains("Error")).FirstOrDefault();

            swaggerDoc.Paths.RemoveAll(a=>a.Key.Contains("Error"));
        }
    }
}
