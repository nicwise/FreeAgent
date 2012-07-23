using RestSharp;
using FreeAgent.Authenticators;
using System;

namespace FreeAgent
{
    public partial class FreeAgentClient
    {


        /// <summary>
        /// Gets a token from the almightly dropbox.com (Token cant be used until authorized!)
        /// </summary>
        /// <returns></returns>
        public AccessToken GetAccessToken(string code, string redirectUri = "")
        {


            _restClient.BaseUrl = BaseUrl;

            var request = _requestHelper.CreateAccessTokenRequest(code, redirectUri);

            var response = Execute<AccessToken>(request);

            if (response != null && !string.IsNullOrEmpty(response.access_token))
            {
                CurrentAccessToken = response;
            }

            return CurrentAccessToken;
        }



        public AccessToken RefreshAccessToken()
        {
            _restClient.BaseUrl = BaseUrl;

            var request = _requestHelper.CreateRefreshTokenRequest();
            var response = Execute<AccessToken>(request);

            if (response != null && !string.IsNullOrEmpty(response.access_token))
            {
                var token = CurrentAccessToken;


                token.access_token = response.access_token;
                token.expires_in = response.expires_in;

                CurrentAccessToken = token;
                return CurrentAccessToken;
            }

            return null;
        }
    }
}
