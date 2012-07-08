
using System.Collections.Generic;
using RestSharp;
using FreeAgent.Authenticators;
using System;

namespace FreeAgent
{
   	public class CompanyClient : BaseClient
	{
		public CompanyClient(FreeAgentClient client) : base(client) {}
		
        public override string ResouceName
        {
            get
            {
                return "company";
            }
        }

		public Company Single ()
		{
            var request = CreateBasicRequest(Method.GET);
            var response = Client.Execute<CompanyWrapper>(request);

            if (response != null) return response.company;

            return null;	
		
		}
		public List<TaxTimeline> TaxTimeline()
		{
			var request = CreateBasicRequest(Method.GET, "/tax_timeline");
            var response = Client.Execute<TaxTimelineWrapper>(request);

            if (response != null) return response.timeline_items;

            return null;

		}
		
	}
}
