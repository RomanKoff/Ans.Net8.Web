using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ans.Net8.Web
{

	public class TextareaTag
		: TagBuilderExt
	{

		/* ctor */


		public TextareaTag(
			string name,
			string value)
			: base("textarea", TagRenderMode.Normal)
		{
			Name = name;
			Value = value;
			MergeAttribute("id", Name);
			MergeAttribute("name", Name);
			InnerHtml.AppendHtml(Value);
		}


		/* readonly properties */


		public string Name { get; }
		public string Value { get; }

	}

}
