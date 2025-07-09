using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ans.Net8.Web
{

	public class DivTag
		: TagBuilderExt
	{

		/* ctor */


		public DivTag(
			string inner)
			: base("div", TagRenderMode.Normal)
		{
			Inner = inner;
			InnerHtml.AppendHtml(
				string.IsNullOrEmpty(Inner) ? "&nbsp;" : Inner);
		}


		/* readonly properties */


		public string Inner { get; }

	}

}
