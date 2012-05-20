using System;

namespace FreeAgent
{
	public class BaseModel
	{
		public BaseModel()
		{
			url = "";
			updated_at = "";
			created_at = "";
			
		}
		public string url { get; set;}
		public string updated_at { get; set; }
		public string created_at { get; set; }
		
		
	}
}

