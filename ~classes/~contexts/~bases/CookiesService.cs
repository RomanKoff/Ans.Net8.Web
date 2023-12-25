namespace Ans.Net8.Web
{

	public class CookiesService(
		ICurrentContext current)
		: _Current_Base(current)
	{

		/* functions */


		public bool Has(
			string key)
			=> _current.HttpContext.Request.Cookies.ContainsKey(key);


		public string Get(
			string key)
			=> _current.HttpContext.Request.Cookies.TryGetValue(key, out var value1)
				? value1 : null;


		/* methods */


		public void Append(
			string key,
			string value)
			=> _current.HttpContext.Response.Cookies.Append(key, value);

	}

}
