using System;

namespace CleaningManagement.Api.Middleware.ExceptionHandler
{
    public interface IExceptionToHttpResponseParametersMapper
    {
        public HttpResponseParameters GetHttpResponseParametersForException(Exception e);
    }
}
