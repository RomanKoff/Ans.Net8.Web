using Ans.Net8.Common;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Ans.Net8.Web.TagHelpers
{

	/*
	 * <sample-ru />
	 * <sample-ru-small />
	 * <sample-ru-smaller />
     */



	public class SampleRuTagHelper
		: TagHelper
	{
		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = null;
			output.Content.AppendHtml(SuppText.SampleRu());
		}
	}



	public class SampleRuSmallTagHelper
		: TagHelper
	{
		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = null;
			output.Content.AppendHtml(SuppText.SampleSmallRu());
		}
	}



	public class SampleRuSmallerTagHelper
		: TagHelper
	{
		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = null;
			output.Content.AppendHtml(SuppText.SampleSmallerRu());
		}
	}

}
