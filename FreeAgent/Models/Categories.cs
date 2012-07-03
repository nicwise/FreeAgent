using System;
using System.Collections.Generic;

namespace FreeAgent
{
	
	//https://dev.freeagent.com/docs/categories
	
	//https://api.freeagent.com/v2/categories
	public class Category  : BaseModel
	{
		//public string url { get; set; }
		public string description { get; set; }
		public string nominal_code { get; set; }
		public bool allowable_for_tax { get; set; }
		public string tax_reporting_name { get; set; }
		public string auto_sales_tax_rate { get; set; }
	}
	public class Categories
	{
		public Categories()
		{
			admin_expenses_categories = new List<Category>();
			cost_of_sales_categories = new List<Category>();
			income_categories = new List<Category>();
			general_categories = new List<Category>();
		}
		public List<Category> admin_expenses_categories { get; set; }
		public List<Category> cost_of_sales_categories { get; set; }
		public List<Category> income_categories { get; set; }
		public List<Category> general_categories { get; set; }
		
	}
}

