using System;
using System.Collections.Generic;
using System.IO;
using RestSharp;


namespace FreeAgent
{
	public class ContactClient : ResourceClient<ContactWrapper, ContactsWrapper, Contact>
	{
		public ContactClient(FreeAgentClient client) : base(client) {}
		
		
		public override void CustomizeAllRequest(RestRequest request)
        {
            request.AddParameter("view", "active", ParameterType.GetOrPost);
        }
		
        public override string ResouceName { get { return "contacts"; } } 

        public override ContactWrapper WrapperFromSingle(Contact single)
        {
            return new ContactWrapper { contact = single };
        }
        public override List<Contact> ListFromWrapper(ContactsWrapper wrapper)
        {
            return wrapper.contacts;
        }

        public override Contact SingleFromWrapper(ContactWrapper wrapper)
        {
            return wrapper.contact;
        }

		
		
		
	}
}

