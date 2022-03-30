using System.Net;

namespace EmployeeApi.DataAccess
{
    public class ResponseObject
    {
        /// <summary>
        /// Creates a new <see cref="ResponseObject"/> instance
        /// </summary>
        public ResponseObject() { }

        /// <summary>
        /// Creates a new <see cref="ResponseObject"/> instance
        /// </summary>
        /// <param name="statusCode">Sets the <see cref="StatusCode"/> property exposed by this class</param>
        public ResponseObject(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// The <see cref="HttpStatusCode"/>
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// The message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The actual response object that should be returned
        /// </summary>
        public object Result { get; set; }
    }
}
