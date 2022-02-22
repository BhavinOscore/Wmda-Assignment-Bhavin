using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace WMDAApi.Utils
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (CustomException ex)
            {
                await HandleExceptionAsync(context, ex.Errors).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, new ErrorData() { Message = ex.Message }).ConfigureAwait(false);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, List<ErrorData> errors)
        {
            context.Response.ContentType = "application/json";
            var statusCode = (int)HttpStatusCode.BadRequest;
            var result = JsonConvert.SerializeObject(new { Errors = errors });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }

        private static Task HandleExceptionAsync(HttpContext context, ErrorData error)
        {
            var errors = new List<ErrorData>();
            errors.Add(error);
            return HandleExceptionAsync(context, errors);
        }
    }
}
