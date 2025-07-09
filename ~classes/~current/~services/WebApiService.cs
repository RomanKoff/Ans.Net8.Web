using Ans.Net8.Common;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace Ans.Net8.Web
{

	public class WebApiService(
		CurrentContext current)
	{

		private readonly CurrentContext _current = current;


		/* functions */


		public WebApiCachedHelper<T> GetHelper<T>(
			string resUrl,
			string cacheKey,
			MemoryCacheEntryOptions cacheOptions,
			JsonSerializerOptions jsonOptions)
		{
			var url1 = _current.GetResUrl(resUrl);
			return new(
				_current.HttpClientFactory.CreateClient(),
				_current.MemoryCache,
				url1,
				cacheKey ?? url1,
				cacheOptions,
				jsonOptions);
		}


		public T Get<T>(
			string resUrl,
			int slidingExpirationSeconds = 0,
			int absoluteExpirationRelativeToNowSeconds = 10)
		{
			var helper1 = GetHelper<T>(
				resUrl,
				null,
				SuppCache.GetOptions(
					slidingExpirationSeconds,
					absoluteExpirationRelativeToNowSeconds),
				null);
			return helper1.SendQuery().Content;
		}

	}

}
