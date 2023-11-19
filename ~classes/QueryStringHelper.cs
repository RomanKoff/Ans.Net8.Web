using Ans.Net8.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace Ans.Net8.Web
{

	public class QueryStringHelper
	{

		/* ctors */


		public QueryStringHelper(
			Dictionary<string, StringValues> queryParams,
			params string[] ignoreParams)
		{
			var ignores1 = ignoreParams
				.Concat(new string[] { "descending" })
				.ToArray();
			Params = new Dictionary<string, StringValues>();
			if (queryParams != null)
				foreach (var key1 in queryParams.Keys)
				{
					if (!ignores1.Any(x => x == key1))
						Params.Add(key1, queryParams[key1]);
				}
		}


		public QueryStringHelper(
			IQueryCollection queryParams,
			params string[] ignoreParams)
			: this(queryParams.ToDictionary(x => x.Key, x => x.Value), ignoreParams)
		{
		}


		public QueryStringHelper(
			string queryString,
			params string[] ignoreParams)
			: this(QueryHelpers.ParseQuery(queryString), ignoreParams)
		{
		}


		public QueryStringHelper()
			: this(string.Empty)
		{
		}


		/* readonly properties */


		public Dictionary<string, StringValues> Params { get; private set; }


		/* functions */


		public bool TestHas(
			string key)
		{
			return Params.ContainsKey(key);
		}


		public bool Test(
			string key,
			string value)
		{
			return TestHas(key)
				&& Params[key].ToString().Equals(value);
		}


		public string GetString(
			string key,
			string defaultValue = null)
		{
			return TestHas(key)
				? Params[key].ToString()
				: defaultValue;
		}


		public int? GetInt(
			string key)
		{
			return TestHas(key)
				? Params[key].ToString().ToInt()
				: null;
		}


		public int GetInt(
			string key,
			int defaultValue)
		{
			return TestHas(key)
				? Params[key].ToString().ToInt(defaultValue)
				: defaultValue;
		}


		public DateTime? GetDateTime(
			string key)
		{
			return TestHas(key)
				? Params[key].ToString().ToDateTime()
				: null;
		}


		public DateTime GetDateTime(
			string key,
			DateTime defaultValue)
		{
			return TestHas(key)
				? Params[key].ToString().ToDateTime(defaultValue)
				: defaultValue;
		}


		public string GetQueryString(
			string baseUrl)
		{
			var d1 = Params
				.GroupBy(x => x.Key)
				.ToDictionary(x => x.Key, x => x.Last().Value);
			return QueryHelpers.AddQueryString(baseUrl, d1);
		}


		/* methods */


		public void Append(
			string key,
			string value)
		{
			if (!string.IsNullOrEmpty(key))
			{
				if (TestHas(key))
					Params[key] = value;
				else
					Params.Add(key, value);
			}
		}


		public void Append(
			string key,
			int? value)
		{
			if (value != null && value != 0)
				Append(key, value.ToString());
		}


		public void Append(
			string key,
			bool value)
		{
			if (value)
				Append(key, "true");
		}


		public void Append(
			string key,
			DateTime? value)
		{
			if (value != null)
				Append(key, value?.ToString("s")); // 2008-06-15T21:15:07 o:2008-06-15T21:15:07.0000000
		}


		public void Remove(
			string key)
		{
			Params.Remove(key);
		}

	}

}
