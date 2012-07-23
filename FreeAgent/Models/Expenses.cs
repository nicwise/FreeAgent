using System;
using System.Collections.Generic;

namespace FreeAgent
{
	
	//GET https://api.freeagent.com/v2/contacts
	
	//https://api.freeagent.com/v2/contacts
	
	public class Expense : UpdatableModel, IRemoveUrlOnSerialization, IRemoveRecurringOnSerialization
	{
		
        public Expense()
        {
            //recurring = false;
            have_vat_receipt = false;
            ec_status = ExpenseECStatus.None;
        }
	
		public string user { get; set;}
		public string project { get; set;}
		public double gross_value { get; set;}
		public double sales_tax_rate { get; set;}
		public string description { get; set;}
		public string dated_on { get; set;}
		public string category { get; set;}
		public double mileage { get; set;}
		public double reclaim_mileage_rate { get; set;}
		public double rebill_mileage_rate { get; set;}
		//public bool recurring { get; set;}
		//public string recurring_end_date { get; set; }
        public string rebill_type { get; set; }
        public double initial_rate_mileage { get; set; }
        public string receipt_reference { get; set; }
        public int ec_status { get; set; }


        public double manual_sales_tax_amount { get; set;}
		public double rebill_factor { get; set;}
        public string vehicle_type { get; set;}
        public int engine_size_index { get; set;}
        public int engine_type_index { get; set; }
        public bool have_vat_receipt { get; set; }
		public ExpenseAttachment attachment  { get; set;}
				

	
	}
	
	
	
	public class ExpenseAttachment
	{
		public string data {get; set;}
		public string file_name {get; set;}
		public string description {get; set;}
		public string content_type {get; set;}
	}
	
	public class ExpenseAttachmentContentType
	{
		public const string Png = "image/png";
		public const string XPng = "image/x-png";
		public const string Jpeg = "image/jpeg";
		public const string Jpg = "image/jpg";
		public const string Gif = "image/gif";
		public const string Pdf = "application/x-pdf";
	}

    public class ExpenseECStatus 
    {
        public const int None = 0;
        public const int Goods = 2;
        public const int Service = 1;
    }
	
	public class ExpenseWrapper
	{
		public ExpenseWrapper()
		{
			
			expense = null;
		}
		public Expense expense { get; set; }
		
	}

    public class ExpensesWrapper
    {
        public ExpensesWrapper()
        {
            expenses = new List<Expense>();
        }

        public List<Expense> expenses { get; set; }
    }
	
	
	
}




