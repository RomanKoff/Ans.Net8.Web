namespace Ans.Net8.Web
{

	public class HostData
		: _Current_Base
	{

		/* ctor */


		public HostData(
			ICurrentContext current)
			: base(current)
		{
			BaseUrl = current.HttpContext.GetBaseUrl();
			VirtualPath = current.HttpContext.GetVirtualPath();
			ApplicationUrl = $"{BaseUrl}{VirtualPath}";
		}


		/* readonly properties */


		public string BaseUrl { get; }
		public string VirtualPath { get; }
		public string ApplicationUrl { get; }

	}

}
