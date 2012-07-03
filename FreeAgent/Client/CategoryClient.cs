using System;
using RestSharp;

namespace FreeAgent
{
    public class CategoryClient : BaseClient
    {
        public CategoryClient(FreeAgentClient client) : base(client) {}

        //need to add in the GET to have a parameter for the date filter

        public override string ResouceName { get { return "categories"; } } 

        public Categories All()
        {
            var request = CreateBasicRequest(Method.GET);
            var response = Client.Execute<Categories>(request);

            if (response != null) return response;

            return null;    
        
        }

        // OMFG this is going to be so slow. Caching?
        private Categories all;

        public Category Single(string id)
        {
            if (all == null) all = All();

            foreach(var cat in all.admin_expenses_categories)
            {
                if (cat.nominal_code == id) return cat;
            }
            foreach(var cat in all.cost_of_sales_categories)
            {
                if (cat.nominal_code == id) return cat;
            }

            foreach(var cat in all.general_categories)
            {
                if (cat.nominal_code == id) return cat;
            }

            foreach(var cat in all.income_categories)
            {
                if (cat.nominal_code == id) return cat;
            }

            return null;

        }

        /* Disabled
         * 
         * The API comes back with a different root node based on the type.
         * FFS. This is insane. 
         * 
        public Categories Single(string id)
        {
            var request = CreateBasicRequest(Method.GET, "/{id}");
            request.AddParameter("id", id, ParameterType.UrlSegment);

            var response = Client.Execute<Categories>(request);

            if (response != null) return response;

            return null;    
        
        }
        */

        
    }
}

