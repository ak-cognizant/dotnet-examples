using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;

namespace Logging
{
    public static class LoggerExtension
    {
        public static bool IsEnabled { get; set; }
        public static void LogInfo<T>(this ILogger<T> logger, string traceId, string methodName, string msg)
        {
            if (string.IsNullOrWhiteSpace(msg) || !IsEnabled)
            {
                return;
            }

            string className = GetClassName(logger);
            logger.LogInformation($"{className}.{methodName} --- {traceId} --- {msg}");
        }

        public static void LogRequest<T>(this ILogger<T> logger, string traceId, string methodName, string apiName, object request)
        {
            string reqStr = string.Empty;
            if (request != null)
            {
                reqStr = JsonConvert.SerializeObject(request);
            }

            string message = $"{apiName} API Gateway Call with request: {reqStr}";
            logger.LogInfo(traceId, methodName, message);
        }

        public static void LogResponse<T>(this ILogger<T> logger, string traceId, string methodName, string apiName, HttpStatusCode statusCode, object? response)
        {
            string resStr = string.Empty;
            if (response != null)
            {
                resStr = JsonConvert.SerializeObject(response);
            }

            string message = $"{apiName} API Gateway Call with response({statusCode}): {resStr}";
            logger.LogInfo(traceId, methodName, message);
        }

        
        // Get ClassName only
        private static string GetClassName<T>(ILogger<T> logger)
        {
            var className = string.Empty;
            var fullName = logger.GetType().FullName;
            
            if (!string.IsNullOrWhiteSpace(fullName))
            {
                var regex = @"\[\[[\w+\.\w+]+";
                var result = Regex.Match(fullName, regex);
                if (result != null && result.Success)
                {
                    className = result.Value;
                    className = className.Split(".").Last();
                }
            }
            
            return className;
        }

        
        // Get ClassName and MethodName from System.Diagnostics.StackTrace
        private static void GetClassAndMethodNames(out string className, out string methodName)
        {
            var methodInfo = new StackTrace().GetFrame(2)?.GetMethod();
            var callingMethodName = methodInfo?.ReflectedType?.Name;
            var callingClassName = methodInfo?.ReflectedType?.DeclaringType?.Name;
            className = callingClassName ?? string.Empty;
            methodName = string.Empty;
            if (!string.IsNullOrWhiteSpace(callingMethodName))
            {
                var pattern = @"<.+>";
                var result = Regex.Match(callingMethodName, pattern);
                if (result.Success) methodName = result.Value;
                methodName = methodName.Replace("<", string.Empty);
                methodName = methodName.Replace(">", string.Empty);
            }
        }
    }
}
