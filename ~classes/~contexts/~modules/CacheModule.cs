using Microsoft.Extensions.Caching.Memory;

namespace Ans.Net8.Web
{

	public class CacheModule
		: _ContextModule_Base
	{

		/* ctor */


		public CacheModule(
			ICurrentContext current)
			: base(current)
		{
		}


		/* functions */


		public T Get<T>(
			string cacheKey,
			Func<T> getObject)
		{
			if (!_current.MemoryCache.TryGetValue(cacheKey, out T value1))
			{
				value1 = getObject();
				var opt1 = new MemoryCacheEntryOptions
				{
					AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5),
					//SlidingExpiration = TimeSpan.FromSeconds(5)
				};
				_current.MemoryCache.Set(cacheKey, value1, opt1);
				//_current.CacheMap.Items.Add(cacheKey, new Common.Services.CacheInfo { Options = opt1 });
			}
			return value1;
		}

	}

}
