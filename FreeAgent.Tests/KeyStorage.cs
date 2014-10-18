using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeAgent.Tests
{
    public static class KeyStorage
    {
        public static bool UseSandbox { get { return true; } }
        public static bool UseProxy { get { return true; } }
        public static string AppKey { get { return "*your appkey here*"; } }
        public static string AppSecret { get { return "*your appsecret here*"; } }
        public static string RefreshToken { get { return "*your refreshtoken here*"; } }
    }
}
