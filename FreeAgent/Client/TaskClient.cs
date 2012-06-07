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

        
        
        
    }
}

