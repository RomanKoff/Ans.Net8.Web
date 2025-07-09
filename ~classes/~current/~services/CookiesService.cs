namespace Ans.Net8.Web
{

	public class CookiesService(
		CurrentContext current)
	{

		private readonly CurrentContext _current = current;


		/* functions */


		public bool Has(
			string key)
		{
			return _current.HttpContext.Request.Cookies.ContainsKey(key);
		}


		public string Get(
			string key)
		{
			return _current.HttpContext.Request.Cookies.TryGetValue(
				key, out var value1)
					? value1 : null;
		}


		/* methods */


		public void Append(
			string key,
			string value)
		{
			_current.HttpContext.Response.Cookies.Append(key, value);
		}


		public void Delete(
			string key)
		{
			_current.HttpContext.Response.Cookies.Delete(key);
		}

	}

}
