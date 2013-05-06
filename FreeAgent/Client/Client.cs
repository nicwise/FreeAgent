using System;
using System.Net;
using System.Threading.Tasks;
using FreeAgent.Authenticators;
using FreeAgent.Exceptions;
using FreeAgent.Extensions;
using FreeAgent.Helpers;
using RestSharp;
using RestSharp.Deserializers;


namespace FreeAgent
{
    public partial class FreeAgentClient
    {
        private const string ApiBaseUrl = "https://api.freeagent.com";
        private const string ApiSandboxBaseUrl = "https://api.sandbox.freeagent.com";

        public const string Version = "2";

		public static WebProxy Proxy = null;


        /// <summary>
        /// To use Dropbox API in sandbox mode (app folder access) set to true
        /// </summary>
  		private static bool useSandbox = false;      
		public static bool UseSandbox { get { return useSandbox;} set { useSandbox = value;} }

        private readonly string _apiKey;
        private readonly string _appsecret;

        private RestClient _restClient, _restClientModified;
        private AccessToken _currentAccessToken = null;
        private RequestHelper _requestHelper;
		
		public RequestHelper Helper { get { return _requestHelper; }}

        public AccessToken CurrentAccessToken
        {
            get { return _currentAccessToken; }
            set { 
                _currentAccessToken = value;
                if (_requestHelper != null)
                {
                    _requestHelper.CurrentAccessToken = _currentAccessToken;
                }
            }
        }

        /// <summary>
        /// Gets the directory root for the requests (full or sandbox mode)
        /// </summary>
        private string BaseUrl
        {
            get { return UseSandbox ? ApiSandboxBaseUrl : ApiBaseUrl; }
        }

        /// <summary>
        /// Default Constructor for the DropboxClient
        /// </summary>
        /// <param name="apiKey">The Api Key to use for the Dropbox Requests</param>
        /// <param name="appSecret">The Api Secret to use for the Dropbox Requests</param>
        public FreeAgentClient(string apiKey, string appSecret)
        {
            _apiKey = apiKey;
            _appsecret = appSecret;

            LoadClient();
        }

        /// <summary>
        /// Creates an instance of the DropNetClient given an API Key/Secret and a User Token/Secret
        /// </summary>
        /// <param name="apiKey">The Api Key to use for the Dropbox Requests</param>
        /// <param name="appSecret">The Api Secret to use for the Dropbox Requests</param>
        /// <param name="userToken">The User authentication token</param>
        /// <param name="userSecret">The Users matching secret</param>
        public FreeAgentClient(string apiKey, string appSecret, AccessToken savedToken)
        {
            _apiKey = apiKey;
            _appsecret = appSecret;
            CurrentAccessToken = savedToken;

            LoadClient();


        }



        private void LoadClient()
        {
            _restClient = new RestClient(BaseUrl);
            _restClient.ClearHandlers();
            _restClient.AddHandler("application/json", new JsonDeserializer());	

            _requestHelper = new RequestHelper(Version);
            _requestHelper.ApiKey = _apiKey;
            _requestHelper.ApiSecret = _appsecret;
			
			SetProxy();


            //Default to full access
            //UseSandbox = false;
  			SetupClients();
			
		}
		
		public void SetProxy()
		{
			_restClient.Proxy = Proxy;
		}
		
		



        /// <summary>
        /// Helper Method to Build up the Url to authorize a Token/Secret
        /// </summary>
        /// <param name="userLogin"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public string BuildAuthorizeUrl(string callback = null)
        {

            //Go 1-Liner!
            return string.Format("{0}/v{1}/approve_app?response_type=code&client_id={2}{3}&state=foo",
                                 BaseUrl,
                                 Version,
                                 _apiKey,
                                 (string.IsNullOrEmpty(callback) ? string.Empty : "&redirect_uri=" + callback.UrlEncode()));
        }
		
		public string ExtractCodeFromUrl(string url)
		{
			//url will contain code=.... in the parameters
			
			Uri uri = new Uri(url);
			string query = uri.Query;
			if (string.IsNullOrEmpty(query) || !query.Contains("code="))
			{
				return "";
			}
			
			var elements = query.ParseQueryString();
			
			if (elements.ContainsKey("code")) return elements["code"];
			
			return "";
			
		}

		
		public CompanyClient Company = null;
		public ContactClient Contact = null;
		public ProjectClient Project = null;

