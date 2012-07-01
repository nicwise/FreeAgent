

## .NET client for FreeAgent (freeagent.com) API (v2)

This is very much a work in progress. Eventually, you should be able to do this, which is stolen (like a lot of this) from DropNet



## How do I use it?

##### The Client:
To use FreeAgentClient you need an instance of the FreeAgentClient class, this class does everything for FreeAgentClient. 
This class takes the API Key and API Secret (These must be obtained from FreeAgent to access the API).

```csharp
    //OPTIONAL
    FreeAgentClient.UseSandbox = true;
    
    _client = new FreeAgentClient("API KEY", "API SECRET");
```
 

##### Login/Tokens:
FreeAgent requires a web authentication to get a usable token/secret, so this is a 3 step process.

**Step 1.** Authorize App with FreeAgent - This step involves sending the user to a login page on the FreeAgent site and having them authenticate there. 
The FreeAgent client has a function to return the url for you but the rest must be handled in app, this function also takes a callback url for 
redirecting the user to after they have logged in. (NOTE: The token still cant be used yet.)

```csharp
    string callbackUri = "http://your-server.com/something";
    string url = client.BuildAuthorizeUrl(callbackUri);
    
    //Send the user to the url in a browser so the user can login
```

Open a browser with the url returned by BuildAuthorizeUrl - After we have the authorize url we need to direct the user there 
(use some sort of browser here depending on the platform) and navigate the user to the url. This will prompt them to login and 
authorize your app with the API.

**Step 2.** Get an Access Token from the URL code and Request Token - This is the last stage of the process, converting the oauth request token into a usable 
FreeAgent API token. This function will use the clients stored Request Token but this can be overloaded if you need to specify a token to use.

```csharp
    string code = client.ExtractCodeFromUrl(url);
			
	var newToken = client.GetAccessToken (code, callbackUri);
	
    
    //Store this newToken.accessToken so we dont have to do all of this next time!
```



**Once you have a token:** 

```csharp
			FreeAgentClient.UseSandbox = true;

            var Client = new FreeAgentClient(KeyStorage.AppKey, KeyStorage.AppSecret);

            var token = new AccessToken
            {
                access_token = "",
                refresh_token = KeyStorage.RefreshToken,
                token_type = "bearer"
            };

            Client.CurrentAccessToken = token;


            var Token = Client.RefreshAccessToken();

            if (Token == null || string.IsNullOrEmpty(Token.access_token) || string.IsNullOrEmpty(Token.refresh_token))
            {
                throw new Exception("Could not setup the Token");
            }
```

