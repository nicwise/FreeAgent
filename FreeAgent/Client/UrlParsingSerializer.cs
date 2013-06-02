using System;
using RestSharp.Serializers;
using System.Diagnostics;

namespace FreeAgent
{
    public interface IRemoveUrlOnSerialization {}
    public interface IRemoveRecurringOnSerialization {}

    public class UrlParsingJsonSerializer : ISerializer
    {
 
        JsonSerializer baseSerializer;

        public UrlParsingJsonSerializer() {
            ContentType = "application/json";
            baseSerializer = new JsonSerializer();

        }
     
        ///
        /// Serialize the object as JSON
        ///
        /// <param name="obj" />Object to serialize
        /// JSON as String
        public string Serialize(object obj) {
            string json = baseSerializer.Serialize(obj);




            
            json = Remove(json, "\"url\":\"\"");
            json = Remove(json, "\"project\":\"\"");
            json = Remove(json, "\"url\":null");
            json = Remove(json, "\"project\":null");

            json = Remove(json, "\"recurring\":\"true\"");
            json = Remove(json, "\"recurring\":\"false\"");
            json = Remove(json, "\"recurring_end_date\":\"\"");
            json = Remove(json, "\"recurring_end_date\":null");
			json = Remove(json, "\"recurring_end_date\":\"0001-01-01\"");

            json = Remove(json, "\"reclaim_mileage_rate\":-1.0");
			json = Remove(json, "\"reclaim_mileage_rate\":-1");

            if (json.Contains("\"sales_tax_rate\":-2"))
            {
                json = Remove(json, "\"sales_tax_rate\":-2.0");
                json = Remove(json, "\"sales_tax_rate\":-2");

                json = Remove(json, "\"manual_sales_tax_amount\":0");

            }

			json = Remove (json, "\"sales_tax_value\":0.0");
			json = Remove (json, "\"sales_tax_value\":0");

            //manual amount
            if (json.Contains("\"sales_tax_rate\":-3"))
            {
                json = Remove(json, "\"sales_tax_rate\":-3.0");
                json = Remove(json, "\"sales_tax_rate\":-3");

            }
            json = Remove(json, "\"manual_sales_tax_amount\":-1");
			json = Remove(json, "\"manual_sales_tax_amount\":-1.0");

            json = Remove(json, "\"mileage\":0");
            json = Remove(json, "\"ec_status\":0");


               

            //Debug.WriteLine("post: " + json);
            return json;
        }

        private string Remove(string source, string match)
        {
            return source.Replace("," + match + "}",  "}").Replace(match + ",", "");
        }

        private string Replace(string source, string match, string replacement)
        {
            return source.Replace("," + match + "}",  "," + replacement + "}").Replace(match + ",", replacement + ",");
        }
     
        ///
        /// Unused for JSON Serialization
        ///
        public string DateFormat { get; set; }
        ///
        /// Unused for JSON Serialization
        ///
        public string RootElement { get; set; }
        ///
        /// Unused for JSON Serialization
        ///
        public string Namespace { get; set; }
        ///
        /// Content type for serialized content
        ///
        public string ContentType { get; set; }
    }

}

