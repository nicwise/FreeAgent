using System;
using System.Collections.Generic;
using System.IO;
using RestSharp;

namespace FreeAgent
{
	public class BillClient : ResourceClient<BillWrapper, BillsWrapper, Bill>
	{
		public BillClient(FreeAgentClient client) : base(client) {}
		
		public override string ResouceName { get { return "bills"; } } 
		
		public override BillWrapper WrapperFromSingle(Bill single)
		{
			return new BillWrapper { bill = single };
		}
		public override List<Bill> ListFromWrapper(BillsWrapper wrapper)
		{
			return wrapper.bills;
		}
		
		public override Bill SingleFromWrapper(BillWrapper wrapper)
		{
			return wrapper.bill;
		}
		
		/// <summary>
		/// All the specified view, from_date and to_date.
		/// </summary>
		/// <param name='view'>
		/// View. - recent or recurring
		/// </param>
		/// <param name='from_date'>
		/// From_date.
		/// </param>
		/// <param name='to_date'>
		/// To_date.
		/// </param>
		/// 
		public List<Bill> All(string from_date = "", string to_date = "")
		{
			return All((r) => {
				if (!string.IsNullOrEmpty(from_date))
				{
					r.AddParameter("from_date", from_date, ParameterType.GetOrPost);
				}
				if (!string.IsNullOrEmpty(to_date))
				{
					r.AddParameter("to_date", to_date, ParameterType.GetOrPost);
				}
			});
			
		}
		
		
	}
}

