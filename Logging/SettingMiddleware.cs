using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Logging
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SettingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public SettingMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            #region AppSetting Approach
            LoggerExtension.IsEnabled = _configuration.GetValue<bool>("IsLoggingEnabled");
            #endregion

            #region DB Approach
            var dbContext = httpContext.RequestServices.GetRequiredService<AppDbContext>();
            var settings = await dbContext.SettingModels.ToListAsync();
            LoggerExtension.IsEnabled = SettingHelper.HasEnabled(settings, "IsLoggingEnabled");
            #endregion

            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SettingMiddlewareExtensions
    {
        public static IApplicationBuilder UseSettingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SettingMiddleware>();
        }
    }
}
