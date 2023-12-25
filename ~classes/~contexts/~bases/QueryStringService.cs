namespace Ans.Net8.Web
{

	public class QueryStringService
		: _Current_Base
	{

		/* ctor */


		public QueryStringService(
			ICurrentContext current)
			: base(current)
		{
			Helper = new QueryStringHelper(
				_current.HttpContext.Request.Query);
		}


		/* readonly properties */


		public QueryStringHelper Helper { get; }


		/* functions */


		public QueryStringHelper GetHelper(
			params string[] ignoreParams)
		{
			return new(_current.HttpContext.Request.Query, ignoreParams);
		}

	}

}
