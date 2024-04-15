using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace Logging
{
    public static class ControllerExtension
    {
        public static string GetSmeTraceId(this ControllerBase controller)
        {
            return controller.HttpContext.Request.Headers.TryGetValue("Sme-Trace-Id", out StringValues values)
                ? values.ToString()
                : string.Empty;
        }
    }
}
