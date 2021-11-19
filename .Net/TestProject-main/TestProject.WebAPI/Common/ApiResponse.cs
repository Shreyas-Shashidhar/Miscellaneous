using System.Net;

namespace TestProject.WebAPI.Common
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }

        public HttpStatusCode StatusCode;

        public object ErrorObject { get; set; }
    }
}
