using System;
using System.Collections.Generic;
using System.IO;
using RestSharp;


namespace FreeAgent
{
    public class InvoiceClient : ResourceClient<InvoiceWrapper, InvoicesWrapper, Invoice>
    {
        public InvoiceClient(FreeAgentClient client) : base(client) {}

        public override string ResouceName { get { return "invoices"; } } 

        public override void CustomizeAllRequest(RestRequest request)
        {
            request.AddParameter("nested_invoice_items", "true", ParameterType.GetOrPost);
        }

        public override InvoiceWrapper WrapperFromSingle(Invoice single)
        {
            return new InvoiceWrapper { invoice = single };
        }
        public override List<Invoice> ListFromWrapper(InvoicesWrapper wrapper)
        {
            return wrapper.invoices;
        }

        public override Invoice SingleFromWrapper(InvoiceWrapper wrapper)
        {
            return wrapper.invoice;
        }

        public List<Invoice> AllForProject(string projectId)
        {
            return All((r) => {
                r.AddParameter("project", projectId, ParameterType.GetOrPost);
            });
        }

        public List<Invoice> AllForContact(string contactId)
        {
            return All((r) => {
                r.AddParameter("contact", contactId, ParameterType.GetOrPost);
            });
        }

        public List<Invoice> AllWithFilter(string filter)
        {
            return All((r) => {
                r.AddParameter("view", filter, ParameterType.GetOrPost);
            });
        }

		public bool SendEmail(string invoiceId, InvoiceEmail email)
		{
			var request = CreateBasicRequest(Method.POST, "/{id}/send_email");

			request.RequestFormat = DataFormat.Json;

			request.AddUrlSegment ("id", invoiceId);
			request.AddBody(new InvoiceEmailWrapper() { invoice = email });  


			var response = Client.Execute(request);

			if (response != null)
				return response.StatusCode == System.Net.HttpStatusCode.OK;

			return false;



		}

		public bool MarkAsSent(string invoiceId)
		{
			var request = CreateBasicRequest(Method.PUT, "/{id}/transitions/mark_as_sent");

			request.RequestFormat = DataFormat.Json;

			request.AddUrlSegment ("id", invoiceId);

			var response = Client.Execute(request);

			if (response != null)
				return response.StatusCode == System.Net.HttpStatusCode.OK;

			return false;



		}


        
        
    }
}

