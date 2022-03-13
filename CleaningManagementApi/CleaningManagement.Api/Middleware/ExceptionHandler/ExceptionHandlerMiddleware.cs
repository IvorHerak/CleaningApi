using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CleaningManagement.Api.Middleware.ExceptionHandler
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IExceptionToHttpResponseParametersMapper exceptionToHttpStatusCodeMapper;
        private readonly ILogger<ExceptionHandlerMiddleware> logger;

        public ExceptionHandlerMiddleware
        (
            RequestDelegate next,
            IExceptionToHttpResponseParametersMapper exceptionToHttpStatusCodeMapper,
            ILogger<ExceptionHandlerMiddleware> logger
        )
        {
            this.next = next;
            this.exceptionToHttpStatusCodeMapper = exceptionToHttpStatusCodeMapper;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, exception.Message);
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
