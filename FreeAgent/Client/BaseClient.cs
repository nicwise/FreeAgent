using System;
using FreeAgent.Helpers;
using FreeAgent.Models;
using System.Collections.Generic;
using RestSharp;

namespace FreeAgent
{
	public class BaseClient
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
	}
}

