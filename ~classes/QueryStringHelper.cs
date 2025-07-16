using Ans.Net8.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
				.Concat(["descending"])
				.ToArray();
			if (queryParams != null)
				foreach (var key1 in queryParams.Keys)
					if (!ignores1.Any(x => x == key1))
						Params.Add(key1, queryParams[key1]);
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


		public Dictionary<string, StringValues> Params { get; } = [];


		/* functions */


		public bool TestKey(
			string key)
		{
			return Params.ContainsKey(key);
		}


		public bool TestValue(
			string key,
			string value)
		{
			return TestKey(key)
				&& Params[key].ToString().Equals(value);
		}


		public string GetString(
			string key,
			string defaultValue = null)
		{
			return TestKey(key)
				? Params[key].ToString()
				: defaultValue;
		}


		public int? GetInt(
			string key)
		{
			return TestKey(key)
				? Params[key].ToString().ToInt()
				: null;
		}
		public int GetInt(
			string key,
			int defaultValue)
		{
			return GetInt(key) ?? defaultValue;
		}


		public bool GetBool(
			string key)
		{
			return TestKey(key) && Params[key].ToString().ToBool();
		}


		public DateTime? GetDateTime(
			string key)
		{
			return TestKey(key)
				? Params[key].ToString().ToDateTime()
				: null;
		}
		public DateTime GetDateTime(
			string key,
			DateTime defaultValue)
		{
			return GetDateTime(key) ?? defaultValue;
		}


		public DateOnly? GetDateOnly(
			string key)
		{
			return TestKey(key)
				? Params[key].ToString().ToDateOnly()
				: null;
		}
		public DateOnly GetDateOnly(
			string key,
			DateOnly defaultValue)
		{
			return GetDateOnly(key) ?? defaultValue;
		}


		public TimeOnly? GetTimeOnly(
			string key)
		{
			return TestKey(key)
				? Params[key].ToString().ToTimeOnly()
				: null;
		}
		public TimeOnly GetTimeOnly(
			string key,
			TimeOnly defaultValue)
		{
			return GetTimeOnly(key) ?? defaultValue;
		}


		public string MakeQueryString(
			string baseUrl,
			params string[] allowedParams)
		{
			var params1 = (allowedParams?.Length > 0)
				? Params.Where(x => allowedParams.Any(y => y == x.Key))
				: Params;
			var d1 = params1
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
				if (TestKey(key))
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
			// 2008-06-15T21:15:07 o:2008-06-15T21:15:07.0000000
			if (value != null)
				Append(key, value?.ToString("s"));
		}


		public void Append(
			string key,
			DateOnly? value)
		{
			if (value != null)
				Append(key, value?.ToString());
		}


		public void Append(
			string key,
			TimeOnly? value)
		{
			if (value != null)
				Append(key, value?.ToString());
		}


		public void AppendString(
			ViewDataDictionary viewData,
			string key,
			string defaultValue = null)
		{
			var value1 = viewData.GetString(key, defaultValue);
			Append(key, value1);
		}
		public void AppendString(
			QueryStringHelper helper,
			string key,
			string defaultValue = null)
		{
			var value1 = helper.GetString(key, defaultValue);
			Append(key, value1);
		}


		public void AppendInt(
			ViewDataDictionary viewData,
			string key,
			int defaultValue = 0)
		{
			var value1 = viewData.GetInt(key, defaultValue);
			Append(key, value1);
		}
		public void AppendInt(
			QueryStringHelper helper,
			string key,
			int defaultValue = 0)
		{
			var value1 = helper.GetInt(key, defaultValue);
			Append(key, value1);
		}


		public void AppendBool(
			ViewDataDictionary viewData,
			string key)
		{
			var value1 = viewData.GetBool(key);
			Append(key, value1);
		}
		public void AppendBool(
			QueryStringHelper helper,
			string key)
		{
			var value1 = helper.GetBool(key);
			Append(key, value1);
		}


		public void AppendDateTime(
			ViewDataDictionary viewData,
			string key,
			DateTime? defaultValue = null)
		{
			var value1 = defaultValue == null
				? viewData.GetDateTime(key)
				: viewData.GetDateTime(key, defaultValue.Value);
			Append(key, value1);
		}
		public void AppendDateTime(
			QueryStringHelper helper,
			string key,
			DateTime? defaultValue = null)
		{
			var value1 = defaultValue == null
				? helper.GetDateTime(key)
				: helper.GetDateTime(key, defaultValue.Value);
			Append(key, value1);
		}


		public void AppendDateOnly(
			ViewDataDictionary viewData,
			string key,
			DateOnly? defaultValue = null)
		{
			var value1 = defaultValue == null
				? viewData.GetDateOnly(key)
				: viewData.GetDateOnly(key, defaultValue.Value);
			Append(key, value1);
		}
		public void AppendDateOnly(
			QueryStringHelper helper,
			string key,
			DateOnly? defaultValue = null)
		{
			var value1 = defaultValue == null
				? helper.GetDateOnly(key)
				: helper.GetDateOnly(key, defaultValue.Value);
			Append(key, value1);
		}


		public void AppendTimeOnly(
			ViewDataDictionary viewData,
			string key,
			TimeOnly? defaultValue)
		{
			var value1 = defaultValue == null
				? viewData.GetTimeOnly(key)
				: viewData.GetTimeOnly(key, defaultValue.Value);
			Append(key, value1);
		}
		public void AppendTimeOnly(
			QueryStringHelper helper,
			string key,
			TimeOnly? defaultValue)
		{
			var value1 = defaultValue == null
				? helper.GetTimeOnly(key)
				: helper.GetTimeOnly(key, defaultValue.Value);
			Append(key, value1);
		}


		public void Remove(
			string key)
		{
			Params.Remove(key);
		}

	}

}
