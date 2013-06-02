using System;
using System.Collections.Generic;
using RestSharp;

namespace FreeAgent
{
	public class AccountingClient : BaseClient
	{
		public AccountingClient(FreeAgentClient client) : base(client) {}

		public override string ResouceName
		{
			get
			{
				return "accounting";
			}
		}


		public List<TrialBalanceSummary> TrialBalanceSummary()
		{
			var request = CreateBasicRequest(Method.GET, "/trial_balance/summary");
			var response = Client.Execute<TrialBalanceSummaryWrapper>(request);

			if (response != null) return response.trial_balance_summary;

			return null;

		}

	}
}

