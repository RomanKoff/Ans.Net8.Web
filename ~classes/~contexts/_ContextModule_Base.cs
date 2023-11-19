namespace Ans.Net8.Web
{

	public class _ContextModule_Base
	{
		internal readonly ICurrentContext _current;

		public _ContextModule_Base(
			ICurrentContext current)
		{
			_current = current;
		}
	}

}
