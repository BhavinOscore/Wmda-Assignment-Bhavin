using Microsoft.AspNetCore.Builder;

namespace WMDAApi.Utils
{
    public static class ExtensionMethods
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
