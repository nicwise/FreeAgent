using System;
using System.Collections.Generic;

namespace FreeAgent
{
	
	//GET https://api.freeagent.com/v2/bank_accounts
	
	//https://api.freeagent.com/v2/bank_accounts
	
	public class BankAccount : BaseModel
	{
		
		
		public double opening_balance { get; set; }
		public string type { get; set; }
		public string name { get; set; }
		public bool is_personal { get; set; }
		
		//for standard ones - account_number also on CC
		public string account_number  { get; set; }
		public string sort_code { get; set; }
		public string secondary_sort_code { get; set; }
		public string iban { get; set; }
		public string bic { get; set; }
				
		//for paypal
		public string email { get; set; }
		
		
		//credit card ones
		
		
		public double current_balance { get; set;}

		
		
	}
	
	public class BankAccountWrapper
	{
		public BankAccountWrapper()
		{
		
			bank_account = null;
		}
		public BankAccount bank_account { get; set; }
	
	}

    public class BankAccountsWrapper
    {
        public BankAccountsWrapper()
        {
            bank_accounts = new List<BankAccount>();

        }

        public List<BankAccount> bank_accounts { get; set; }
    }
	
	public class BankAccountType 
	{
		public static string StandardBankAccount = "StandardBankAccount";
		public static string PaypalAccount = "PaypalAccount";
		public static string CreditCardAccount = "CreditCardAccount";
	}
	
}



