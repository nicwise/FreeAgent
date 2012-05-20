using System;
using System.Collections.Generic;
using System.IO;
using RestSharp;
using FreeAgent.Models;

namespace FreeAgent
{
	public class ProjectClient : BaseClient
	{
		public ProjectClient(FreeAgentClient client) : base(client) {}
		
		public IEnumerable<Project> All()
		{
			var request = CreateProjectAllRequest();
            var response = Client.Execute<ProjectsWrapper>(request);

            if (response != null) return response.projects;

            return null;	
		}
		
		
		
		public Project Get(string id)
		{
			var request = CreateProjectGetRequest(id);
            var response = Client.Execute<ProjectWrapper>(request);

            if (response != null) return response.project;

            return null;
		}
		
		public Project Put(Project item)
		{
			var request = CreateProjectPutRequest(item);
            var response = Client.Execute<ProjectWrapper>(request);

            if (response != null) return response.project;

            return null;
		}
		
		public void Delete(string id)
		{
			var request = CreateProjectDeleteRequest(id);
            var response = Client.Execute(request);
            
		}
		
				
		private RestRequest CreateProjectAllRequest(string filter = "active")
		{
			var request = new RestRequest(Method.GET);
			request.Resource = "v{version}/projects";
            request.AddParameter("version", Version, ParameterType.UrlSegment);
   			request.AddParameter("filter", filter, ParameterType.GetOrPost);
                     
			SetAuthentication(request);
            return request;
		}
		
		private RestRequest CreateProjectGetRequest(string id)
		{
			var request = new RestRequest(Method.GET);
			request.Resource = "v{version}/projects/{id}";
            request.AddParameter("version", Version, ParameterType.UrlSegment);
   			request.AddParameter("id", id, ParameterType.UrlSegment);
                     
			SetAuthentication(request);
            return request;
		}
		
		private RestRequest CreateProjectPutRequest(Project item)
		{
			bool isNewRecord = string.IsNullOrEmpty(item.url);
			var request = new RestRequest(isNewRecord ? Method.POST: Method.PUT);
			request.RequestFormat = DataFormat.Json;
			request.Resource = "v{version}/projects" + (isNewRecord ? "" : "/{id}");
            request.AddParameter("version", Version, ParameterType.UrlSegment);
   			if (!isNewRecord) request.AddParameter("id", item.Id(), ParameterType.UrlSegment);
            request.AddBody(new ProjectWrapper { project = item});         
			
			SetAuthentication(request);
            return request;
		}
		
		private RestRequest CreateProjectDeleteRequest(string id)
		{
			var request = new RestRequest(Method.DELETE);
			request.Resource = "v{version}/projects/{id}";
            request.AddParameter("version",Version, ParameterType.UrlSegment);
   			request.AddParameter("id", id, ParameterType.UrlSegment);
                     
			SetAuthentication(request);
            return request;
		}
	
		
	}
}

