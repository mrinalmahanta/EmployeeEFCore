using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace EmployeeApi.Utitlities
{
    public static class ControllersExtension
    {
        public static void AddControllersConfig(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {              
                options.Filters.Add(typeof(ValidateModelAttribute));
                options.RespectBrowserAcceptHeader = true;
            })
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.Converters.Add(new StringEnumConverter
                    { NamingStrategy = new CamelCaseNamingStrategy() }))
                .AddXmlDataContractSerializerFormatters();
        }
    }

    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid == false)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
