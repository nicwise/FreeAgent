using System;
using System.Collections.Generic;

namespace FreeAgent
{
	
	//GET https://api.freeagent.com/v2/bank_accounts
	
	//https://api.freeagent.com/v2/bank_accounts
	
	public class BankAccount : UpdatableModel, IRemoveUrlOnSerialization
	{
		public double opening_balance { get; set; }

		public string type { get; set; }

		public string name { get; set; }

		public bool is_personal { get; set; }

		public string bank_name { get; set; }

		public string currency { get; set; }
		
		//for standard ones - account_number also on CC
		public string account_number  { get; set; }

		public string sort_code { get; set; }

		public string secondary_sort_code { get; set; }

		public string iban { get; set; }

		public string bic { get; set; }
				
		//for paypal
		public string email { get; set; }

		public double current_balance { get; set; }

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
	
	//needs to be moved to it's own file

	public class BankTransaction : UpdatableModel
	{
		public string bank_account { get; set; }

		public double amount { get; set; }

		public string dated_on { get; set; }

		public string description { get; set; }

		public double unexplained_amount { get; set; }

		public bool is_manual { get; set; }

	}

	public class BankTransactionWrapper
	{
		public BankTransactionWrapper()
		{
        
			bank_transaction = null;
		}

		public BankTransaction bank_transaction { get; set; }
    
	}

	public class BankTransactionsWrapper
	{
		public BankTransactionsWrapper()
		{
			bank_transactions = new List<BankTransaction>();

		}

		public List<BankTransaction> bank_transactions { get; set; }
	}

	public class BankTransactionExplanation : UpdatableModel
	{
		public string bank_account { get; set; }

		public string bank_transaction { get; set; }

		public string dated_on { get; set; }

		public double gross_value { get; set; }

		public string description { get; set; }

		public string category { get; set; }

		public string rebill_type { get; set; }

		public double foreign_currency_value { get; set; }

		public double manual_sales_tax_amount { get; set; }

		public double sales_tax_rate { get; set; }

		public double rebill_factor { get; set; }

		public ExpenseAttachment attachment  { get; set; }

		/* public string receipt_reference { get; set; }



		public int ec_status { get; set; }

		public string currency { get; set; }


		public bool have_vat_receipt { get; set; }
*/

	}

	public class BankTransactionExplanationWrapper
	{
		public BankTransactionExplanationWrapper()
		{

			bank_transaction_explanation = null;
		}

		public BankTransactionExplanation bank_transaction_explanation { get; set; }

	}

	public class BankTransactionExplanationsWrapper
	{
		public BankTransactionExplanationsWrapper()
		{
			bank_transaction_explanations = new List<BankTransactionExplanation>();

		}

		public List<BankTransactionExplanation> bank_transaction_explanations { get; set; }
	}


}



