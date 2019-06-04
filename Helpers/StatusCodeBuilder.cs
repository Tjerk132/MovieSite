using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Logic
{
    public class StatusCodeBuilder
    {
        public string GetStatusCodeTitle(int? StatusCode)
        {
            switch(StatusCode)
            {
                case 400:
                    return "Bad Request";
                case 401:
                    return "Unauthorized";
                case 403:
                    return "Forbidden";
                case 404:
                    return "Not Found";
                case 500:
                    return "Internal Server Error";
                case 502:
                    return "Bad Gateway";
                case 503:
                    return "Service Unavailable";
                case 504:
                    return "Gateway Timeout";
                default:
                    return "Something unexpected happened";
            }
        }
        public string GetStatusCodeInfo(int? StatusCode)
        {
            switch (StatusCode)
            {
                case 400:
                    return "The request could not be understood by the server due to malformed syntax.";
                case 401:
                    return "The request requires user authentication. The response MUST include a WWW-Authenticate header field containing a challenge applicable to the requested resource.";
                case 403:
                    return "The server understood the request, but is refusing to fulfill it. Authorization will not help.";
                case 404:
                    return "The server has not found anything matching the Request-URI. No indication is given of whether the condition is temporary or permanent.";
                case 500:
                    return "The server encountered an unexpected condition which prevented it from fulfilling the request.";
                case 502:
                    return "One server on the internet received an invalid response from another server.";
                case 503:
                    return "The web server is unable to handle your HTTP request at the time.";
                case 504:
                    return "The server was acting as a gateway or proxy and did not receive a timely response from the upstream server.";
                default:
                    return "We're not sure what causes the problem";
            }
        }
    }
}
