using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ans.Net8.Web
{

	public class InputTextTag
		: TagBuilderExt
	{

		/* ctor */


		public InputTextTag(
			string name,
			string value)
			: base("input", TagRenderMode.SelfClosing)
		{
			Name = name;
			Value = value;
			MergeAttribute("id", Name);
			MergeAttribute("name", Name);
			MergeAttribute("type", "text");
			if (!string.IsNullOrEmpty(Value))
				MergeAttribute("value", Value);
		}


		/* readonly properties */


		public string Name { get; }
		public string Value { get; }

	}

}
