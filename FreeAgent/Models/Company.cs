using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeAgent
{
    public class Company
    {
        public string url { get; set; }
        public string name { get; set; }
        public string subdomain { get; set; }
        public string type { get; set; }
        public string currency { get; set; }
        public string mileage_units { get; set; }
        public string company_start_date { get; set; }
        public string freeagent_start_date { get; set; }
        public string first_accounting_year_end { get; set; }
        public string company_registration_number { get; set; }
        public string sales_tax_registration_status { get; set; }
        public string sales_tax_registration_number { get; set; }
    }

    public class CompanyWrapper
    {
        public CompanyWrapper()
        {
            company = new Company();
        }
        public Company company { get; set; }
    }

    public class TaxTimeline
    {
        public string description { get; set; }
        public string nature { get; set; }
        public string dated_on { get; set; }
        public float amount_due { get; set; }
        public bool is_personal { get; set; }
    }

    public class TaxTimelineWrapper
    {
        public TaxTimelineWrapper()
        {
            timeline_items = new List<TaxTimeline>();
        }
        public List<TaxTimeline> timeline_items { get; set; }
    }



}
