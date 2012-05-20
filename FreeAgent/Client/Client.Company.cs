
using System.Collections.Generic;
using FreeAgent.Models;
using RestSharp;
using FreeAgent.Authenticators;
using System;

namespace FreeAgent
{
   	public class CompanyClient : BaseClient
	{
		public CompanyClient(FreeAgentClient client) : base(client) {}
		
		public Company Single ()
		{
			var request = CreateCompanyRequest();
            var response = Client.Execute<CompanyWrapper>(request);

            if (response != null) return response.company;

            return null;	
		
		}
		public List<TaxTimeline> TaxTimeline()
		{
			var request = CreateTaxTimelineRequest();
            var response = Client.Execute<TaxTimelineWrapper>(request);

            if (response != null) return response.timeline_items;

            return null;

		}
		
		public RestRequest CreateCompanyRequest()
        {
            var request = new RestRequest(Method.GET);
			request.Resource = "v{version}/company";
            request.AddParameter("version", Version, ParameterType.UrlSegment);
            SetAuthentication(request);
            return request;
        }

        public RestRequest CreateTaxTimelineRequest()
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "v{version}/company/tax_timeline";
            request.AddParameter("version", Version, ParameterType.UrlSegment);
            SetAuthentication(request);
            return request;
        }
	}
}
