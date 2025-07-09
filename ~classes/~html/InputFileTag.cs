using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ans.Net8.Web
{

	public class InputFileTag
		: TagBuilderExt
	{

		/* ctor */


		public InputFileTag(
			string name)
			: base("input", TagRenderMode.SelfClosing)
		{
			Name = name;
			MergeAttribute("id", Name);
			MergeAttribute("name", Name);
			MergeAttribute("type", "file");
		}


		/* readonly properties */


		public string Name { get; }

	}

}
