using Microsoft.Extensions.Caching.Memory;

namespace Ans.Net8.Web
{

	public class CacheService(
		ICurrentContext current)
		: _Current_Base(current)
	{

		/* functions */


		public T Get<T>(
			string cacheKey,
			Func<T> getObject,
			MemoryCacheEntryOptions options = null)
		{
			if (!_current.MemoryCache.TryGetValue(cacheKey, out T value1))
			{
				value1 = getObject();
				var opt1 = options ?? new MemoryCacheEntryOptions
				{
					AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
				};
				_current.MemoryCache.Set(cacheKey, value1, opt1);
			}
			return value1;
		}

	}

}
