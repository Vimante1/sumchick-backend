using System.Net;

namespace NovelsRanboeTranslates.Domain.Models
{
    public class Response<T>
    {
        public string Comment { get; set; }
        public T Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public Response(string comment, T result, HttpStatusCode statusCode)
        {
            Comment = comment;
            Result = result;
            this.StatusCode = statusCode;
        }
    }
}
