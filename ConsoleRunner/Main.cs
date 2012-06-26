using System;
using System.Collections.Generic;
using System.Diagnostics;
using FreeAgent;
using FreeAgent.Models;
using System.Net;
using System.Linq;

namespace ConsoleRunner
{

    /* You need to make a file called KeyStorage.cs which has:
     * 
     * 


using System;
namespace ConsoleRunner
{
    public partial class MainClass
    {
        public static string AppKey = "";
        public static string AppSecret = "";

        public static string RefreshToken = "";
    }
}


    *
    * In it. This is excluded for git. Put your keys and the like in here
    */


    public partial class MainClass
    {
        public static void Main(string[] args)
        {
			
			ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, ssl) =>  true;
			
			
			
			FreeAgentClient.UseSandbox = true;
			
            var client = new FreeAgentClient(AppKey, AppSecret);
			
			/*

             uncomment this bit if you want to make a new token etc.
             

			string callbackUri = "http://www.fastchicken.co.nz/oauth/";
            string url = client.BuildAuthorizeUrl(callbackUri);

            Debug.WriteLine(url);

            // Now cut and paste that URL, and go to it in a browser. 
            // Once you have been to the URL above, paste the URL that it sends you to into the command window
            // so we can get the oauth code back!
            
			url = Console.ReadLine ();
			
			string code = client.ExtractCodeFromUrl(url);
			
			var newToken = client.GetAccessToken (code, callbackUri);

            //if you can get the token values out at this point, and put them in below, you dont need
            // to do this bit every time.

			*/

            //Token for my test account!
            //you get it from the newtoken above - hard coded in here
            // so you dont have to do the oauth bit each time
            //you really only need the refresh token!
            // 
			
			var sandbox_bttest_token = new AccessToken
			{
				access_token = "",
				refresh_token = RefreshToken,
				token_type = "bearer"
			};
			
            client.CurrentAccessToken = sandbox_bttest_token;


            var token = client.RefreshAccessToken();

            Debug.WriteLine("{0} / {1} / {2} / {3}", token.access_token, token.refresh_token, token.token_type, token.expires_in);


            Company company = client.Company.Single();


            Debug.WriteLine(company.name);

            List<TaxTimeline> timeline = client.Company.TaxTimeline();

            foreach (TaxTimeline t in timeline)
            {
               Debug.WriteLine("{0} / {1} / {2} / {3}", t.dated_on, t.description, t.nature, t.amount_due);
            }
			
			TestContacts(client);
			
			TestProjects(client);
			
			
			

            Console.ReadLine();
        }
		
		public static void TestContacts(FreeAgentClient client)
		{
			var contacts = client.Contact.All();
			
			foreach(var contact in contacts)
			{
				Debug.WriteLine ("BEFORE: {0} / {1} / {2} / {3}", contact.url, contact.organisation_name, contact.first_name, contact.last_name);
			}
			
			Contact c = new Contact
			{
				url = "",
				first_name = "Nic TEST",
				last_name = "Wise",
				address1 = DateTime.Now.ToLongTimeString()
			};
			

			c = client.Contact.Put(c);	
			

			Debug.WriteLine ("PUT: {0} / {1} / {2} / {3}", c.url, c.organisation_name, c.first_name, c.last_name);
			
			//load by id
			
			c = client.Contact.Get (c.Id());
			
			Debug.WriteLine ("GET: {0} / {1} / {2} / {3}", c.url, c.organisation_name, c.first_name, c.last_name);
			
			
			
			contacts = client.Contact.All();
			
			Debug.WriteLine ("deleting contacts");
			foreach(var contact in contacts)
			{
				if (!contact.first_name.Contains ("TEST")) continue;
				
				Debug.WriteLine ("DELETING: {0}", contact.url);
				client.Contact.Delete(contact.Id());	
			}
			
			contacts = client.Contact.All();
			
			foreach(var contact in contacts)
			{
				Debug.WriteLine ("AFTER: {0} / {1} / {2} / {3}", contact.url, contact.organisation_name, contact.first_name, contact.last_name);
			}
			
			
			Debug.WriteLine ("Done with contacts");
			
		}
		
		public static void TestProjects(FreeAgentClient client)
		{
			var all = client.Project.All();
			
			foreach(var item in all)
			{
				Debug.WriteLine ("BEFORE: {0} / {1} / {2} / {3}", item.url, item.name, item.contact, item.status);
			}
			
			//first, get the contacts 'cos we need one!
			
			var contact = client.Contact.All().First();
			
			if (contact == null)
			{
				Debug.WriteLine ("No contacts - can't do projects");
				return;
			}
			
			Project p = new Project
			{
				url = "",
				contact = contact.UrlId(),
				name = "project TEST",
				status = ProjectStatus.Active,
				budget_units = ProjectBudgetUnits.Days,
				currency = "GBP"
			};
			
			
			//FreeAgentClient.Proxy = new WebProxy("127.0.0.1", 8888);
			p = client.Project.Put(p);
			//FreeAgentClient.Proxy = null;
			
			Debug.WriteLine ("PUT: {0} / {1} / {2} / {3}", p.url, p.name, p.contact, p.status);
			
			p = client.Project.Get(p.Id());
			
			Debug.WriteLine ("GET: {0} / {1} / {2} / {3}", p.url, p.name, p.contact, p.status);
			
			all = client.Project.All();
			
			Debug.WriteLine ("deleting projects");
			foreach(var item in all)
			{
				Debug.WriteLine ("deleting {0}", item.url);
				client.Project.Delete(item.Id());	
			}
			
			all = client.Project.All();
			
			foreach(var item in all)
			{
				Debug.WriteLine ("AFTER: {0} / {1} / {2} / {3}", item.url, item.name, item.contact, item.status);
			}
			
			/*
			 * 
			 * 
			 * 
			 * 
			
			Contact c = new Contact
			{
				url = "",
				first_name = "Nic TEST",
				last_name = "Wise",
				address1 = DateTime.Now.ToLongTimeString()
			};
			
			c = client.Contact.Put(c);	
			
			Debug.WriteLine ("PUT: {0} / {1} / {2} / {3}", c.url, c.organisation_name, c.first_name, c.last_name);
			
			//load by id
			
			c = client.Contact.Get (c.Id());
			
			Debug.WriteLine ("GET: {0} / {1} / {2} / {3}", c.url, c.organisation_name, c.first_name, c.last_name);
			
			
			
			var p = new Project
			{
				url = "",
				first_name = "Nic",
				last_name = "Wise",
				address1 = DateTime.Now.ToLongTimeString()
			};
			
			c = client.Contact.Put(c);	
			
			Debug.WriteLine ("{0} / {1} / {2} / {3}", c.url, c.organisation_name, c.first_name, c.last_name);
			
			//load by id
			
			c = client.Contact.Get (c.Id());
			
			Debug.WriteLine ("{0} / {1} / {2} / {3}", c.url, c.organisation_name, c.first_name, c.last_name);
			
			
			
			contacts = client.Contact.All();
			
			Debug.WriteLine ("deleting contacts");
			foreach(var contact in contacts)
			{
				Debug.WriteLine ("deleting {0}", contact.url);
				client.Contact.Delete(contact.Id());	
			}
			
			contacts = client.Contact.All();
			
			foreach(var contact in contacts)
			{
				Debug.WriteLine ("{0} / {1} / {2} / {3}", contact.url, contact.organisation_name, contact.first_name, contact.last_name);
			}
			
			*/
			Debug.WriteLine ("Done with projects");
			
		}
    }
}