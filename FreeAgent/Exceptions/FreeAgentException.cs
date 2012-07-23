using System;
using RestSharp;
using System.Net;
using System.Collections.Generic;

namespace FreeAgent.Exceptions
{
    public class FreeAgentException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// The response of the error call (for Debugging use)
        /// </summary>
        public IRestResponse Response { get; private set; }

        public FreeAgentException() : base()
        {
        }

        public FreeAgentException(string message)
            : base(message)
        {

        }

        public FreeAgentException(IRestResponse r) : base()
        {
            Response = r;
            StatusCode = r.StatusCode;

            try
            {
                var json = Response.Content;
                if (json.Contains("\"errors\""))
                {
                    Errors = json;
                }
            } catch {
                //do nothing
            }

        }

        public string Errors = "";

        public override string ToString()
        {
            return string.Format("[FreeAgentException: StatusCode={0}, Response={1}, Content={2}]", StatusCode, Response, Response.Content);
        }

    }

}
