using System;
using System.Collections.Generic;

namespace FreeAgent
{
	public class Summary
	{

	}

	public class TrialBalanceSummary
	{
		public string category { get; set; }
		public string nominal_code { get; set; }
		public string name { get; set; }
		public float total { get; set; }
	}

	public class TrialBalanceSummaryWrapper
	{
		public TrialBalanceSummaryWrapper()
		{
			trial_balance_summary = new List<TrialBalanceSummary>();
		}
		public List<TrialBalanceSummary> trial_balance_summary { get; set; }
	}
}

