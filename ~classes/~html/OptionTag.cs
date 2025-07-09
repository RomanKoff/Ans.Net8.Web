using Ans.Net8.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ans.Net8.Web
{

	public class OptionTag
		: TagBuilderExt
	{

		/* ctor */


		public OptionTag(
			string value,
			string inner,
			int level,
			bool selected)
			: base("option", TagRenderMode.Normal)
		{
			Value = value;
			Inner = inner;
			Level = level;
			IsSelected = selected;
			MergeAttribute("value", Value);
			if (IsSelected)
				MergeAttribute("selected", "selected");
			InnerHtml.AppendHtml(
				$"{_Consts.OPTION_TABS.MakeRepeats(Level)}{Inner}");
		}


		/* readonly properties */


		public string Value { get; }
		public string Inner { get; }
		public int Level { get; }
		public bool IsSelected { get; }

	}

}
