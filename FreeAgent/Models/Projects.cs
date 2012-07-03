using System;
using System.Collections.Generic;

namespace FreeAgent
{
	
	//GET https://api.freeagent.com/v2/contacts
	
	//https://api.freeagent.com/v2/contacts
	
	public class Project : UpdatableModel, IRemoveUrlOnSerialization
	{
		public Project() : base()
		{
			contact = "";
			name = "";
			status = ProjectStatus.Active;
			budget_units = ProjectBudgetUnits.Hours;
			currency = "GBP";
            billing_period = ProjectBillingPeriod.Hour;
		}
		public string contact { get; set; }
		public string name { get; set; }
		public string starts_on { get; set; }
		public string ends_on { get; set; }
		public double budget { get; set; }
		public bool is_ir35 { get; set; }
		public string contract_po_reference { get; set; }
		public string status { get; set; }
		public string budget_units { get; set; }
		public double normal_billing_rate { get; set; }
		public double hours_per_day { get; set; }
		public bool uses_project_invoice_sequence { get; set; }
		public string currency { get; set; }
		public string billing_period { get; set; }
	}
	
	public class ProjectBudgetUnits 
	{
		public static string Hours = "Hours";
		public static string Days = "Days";
		public static string Monetary = "Monetary";
	}
	
	public class ProjectStatus 
	{
		public static string Active = "Active";
		public static string Completed = "Completed";
		public static string Cancelled = "Cancelled";
		public static string Hidden = "Hidden";
	}

    public class ProjectBillingPeriod
    {
        public static string Hour = "hour";
        public static string Day = "day";
    }
	
	public class ProjectWrapper
	{
		public ProjectWrapper()
		{
			//projects = new List<Project>();
			project = null;
		}
		public Project project { get; set; }
		//public List<Project> projects { get; set; }
	}
	
	public class ProjectsWrapper
	{
		public ProjectsWrapper()
		{
			projects = new List<Project>();
			//project = null;
		}
		//public Project project { get; set; }
		public List<Project> projects { get; set; }
	}
	
	
	
}




