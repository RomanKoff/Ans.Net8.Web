namespace Ans.Net8.Web
{

	public class QueryStringModule
		: _ContextModule_Base
	{

		/* ctor */


		public QueryStringModule(
			ICurrentContext current)
			: base(current)
		{
			Helper = new QueryStringHelper(
				_current.HttpContext.Request.Query);
		}


		/* readonly properties */


		public QueryStringHelper Helper { get; private set; }


		/* functions */


		public QueryStringHelper GetHelper(
			params string[] ignoreParams)
		{
			return new(_current.HttpContext.Request.Query, ignoreParams);
		}

	}

}
