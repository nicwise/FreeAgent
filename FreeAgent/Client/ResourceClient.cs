using System;
using FreeAgent.Helpers;
using FreeAgent.Models;
using System.Collections.Generic;
using RestSharp;

namespace FreeAgent
{
    public abstract class ResourceClient<TSingleWrapper, TListWrapper, TSingle> : BaseClient 
        where TSingle : BaseModel 
        where TListWrapper : new()
        where TSingleWrapper : new()
    {


        public ResourceClient(FreeAgentClient client) : base(client)
        {
        }
        



        public abstract TSingleWrapper WrapperFromSingle(TSingle single);
        public abstract IEnumerable<TSingle> ListFromWrapper(TListWrapper wrapper);
        public abstract TSingle SingleFromWrapper(TSingleWrapper wrapper);



        public IEnumerable<TSingle> All()
        {
            var request = CreateAllRequest();
            var response = Client.Execute<TListWrapper>(request);

            if (response != null) return ListFromWrapper(response);

            return null;    
        }
        
        public TSingle Get(string id)
        {
            var request = CreateGetRequest(id);
            var response = Client.Execute<TSingleWrapper>(request);

            if (response != null) return SingleFromWrapper(response);

            return null;
        }
        
        public TSingle Put(TSingle c)
        {
            var request = CreatePutRequest(c);
            var response = Client.Execute<TSingleWrapper>(request);

            if (response != null) return SingleFromWrapper(response);

            return null;
        }
        
        public void Delete(string id)
        {
            var request = CreateDeleteRequest(id);
            var response = Client.Execute(request);
            
        }

       

        private RestRequest CreateAllRequest()
        {
            var request = CreateBasicRequest(Method.GET);
            CustomizeAllRequest(request);
            return request;
        }
        
        private RestRequest CreateGetRequest(string id)
        {
            var request = CreateBasicRequest(Method.GET, "/{id}");
            request.AddParameter("id", id, ParameterType.UrlSegment);
                     
            return request;
        }
        
        private RestRequest CreatePutRequest(TSingle item)
        {
            bool isNewRecord = string.IsNullOrEmpty(item.url);
            var request = CreateBasicRequest(isNewRecord ? Method.POST: Method.PUT, isNewRecord ? "" : "/{id}");
            request.RequestFormat = DataFormat.Json;

            if (!isNewRecord) request.AddParameter("id", item.Id(), ParameterType.UrlSegment);
            request.AddBody(WrapperFromSingle(item));         
            
            return request;
        }
        
        private RestRequest CreateDeleteRequest(string id)
        {
            var request = CreateBasicRequest(Method.DELETE, "/{id}");

            request.AddParameter("id", id, ParameterType.UrlSegment);
                     
            return request;
        }


    }
}

