using System;
using RestSharp.Serializers;
using System.Diagnostics;

namespace FreeAgent
{
    public interface IRemoveUrlOnSerialization {}

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




            json = json
                .Replace("\"url\":\"\",", "")
                .Replace("\"url\":null,", "")
                .Replace("\"project\":\"\",", "")
                .Replace("\"project\":null,", "")
                    .Replace(",\"url\":\"\"}", "}")
                    .Replace(",\"url\":null}", "}")
                    .Replace(",\"project\":\"\"}", "}")
                    .Replace(",\"project\":null}", "}");

            Console.WriteLine(json);
            return json;
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

