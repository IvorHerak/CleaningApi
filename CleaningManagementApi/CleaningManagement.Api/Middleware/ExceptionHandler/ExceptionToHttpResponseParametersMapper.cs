using CleaningManagement.Api.Infrastructure;
using CleaningManagement.BLL.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace CleaningManagement.Api.Middleware.ExceptionHandler
{
    public class ExceptionToHttpResponseParametersMapper : IExceptionToHttpResponseParametersMapper
    {
        private static readonly JsonSerializerOptions SerializationOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private const string NotFoundErrorMessage = "Requested resource was not found.";

        public HttpResponseParameters GetHttpResponseParametersForException(Exception exception)
        {
            if (exception is KeyNotFoundException)
            {
                return new HttpResponseParameters
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    ContentType = HttpResponseContentTypes.ApplicationJson,
                    Content = JsonSerializer.Serialize(new ErrorDetails { Message = NotFoundErrorMessage }, SerializationOptions)
                };
            }
            else if (exception is BusinessException bex)
            {
                return new HttpResponseParameters
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    ContentType = HttpResponseContentTypes.ApplicationJson,
                    Content = JsonSerializer.Serialize(new ErrorDetails(bex), SerializationOptions)
                };
            }
            else
            {
                return new HttpResponseParameters
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    ContentType = HttpResponseContentTypes.ApplicationJson,
                    Content = JsonSerializer.Serialize(new ErrorDetails(), SerializationOptions)
                };
            }
        }
    }
}
