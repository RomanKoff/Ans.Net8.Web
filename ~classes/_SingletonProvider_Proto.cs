using Ans.Net8.Common;
using System.Diagnostics;

namespace Ans.Net8.Web
{

	public abstract class _SingletonProvider_Proto<T>
	{

		private readonly Dictionary<string, T> _items = [];


		/* abstracts */


		public abstract bool TestMissed(string key);
		public abstract T GetItem(string key);


		/* properties */


		public string ProviderName
		{
			get => _providerName ??= GetType().GetCSharpTypeName();
			set => _providerName = value;
		}
		private string _providerName;


		/* functions */


		public T Get(
			string key)
		{
			key ??= "";
			if (_items.TryGetValue(key, out T item1))
				return item1;
			if (TestMissed(key))
			{
				_items.Add(key, default);
				Debug.WriteLine($"{ProviderName}(\"{key}\") : MISSED");
				return default;
			}
			T item2;
			try
			{
				item2 = GetItem(key);
				Debug.WriteLine($"{ProviderName}(\"{key}\") : LOADED");
			}
			catch (Exception)
			{
				throw new Exception($"{ProviderName}(\"{key}\") : ERROR");
			}
			_items.Add(key, item2);
			return item2;
		}


		/* methods */


		public void Remove(
			string key)
		{
			_ = _items.Remove(key ?? "");
		}

	}

}
