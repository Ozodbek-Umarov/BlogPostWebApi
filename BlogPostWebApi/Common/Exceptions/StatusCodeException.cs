using System.Net;

namespace BlogPostWebApi.Common.Exceptions;

public class StatusCodeException : Exception
{
    public new string Message { get; set; }
    public HttpStatusCode  StatusCode { get; set; }

    public StatusCodeException(HttpStatusCode statusCode, string message)
        : base(message)
    {
        Message = message;
        StatusCode = statusCode;
    }
}