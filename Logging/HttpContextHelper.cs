using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace Logging
{
    public class HttpContextHelper
    {
        public static string GetSmeTraceId(HttpContext? httpContext)
        {
            if (httpContext == null)
            {
                return string.Empty;
            }
            return httpContext.Request.Headers.TryGetValue("Sme-Trace-Id", out StringValues values)
                ? values.ToString()
                : string.Empty;
        }
    }
}