        public ExpenseClient Expense = null;
        public InvoiceClient Invoice = null;
        public TaskClient Task = null;
        public TimeslipClient Timeslip = null;
        public UserClient User = null;
        public BankAccountClient BankAccount = null;
        public CategoryClient Categories = null;
        public BankTransactionClient BankTransaction = null;
		public BillClient Bill = null;
		
		private void SetupClients()
		{
			Company = new CompanyClient(this);
			Contact = new ContactClient(this);
			Project = new ProjectClient(this);
            Expense = new ExpenseClient(this);
            Invoice = new InvoiceClient(this);
            Task = new TaskClient(this);
            Timeslip = new TimeslipClient(this);
            User = new UserClient(this);
            BankAccount = new BankAccountClient(this);
            Categories = new CategoryClient(this);
            BankTransaction = new BankTransactionClient(this);
			Bill = new BillClient(this);
		}
  


		private bool IsSuccess(HttpStatusCode code)
		{
			int val = (int)code;
			if (val < 299) return true;
			return false;
		}
		
		private void SetAuthentication(RestRequest request)
        {
            request.AddHeader("Authorization", "Bearer " + CurrentAccessToken.access_token);
        }
		
		
#if !WINDOWS_PHONE
        internal T Execute<T>(IRestRequest request) where T : new()
        {
            IRestResponse<T> response;

			SetProxy ();
            //Console.WriteLine(_restClient.BuildUri(request));
            response = _restClient.Execute<T>(request);

            if (!IsSuccess(response.StatusCode))
            {
                Console.WriteLine(response.Content);
                throw new FreeAgentException(response);
            }

            if (response.Data == null)
            {
                Console.WriteLine("{0} returned null", _restClient.BuildUri(request));
            }


            return response.Data;
        }

        internal IRestResponse Execute(IRestRequest request)
        {
            IRestResponse response;


            response = _restClient.Execute(request);

            if (!IsSuccess(response.StatusCode))
            {
                throw new FreeAgentException(response);
            }


            return response;
        }
#endif

        protected void ExecuteAsync(IRestRequest request, Action<IRestResponse> success, Action<FreeAgentException> failure)
        {
#if WINDOWS_PHONE
    //check for network connection
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                //do nothing
                failure(new DropboxException
                {
                    StatusCode = System.Net.HttpStatusCode.BadGateway
                });
                return;
            }
#endif

            _restClient.ExecuteAsync(request, (response, asynchandler) =>
                                                  {
                                                      if (response.StatusCode != HttpStatusCode.OK)
                                                      {
                                                          failure(new FreeAgentException(response));
                                                      }
                                                      else
                                                      {
                                                          success(response);
                                                      }
                                                  });
        }

        protected void ExecuteAsync<T>(IRestRequest request, Action<T> success, Action<FreeAgentException> failure) where T : new()
        {
#if WINDOWS_PHONE
    //check for network connection
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                //do nothing
                failure(new DropboxException
                {
                    StatusCode = System.Net.HttpStatusCode.BadGateway
                });
                return;
            }
#endif

            _restClient.ExecuteAsync<T>(request, (response, asynchandler) =>
                                                     {
                                                         if (response.StatusCode != HttpStatusCode.OK)
                                                         {
                                                             failure(new FreeAgentException(response));
                                                         }
                                                         else
                                                         {
                                                             success(response.Data);
                                                         }
                                                     });
        }

        private Task<T> ExecuteTask<T>(IRestRequest request) where T : new()
        {
            return _restClient.ExecuteTask<T>(request);
        }

        private Task<IRestResponse> ExecuteTask(IRestRequest request)
        {
            return _restClient.ExecuteTask(request);
        }



        private void SetAuthProviders()
        {
            //if (UserLogin != null)
            //{
            //Set the OauthAuthenticator only when the UserLogin property changes
            //   _restClient.Authenticator = new OAuthAuthenticator(_restClient.BaseUrl, _apiKey, _appsecret, UserLogin.Token, UserLogin.Secret);
            //}
        }
    }
}