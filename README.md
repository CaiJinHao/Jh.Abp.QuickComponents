# Jh.Abp.QuickComponents
Abp webapi项目需要使用的基础组件。Swagger、MIniProfiler、IdentityServer.

# Use
为所有Action添加授权验证
context.Services.AddAuthorizeFilter();
为本地化默认使用简体中文
app.UseRequestLocalization();修改为：app.UseJhRequestLocalization();
添加下列语句使用Swagger：app.UseSwaggerComponent(configuration, this.GetType()); 
接下来直接运行项目就可以了，其他使用跟平常一样
