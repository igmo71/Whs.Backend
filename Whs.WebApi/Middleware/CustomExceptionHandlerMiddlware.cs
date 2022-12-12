using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Whs.Application.Common.Exceptions;

namespace Whs.WebApi.Middleware
{
    public class CustomExceptionHandlerMiddlware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddlware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(httpContext, exception);
                throw;
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            //var result = string.Empty;
            var result = JsonSerializer.Serialize(new { error = exception.Message });

            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationException.Errors);
                    break;
                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
                //default:
                //    result = JsonSerializer.Serialize(new { error = exception.Message });
                //    break;
            }

            //if (string.IsNullOrEmpty(result))
            //{
            //    result = JsonSerializer.Serialize(new { error = exception.Message });
            //}

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)code;            

            return httpContext.Response.WriteAsync(result);
        }
    }
}
