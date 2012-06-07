using System;
using System.Collections.Generic;

namespace FreeAgent
{
	
	//https://dev.freeagent.com/docs/tasks
	
	//https://api.freeagent.com/v2/tasks?project=xxx
	
	public class Task : BaseModel
	{
		public Task() : base()
        {

        }
		public string project { get; set; }
		public string name { get; set; }
		public bool is_billable { get; set; }
		public double billing_rate { get; set; }
		public string billing_period { get; set; }
		public string status { get; set; }

	}
	
	public class TaskWrapper
	{
		public TaskWrapper()
		{
			task = null;
		}
		public Task task { get; set; }
		
	}

    public class TasksWrapper
    {
        public TasksWrapper()
        {
            tasks = new List<Task>();

        }

        public List<Task> tasks { get; set; }
    }
	
	
}

