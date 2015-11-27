using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmIndustryNetwork.Utilities
{
    public class NoMyApiFilmsResponseException : Exception
    {
        public string UrlOfResponse { get; set; }

        public NoMyApiFilmsResponseException(string urlOfResponse)
        {
            UrlOfResponse = urlOfResponse;
        }
    }

    public class MyApiFilmsTimeoutException : Exception
    {
        public string UrlOfRequest { get; set; }

        public MyApiFilmsTimeoutException(string urlOfRequest)
        {
            UrlOfRequest = urlOfRequest;
        }
    }
}
