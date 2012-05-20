using System;
using System.Collections.Generic;
using System.IO;
using RestSharp;
using FreeAgent.Models;

namespace FreeAgent
{
	public class ContactClient : BaseClient
	{
		public ContactClient(FreeAgentClient client) : base(client) {}
		
		public IEnumerable<Contact> All()
		{
			var request = CreateContactAllRequest();
            var response = Client.Execute<ContactsWrapper>(request);

            if (response != null) return response.contacts;

            return null;	
		}
		
		public Contact Get(string id)
		{
			var request = CreateContactGetRequest(id);
            var response = Client.Execute<ContactWrapper>(request);

            if (response != null) return response.contact;

            return null;
		}
		
		public Contact Put(Contact c)
		{
			var request = CreateContactPutRequest(c);
            var response = Client.Execute<ContactWrapper>(request);

            if (response != null) return response.contact;

            return null;
		}
		
		public void Delete(string id)
		{
			var request = CreateContactDeleteRequest(id);
            var response = Client.Execute(request);
            
		}
		
		
		private RestRequest CreateContactAllRequest(string filter = "active")
		{
			var request = new RestRequest(Method.GET);
			request.Resource = "v{version}/contacts";
            request.AddParameter("version", Version, ParameterType.UrlSegment);
   			request.AddParameter("filter", filter, ParameterType.GetOrPost);
                     
			SetAuthentication(request);
            return request;
		}
		
		private RestRequest CreateContactGetRequest(string id)
		{
			var request = new RestRequest(Method.GET);
			request.Resource = "v{version}/contacts/{id}";
            request.AddParameter("version", Version, ParameterType.UrlSegment);
   			request.AddParameter("id", id, ParameterType.UrlSegment);
                     
			SetAuthentication(request);
            return request;
		}
		
		private RestRequest CreateContactPutRequest(Contact c)
		{
			bool isNewRecord = string.IsNullOrEmpty(c.url);
			var request = new RestRequest(isNewRecord ? Method.POST: Method.PUT);
			request.RequestFormat = DataFormat.Json;
			request.Resource = "v{version}/contacts" + (isNewRecord ? "" : "/{id}");
            request.AddParameter("version", Version, ParameterType.UrlSegment);
   			if (!isNewRecord) request.AddParameter("id", c.Id(), ParameterType.UrlSegment);
            request.AddBody(new ContactWrapper { contact = c});         
			
			SetAuthentication(request);
            return request;
		}
		
		private RestRequest CreateContactDeleteRequest(string id)
		{
			var request = new RestRequest(Method.DELETE);
			request.Resource = "v{version}/contacts/{id}";
            request.AddParameter("version",Version, ParameterType.UrlSegment);
   			request.AddParameter("id", id, ParameterType.UrlSegment);
                     
			SetAuthentication(request);
            return request;
		}
		
		
	}
}

