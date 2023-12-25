using Ans.Net8.Common;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace Ans.Net8.Web
{

	public class WebApiService(
		ICurrentContext current)
		: _Current_Base(current)
	{

		/* functions */


		public WebApiCachedHelper<T> Get<T>(
			string cacheKey,
			string baseUrl,
			MemoryCacheEntryOptions cacheOptions,
			JsonSerializerOptions jsonOptions)
		{
			return new(
				_current.HttpClientFactory.CreateClient(),
				_current.MemoryCache,
				cacheKey,
				cacheOptions,
				baseUrl,
				jsonOptions);
		}


		public WebApiCachedHelper<T> Get<T>(
			string cacheKey,
			string baseUrl,
			MemoryCacheEntryOptions cacheOptions = null,
			bool propertyNameCaseInsensitive = false)
		{
			return new(
				_current.HttpClientFactory.CreateClient(),
				_current.MemoryCache,
				cacheKey,
				cacheOptions,
				baseUrl,
				propertyNameCaseInsensitive);
		}


		public WebApiCachedHelper<T> Get<T>(
			string cacheKey,
			int slidingExpirationSeconds,
			int absoluteExpirationRelativeToNowSeconds,
			string baseUrl,
			JsonSerializerOptions jsonOptions)
		{
			return new(
				_current.HttpClientFactory.CreateClient(),
				_current.MemoryCache,
				cacheKey,
				slidingExpirationSeconds,
				absoluteExpirationRelativeToNowSeconds,
				baseUrl,
				jsonOptions);
		}


		public WebApiCachedHelper<T> Get<T>(
			string cacheKey,
			int slidingExpirationSeconds,
			int absoluteExpirationRelativeToNowSeconds,
			string baseUrl,
			bool propertyNameCaseInsensitive = false)
		{
			return new(
				_current.HttpClientFactory.CreateClient(),
				_current.MemoryCache,
				cacheKey,
				slidingExpirationSeconds,
				absoluteExpirationRelativeToNowSeconds,
				baseUrl,
				propertyNameCaseInsensitive);
		}

	}

}
