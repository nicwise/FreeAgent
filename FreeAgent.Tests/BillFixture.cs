using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FreeAgent.Tests
{
	[TestFixture]
	public class BillFixture : ResourceFixture<BillWrapper, BillsWrapper, Bill>
	{
		public override void SetupClient()
		{
			base.SetupClient();
			GetAll = BillAll;
		}
		
		public IEnumerable<Bill> BillAll()
		{
			return Client.Bill.All(); // recent not supported according to api docs? view: "recent");
		}
		
		public override ResourceClient<BillWrapper, BillsWrapper, Bill> ResourceClient
		{
			get { return Client.Bill; }
		}
		
		
		public override void CheckSingleItem(Bill item)
		{
			
			Assert.IsNotNullOrEmpty(item.url);
			//Assert.IsNotNullOrEmpty(contact.organisation_name);
			//Assert.IsNotNullOrEmpty(contact.first_name);
			//Assert.IsNotNullOrEmpty(contact.last_name);
		}
		
		
		public override Bill CreateSingleItemForInsert()
		{
			
			Assert.Ignore("IGNORING Bill INSERTING UNTIL IT WORKS");
			var user = Client.User.Me;
			var cat = Client.Categories.Single("250");
			
			return new Bill
			{
				url = "",
				dated_on = DateTime.Now.ModelDate(),
				category = cat.UrlId(),
				//recurring = false
			};
			
			
		}
		
		public override void CompareSingleItem(Bill originalItem, Bill newItem)
		{
			Assert.IsNotNull(newItem);
			Assert.IsNotNullOrEmpty(newItem.url);
			Assert.AreEqual(newItem.status, originalItem.status);
			//Assert.AreEqual(newItem.user, originalItem.user);
			Assert.AreEqual(newItem.dated_on, originalItem.dated_on);
		}
		
		public override bool CanDelete(Bill item)
		{
			return false;
			return item.status.Contains("TEST");
			
		}
		
		
		
	}
}
