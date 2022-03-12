using System;

namespace CleaningManagement.Api.Middleware.ExceptionToHttpResponseMapper
{
    public interface IExceptionToHttpResponseParametersMapper
    {
        public HttpResponseParameters GetHttpResponseParametersForException(Exception e);
    }
}
