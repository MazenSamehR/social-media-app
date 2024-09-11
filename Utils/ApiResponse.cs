using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SocailMediaApp.Utils
{
    public class ApiResponse<T>
    {
        public T Body { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
