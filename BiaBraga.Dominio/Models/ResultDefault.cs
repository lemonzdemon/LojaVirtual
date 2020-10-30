using System.Net;

namespace BiaBraga.Domain.Models
{
    public class ResultDefault
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Message { get; set; }
        public dynamic Entity { get; set; }
    }
}
