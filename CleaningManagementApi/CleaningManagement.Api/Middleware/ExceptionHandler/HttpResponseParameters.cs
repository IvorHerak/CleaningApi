namespace CleaningManagement.Api.Middleware.ExceptionHandler
{
    public class HttpResponseParameters
    {
        public int StatusCode { get; set; }

        public string Content { get; set; }

        public string ContentType { get; set; }
    }
}
