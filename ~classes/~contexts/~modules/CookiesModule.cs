namespace Ans.Net8.Web
{

	public class CookiesModule
		: _ContextModule_Base
	{

		/* ctor */


		public CookiesModule(
			ICurrentContext current)
			: base(current)
		{
		}


		/* functions */


		public bool Has(
			string key)
		{
			return _current.HttpContext.Request.Cookies.ContainsKey(key);
		}


		public string Get(
			string key)
		{
			return Has(key)
				? _current.HttpContext.Request.Cookies[key]
				: null;
		}


		/* methods */


		public void Append(
			string key,
			string value)
		{
			_current.HttpContext.Response.Cookies.Append(key, value);
		}

	}

}
