using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Ans.Net8.Web
{

	public static partial class _e
	{

		/*
         * void Set<T>(this ISession session, string key, T value);
         * T Get<T>(this ISession session, string key);
         */


		public static void Set<T>(
			this ISession session,
			string key,
			T value)
		{
			session.SetString(key, JsonSerializer.Serialize<T>(value));
		}


		public static T Get<T>(
			this ISession session,
			string key)
		{
			var value = session.GetString(key);
			return value == null
				? default
				: JsonSerializer.Deserialize<T>(value);
		}

	}

}
