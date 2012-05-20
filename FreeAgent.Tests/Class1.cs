using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreeAgent;

namespace FreeAgent.Tests
{

    public class Variables
    {
        public static string ApiKey = "";
        public static string ApiSecret = "";

        public static string UserToken = "";
        public static string UserSecret = "";

        public static string AuthorizedToken = "";

    }
    public class Class1
    {

        public static void Main(string[] args)
        {
            var client = new FreeAgentClient(Variables.ApiKey, Variables.ApiSecret);

            //var token = client.GetToken();

//            Console.WriteLine(token.Token, token.Secret);

        }
    }
}
