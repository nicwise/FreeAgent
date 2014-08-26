using System;
using System.Collections.Generic;
using System.IO;
using RestSharp;


namespace FreeAgent
{
	public class BankAccountClient : ResourceClient<BankAccountWrapper, BankAccountsWrapper, BankAccount>
	{
		public BankAccountClient(FreeAgentClient client) : base(client)
		{
		}

		//need to add in the GET to have a parameter for the date filter

		public override string ResouceName { get { return "bank_accounts"; } }

		public override BankAccountWrapper WrapperFromSingle(BankAccount single)
		{
			return new BankAccountWrapper { bank_account = single };
		}

		public override List<BankAccount> ListFromWrapper(BankAccountsWrapper wrapper)
		{
			return wrapper.bank_accounts;
		}

		public override BankAccount SingleFromWrapper(BankAccountWrapper wrapper)
		{
			return wrapper.bank_account;
		}

        
        
        
	}

	public class BankTransactionClient : ResourceClient<BankTransactionWrapper, BankTransactionsWrapper, BankTransaction>
	{
		public BankTransactionClient(FreeAgentClient client) : base(client)
		{
		}

		//need to add in the GET to have a parameter for the date filter

		public override string ResouceName { get { return "bank_transactions"; } }

		public override BankTransactionWrapper WrapperFromSingle(BankTransaction single)
		{
			return new BankTransactionWrapper { bank_transaction = single };
		}

		public override List<BankTransaction> ListFromWrapper(BankTransactionsWrapper wrapper)
		{
			return wrapper.bank_transactions;
		}

		public override BankTransaction SingleFromWrapper(BankTransactionWrapper wrapper)
		{
			return wrapper.bank_transaction;
		}

		public List<BankTransaction> AllForAccount(string bankAccountId, string from_date, string to_date)
		{
			return All((r) =>
			{
				r.AddParameter("bank_account", bankAccountId, ParameterType.GetOrPost);
				r.AddParameter("from_date", from_date, ParameterType.GetOrPost);
				r.AddParameter("to_date", to_date, ParameterType.GetOrPost);
			});
		}
        
        
        
	}

	public class BankTransactionExplanationClient : ResourceClient<BankTransactionExplanationWrapper, BankTransactionExplanationsWrapper, BankTransactionExplanation>
	{
		public BankTransactionExplanationClient(FreeAgentClient client) : base(client)
		{
		}

		//need to add in the GET to have a parameter for the date filter

		public override string ResouceName { get { return "bank_transaction_explanations"; } }

		public override BankTransactionExplanationWrapper WrapperFromSingle(BankTransactionExplanation single)
		{
			return new BankTransactionExplanationWrapper { bank_transaction_explanation = single };
		}

		public override List<BankTransactionExplanation> ListFromWrapper(BankTransactionExplanationsWrapper wrapper)
		{
			return wrapper.bank_transaction_explanations;
		}

		public override BankTransactionExplanation SingleFromWrapper(BankTransactionExplanationWrapper wrapper)
		{
			return wrapper.bank_transaction_explanation;
		}
	}
}

