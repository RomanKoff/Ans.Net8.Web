namespace Ans.Net8.Web
{

	public class HostData
		: _ContextModule_Base
	{

		/* ctor */


		public HostData(
			ICurrentContext current)
			: base(current)
		{
			BaseUrl = $"{current.HttpContext.Request.Scheme}://{current.HttpContext.Request.Host}";
			VirtualPath = $"{current.HttpContext.Request.PathBase}/";
			ApplicationUrl = $"{BaseUrl}{VirtualPath}";
		}


		/* readonly properties */


		public string BaseUrl { get; private set; }
		public string VirtualPath { get; private set; }
		public string ApplicationUrl { get; private set; }

	}

}
