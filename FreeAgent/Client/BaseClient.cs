using System;
using FreeAgent.Helpers;
using FreeAgent.Models;
using System.Collections.Generic;
using RestSharp;

namespace FreeAgent
{
	public abstract class BaseClient
	{
		public FreeAgentClient Client;
		
		public string Version 
		{
			get
			{
				return FreeAgentClient.Version;
			}
		}
		
		public RequestHelper Helper 
		{
			get
			{
				return Client.Helper;
			}
		}
		
		public BaseClient(FreeAgentClient client)
		{
			Client = client;
		}
		
		protected void SetAuthentication(RestRequest request)
        {
            request.AddHeader("Authorization", "Bearer " + Client.CurrentAccessToken.access_token);
        }

        public virtual string ResouceName { get { return "unknown"; } } 

        public virtual void CustomizeAllRequest(RestRequest request)
        {
            //
        }

        public virtual RestRequest CreateBasicRequest(Method method, string appendToUrl = "")
        {
            var request = new RestRequest(method);
            request.Resource = "v{version}/{resource}" + appendToUrl;
            request.AddParameter("version", Version, ParameterType.UrlSegment);
            request.AddParameter("resource", ResouceName, ParameterType.UrlSegment);
            SetAuthentication(request);

            return request;
        }


	}
}

