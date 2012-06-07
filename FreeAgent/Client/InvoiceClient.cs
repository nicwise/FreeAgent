using System;
using System.Collections.Generic;
using System.IO;
using RestSharp;
using FreeAgent.Models;

namespace FreeAgent
{
    public class InvoiceClient : ResourceClient<InvoiceWrapper, InvoicesWrapper, Invoice>
    {
        public InvoiceClient(FreeAgentClient client) : base(client) {}

        public override string ResouceName { get { return "invoices"; } } 

        public override InvoiceWrapper WrapperFromSingle(Invoice single)
        {
            return new InvoiceWrapper { invoice = single };
        }
        public override IEnumerable<Invoice> ListFromWrapper(InvoicesWrapper wrapper)
        {
            return wrapper.invoices;
        }

        public override Invoice SingleFromWrapper(InvoiceWrapper wrapper)
        {
            return wrapper.invoice;
        }

        
        
        
    }
}

