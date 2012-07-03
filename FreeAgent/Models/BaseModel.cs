using System;

namespace FreeAgent
{
	public class BaseModel
	{
		public BaseModel()
		{
			url = "";
		}
		public string url { get; set;}
		
	}

    public class UpdatableModel : BaseModel
    {
        public UpdatableModel()
        {

            updated_at = "";
            created_at = "";
            
        }
        public string updated_at { get; set; }
        public string created_at { get; set; }
    }
}

