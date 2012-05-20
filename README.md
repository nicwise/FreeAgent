

## .NET client for FreeAgent (freeagent.com) API (v2)

This is very much a work in progress. Eventually, you should be able to do this, which is stolen (like a lot of this) from DropNet



## How do I use it?

##### The Client:
To use FreeAgentClient you need an instance of the FreeAgentClient class, this class does everything for FreeAgentClient. 
This class takes the API Key and API Secret (These must be obtained from FreeAgent to access the API).

```csharp
    _client = new FreeAgentClient("API KEY", "API SECRET");
```
 

##### Login/Tokens:
FreeAgent requires a web authentication to get a usable token/secret, so this is a 3 step process.

**Step 1.** Get Request Token - This step gets an oauth token from freeagent (NOTE: the token must pass the other steps before it can be used)

```csharp
    // Sync
    _client.GetToken();
    
```

**Step 2.** Authorize App with FreeAgent - This step involves sending the user to a login page on the FreeAgent site and having them authenticate there. The FreeAgent client has a function to return the url for you but the rest must be handled in app, this function also takes a callback url for redirecting the user to after they have logged in. (NOTE: The token still cant be used yet.)

```csharp
    var url = _client.BuildAuthorizeUrl();
    //Use the url in a browser so the user can login
```

Open a browser with the url returned by BuildAuthorizeUrl - After we have the authorize url we need to direct the user there (use some sort of browser here depending on the platform) and navigate the user to the url. This will prompt them to login and authorize your app with the API.

**Step 3.** Get an Access Token from the Request Token - This is the last stage of the process, converting the oauth request token into a usable FreeAgent API token. This function will use the clients stored Request Token but this can be overloaded if you need to specify a token to use.

```csharp
    // Sync
    var accessToken = _client.GetAccessToken(); //Store this token for "remember me" function
 
    //Store this accessToken so we dont have to do all of this next time!
```



**Best Practices:** 

```csharp
    _client = new FreeAgentClient("API KEY", "API SECRET", "USER TOKEN", "USER SECRET");
    // OR
    _client = new FreeAgentClient("API KEY", "API SECRET");
    _client.UserLogin = new UserLogin { Token = "USER TOKEN", Secret = "USER SECRET" };
```

***


 **Like FreeAgentClient?** Endore Damian Karzon on Coderwall - this is seriously ripped off his design for DropNet, which is a great client for Dropbox.
 
 [![endorse](http://api.coderwall.com/dkarzon/endorsecount.png)](http://coderwall.com/dkarzon)