
You will need to make a KeyStorage.cs file (which isn't commited as per the .gitignore)

namespace FreeAgent.Tests
{
    public class KeyStorage
    {
        public static bool UseSandbox = true; 
        public static bool UseProxy = false; 

        public static string AppKey = "YOUR_API_KEY_HERE";
        public static string AppSecret = "YOUR_API_SECRET_HERE";

        public static string RefreshToken = "TOKEN TO USE FOR REFRESHING";
    }
}

If you use a local proxy be sure to change UseProxy to true and change the proxy as required.

To make a new RefreshToken, you can use the Google OAuth playground, or:


string callbackUri = "http://www.fastchicken.co.nz/oauth/";
string url = client.BuildAuthorizeUrl(callbackUri);

Debug.WriteLine(url);

// Now cut and paste that URL, and go to it in a browser. 
// Once you have been to the URL above, paste the URL that it sends you to into the command window
// so we can get the oauth code back!
// it will look something like:
// http://www.fastchicken.co.nz/oauth/?code=some_big_long_thing_here
            
url = Console.ReadLine ();
			
string code = client.ExtractCodeFromUrl(url);
			
var newToken = client.GetAccessToken (code, callbackUri);

RefreshToken = newToken.refresh_token;



//if you can get the token values out at this point, and put them in below, you dont need
// to do this bit every time.


