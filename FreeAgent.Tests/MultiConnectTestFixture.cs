using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using NUnit.Framework;

namespace FreeAgent.Tests
{

    public class MultiConnectTestFixture
    {
        protected FreeAgentClient Client, LiveClient;
        protected AccessToken Token;

        public virtual void Configure()
        {

        }

        [SetUp]
        public virtual void SetupClient()
        {
            Configure();

           
        }


        [Test]
        public void CanGetList()
        {


            FreeAgentClient.UseSandbox = true;
            //FreeAgentClient.Proxy = new WebProxy("127.0.0.1", 8888);
            var sandbox_bttest_token = new AccessToken
            {
                access_token = "",
                refresh_token = KeyStorage.RefreshToken,
                token_type = "bearer"
            };



            
            {

				var Client = new FreeAgentClient(KeyStorage.AppKey, KeyStorage.AppSecret);

                Client.CurrentAccessToken = sandbox_bttest_token;

                try
                {
                    var co = Client.Company.Single();
                } catch
                {
                    
                }

			}
            

            FreeAgentClient.UseSandbox = false;
           

				var LiveClient = new FreeAgentClient(KeyStorage.AppKey, KeyStorage.AppSecret);

                LiveClient.CurrentAccessToken = sandbox_bttest_token;

                try
                {
                    var co = LiveClient.Company.Single();
                } catch
                {
                    
                }


            
            
            Assert.IsTrue(true);

        }

        
    }
}
