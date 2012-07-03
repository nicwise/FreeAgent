using System;
using System.Collections.Generic;

namespace FreeAgent
{
	
	//GET https://api.freeagent.com/v2/contacts
	
	//https://api.freeagent.com/v2/contacts
	
	public class Contact : UpdatableModel
	{
		
		public Contact() : base()
		{
			locale = "en";
			organisation_name = "";
			first_name = "";
			last_name = "";
		}
		
		public string locale { get; set; }
		public double account_balance { get; set; }
		public string organisation_name { get; set; }
		public string first_name { get; set; }
		public string last_name { get; set; }
		public string email { get; set; }
		public string phone_number { get; set; }
		public string address1 { get; set; }
		public string town { get; set; }
		public string region { get; set; }
		public string postcode { get; set; }
		public string address2 { get; set; }
		public string address3 { get; set; }
		public  bool contact_name_on_invoices { get; set; }
		public string country { get; set; }
		public string sales_tax_registration_number { get; set; }
		public bool uses_contact_invoice_sequence { get; set; }
		
		
	}
	
	public class ContactWrapper
	{
		public ContactWrapper()
		{
			//contacts = new List<Contact>();
			contact = null;
		}
		public Contact contact { get; set; }
		//public List<Contact> contacts { get; set; }
	}
	
	public class ContactsWrapper
	{
		public ContactsWrapper()
		{
			contacts = new List<Contact>();
			//contact = null;
		}
		//public Contact contact { get; set; }
		public List<Contact> contacts { get; set; }
	}
	
	
	
}



