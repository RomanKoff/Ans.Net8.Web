using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Ans.Net8.Web
{

	public static partial class _e
	{

		/*
         * string GetString(this ViewDataDictionary data, string name, string defaultValue = null);
         * int? GetInt(this ViewDataDictionary data, string name);
         * int GetInt(this ViewDataDictionary data, string name, int defaultValue);
         * bool GetBool(this ViewDataDictionary data, string name);
         * DateTime? GetDateTime(this ViewDataDictionary data, string name);
         * DateTime GetDateTime(this ViewDataDictionary data, string name, DateTime defaultValue);
         * 
         */


		public static string GetString(
			this ViewDataDictionary data,
			string name,
			string defaultValue = null)
		{
			return (string)data.Eval(name) ?? defaultValue;
		}


		public static int? GetInt(
			this ViewDataDictionary data,
			string name)
		{
			return (int?)data.Eval(name);
		}


		public static int GetInt(
			this ViewDataDictionary data,
			string name,
			int defaultValue)
		{
			return data.GetInt(name) ?? defaultValue;
		}


		public static bool GetBool(
			this ViewDataDictionary data,
			string name)
		{
			return (bool)data.Eval(name);
		}


		public static DateTime? GetDateTime(
			this ViewDataDictionary data,
			string name)
		{
			return (DateTime?)data.Eval(name);
		}


		public static DateTime GetDateTime(
			this ViewDataDictionary data,
			string name,
			DateTime defaultValue)
		{
			return data.GetDateTime(name) ?? defaultValue;
		}

	}

}
