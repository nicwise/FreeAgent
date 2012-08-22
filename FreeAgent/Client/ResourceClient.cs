using System;
using System.Net;
using FreeAgent.Exceptions;
using FreeAgent.Helpers;
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
        public abstract List<TSingle> ListFromWrapper(TListWrapper wrapper);
        public abstract TSingle SingleFromWrapper(TSingleWrapper wrapper);

        const int PageSize = 50;

        public List<TSingle> All(Action<RestRequest> customizeRequest = null)
        {
            int page = 1;



            List<TSingle> allItems = new List<TSingle>();

            while(true)
            {
                var request = CreateAllRequest();
                if (customizeRequest != null) customizeRequest(request);

                AddPaging(request, page);


                var response = Client.Execute<TListWrapper>(request);


                if (response != null) 
                {
                    var newItems = ListFromWrapper(response);
                    allItems.AddRange(newItems);

                    if (newItems.Count < PageSize) return allItems;

                } else if (response == null && page == 1)
                {
                    return null;
                } else 
                {
                    return allItems;
                }

                page++;
            }



            return null;    
        }
        
        public TSingle Get(string id)
        {
            try
            {


                var request = CreateGetRequest(id);
                var response = Client.Execute<TSingleWrapper>(request);

                if (response != null) return SingleFromWrapper(response);

                return null;
            } catch (FreeAgentException fex)
            {
                if (fex.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                throw;
            }
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

       

        protected RestRequest CreateAllRequest()
        {
            var request = CreateBasicRequest(Method.GET);
            CustomizeAllRequest(request);

            return request;
        }
        
        protected RestRequest CreateGetRequest(string id)
        {
            var request = CreateBasicRequest(Method.GET, "/{id}");
            request.AddParameter("id", id, ParameterType.UrlSegment);
                     
            return request;
        }
        
        protected RestRequest CreatePutRequest(TSingle item)
        {
            bool isNewRecord = string.IsNullOrEmpty(item.url);
            var request = CreateBasicRequest(isNewRecord ? Method.POST: Method.PUT, isNewRecord ? "" : "/{id}");

            if (item is IRemoveUrlOnSerialization || item is IRemoveRecurringOnSerialization) 
            {
                request.JsonSerializer = new UrlParsingJsonSerializer();  
            }

            request.RequestFormat = DataFormat.Json;

            if (!isNewRecord) request.AddParameter("id", item.Id(), ParameterType.UrlSegment);
            request.AddBody(WrapperFromSingle(item));         
            
            return request;
        }
        
        protected RestRequest CreateDeleteRequest(string id)
        {
            var request = CreateBasicRequest(Method.DELETE, "/{id}");

            request.AddParameter("id", id, ParameterType.UrlSegment);
                     
            return request;
        }

        public virtual void AddPaging(RestRequest request, int page = 1)
        {
            request.AddParameter("page", page, ParameterType.GetOrPost);
            request.AddParameter("per_page", PageSize, ParameterType.GetOrPost);

        }


    }
}

