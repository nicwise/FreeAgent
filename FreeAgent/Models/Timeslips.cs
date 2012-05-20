using System;
using System.Collections.Generic;

namespace FreeAgent
{
	
	//https://dev.freeagent.com/docs/timeslips
	
	//https://api.freeagent.com/v2/timeslips?from_date=?&to_date=?
	
	public class Timeslip
	{
		public string url { get; set; }
		public string user { get; set; }
		public string project { get; set; }
		public string task { get; set; }
		public string dated_on { get; set; }
		public double hours { get; set;}
		public string updated_at { get; set; }
		public string created_at { get; set; }
		
	}
	
	public class TimeslipWrapper
	{
		public TimeslipWrapper()
		{
			timeslips = new List<Timeslip>();
			timeslip = null;
		}
		public Timeslip timeslip { get; set; }
		public List<Timeslip> timeslips { get; set; }
	}
	
	
}

