using System;
using System.Collections.Generic;
using System.IO;
using RestSharp;
using FreeAgent.Models;

namespace FreeAgent
{
    public class TaskClient : ResourceClient<TaskWrapper, TasksWrapper, Task>
    {
        public TaskClient(FreeAgentClient client) : base(client) {}

        //need to add in the GET to have a parameter for the project

        public override string ResouceName { get { return "tasks"; } } 

        public override TaskWrapper WrapperFromSingle(Task single)
        {
            return new TaskWrapper { task = single };
        }
        public override IEnumerable<Task> ListFromWrapper(TasksWrapper wrapper)
        {
            return wrapper.tasks;
        }

        public override Task SingleFromWrapper(TaskWrapper wrapper)
        {
            return wrapper.task;
        }

        public IEnumerable<Task> AllByProject(string projectId)
        {
            var request = CreateAllRequest();
            request.AddParameter("project", projectId, ParameterType.GetOrPost);

            var response = Client.Execute<TasksWrapper>(request);

            if (response != null) return ListFromWrapper(response);

            return null; 
        }

        public Task Put(Task item, string projectId)
        {
            bool isNewRecord = string.IsNullOrEmpty(item.url);
            var request = CreateBasicRequest(isNewRecord ? Method.POST: Method.PUT, isNewRecord ? "" : "/{id}");
            request.RequestFormat = DataFormat.Json;

            if (!isNewRecord) request.AddParameter("id", item.Id(), ParameterType.UrlSegment);
            request.AddBody(WrapperFromSingle(item));         


            request.AddParameter("project", projectId, ParameterType.GetOrPost);

            var response = Client.Execute<TaskWrapper>(request);

            if (response != null) return SingleFromWrapper(response);

            return null;
        }

        
        
        
    }
}

