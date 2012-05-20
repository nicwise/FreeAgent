using System;
using System.Text;
using System.Collections.Generic;

namespace FreeAgent.Extensions
{
    public static class StringExtensions
    {
        public static string UrlEncode(this string value)
        {
            value = Uri.EscapeDataString(value);

            StringBuilder builder = new StringBuilder();
            foreach (char ch in value)
            {
                if ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~%".IndexOf(ch) != -1)
                {
                    builder.Append(ch);
                }
                else
                {
                    builder.Append('%' + string.Format("{0:X2}", (int)ch));
                }
            }
            return builder.ToString();
        }
		
		public static string Fmt(this string str, params string[] p)
		{
			return string.Format (str, p);
		}
		
		public static Dictionary<string, string> ParseQueryString(this string value)
		{
			Dictionary<string, string> queryParameters = new Dictionary<string, string>();
			string[] querySegments = value.Split('&');
			foreach(string segment in querySegments)
			{
			   string[] parts = segment.Split('=');
			   if (parts.Length > 0)
			   {
			      string key = parts[0].Trim(new char[] { '?', ' ' });
			      string val = parts[1].Trim();
			
			      queryParameters.Add(key, val);
			   }
			}
			return queryParameters;
		}
    }
}
