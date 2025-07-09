using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ans.Net8.Web
{

	public class InputHiddenTag
		: TagBuilderExt
	{

		/* ctor */


		public InputHiddenTag(
			string name,
			string value)
			: base("input", TagRenderMode.SelfClosing)
		{
			Name = name;
			Value = value;
			MergeAttribute("name", Name);
			MergeAttribute("type", "hidden");
			if (!string.IsNullOrEmpty(Value))
				MergeAttribute("value", Value);
		}


		/* readonly properties */


		public string Name { get; }
		public string Value { get; }

	}

}
