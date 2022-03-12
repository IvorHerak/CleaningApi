using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace CleaningManagement.Api.Middleware.ExceptionToHttpResponseMapper
{
    public class ExceptionToHttpResponseMapperMiddleware
    {
        private readonly RequestDelegate next;

        private readonly IExceptionToHttpResponseParametersMapper exceptionToHttpStatusCodeMapper;

        public ExceptionToHttpResponseMapperMiddleware
        (
            RequestDelegate next,
            IExceptionToHttpResponseParametersMapper exceptionToHttpStatusCodeMapper
        )
        {
            this.next = next;
            this.exceptionToHttpStatusCodeMapper = exceptionToHttpStatusCodeMapper;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                var responseParameters = exceptionToHttpStatusCodeMapper.GetHttpResponseParametersForException(exception);
                await MapExceptionToHttpResponse(context, responseParameters);
            }
        }

        private static async Task MapExceptionToHttpResponse(HttpContext context, HttpResponseParameters parameters)
        {
            context.Response.StatusCode = parameters.StatusCode;
            context.Response.ContentType = parameters.ContentType;
            await context.Response.WriteAsync(parameters.Content);
        }
    }
}
