namespace Ans.Net8.Web
{

	public class _AnsTagHelper_Base(
		CurrentContext current)
		: _TagHelper_Base
	{
		public readonly CurrentContext Current = current;
		public readonly LibOptions Options = current.Options;
	}

}
