using System;
using FreeAgent.Extensions;


namespace FreeAgent
{
	public static class ModelExtensions
	{
		public static string Id(this BaseModel model)
		{
			if (string.IsNullOrEmpty(model.url)) return "";
			
			string[] elements = model.url.Split('/');
			return elements[elements.Length-1];
		}
		
		public static string UrlId(this BaseModel model)
		{
			if (string.IsNullOrEmpty(model.url)) return "";
			try 
			{
				string[] elements = model.url.Split('/');
				return "/v2/{0}/{1}".Fmt(elements[elements.Length-2],elements[elements.Length-1]);
			} catch {
				return "";
			}
		}
	}
}

