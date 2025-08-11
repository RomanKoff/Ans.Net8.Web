using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Ans.Net8.Web.TagHelpers
{

	/*
	 * <sample-ru />
	 * <sample-ru-small />
	 * <sample-ru-smaller />
	 * 
	 * // <sample-code>content</sample-code>
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
			output.Content.AppendHtml(SuppRender.SampleRu());
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
			output.Content.AppendHtml(SuppRender.SampleSmallRu());
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
			output.Content.AppendHtml(SuppRender.SampleSmallerRu());
		}
	}



	//public class SampleCodeTagHelper(
	//	CurrentContext current)
	//	: _AnsTagHelper_Base(current)
	//{
	//	private const string Lang_AttributeName = "lang";
	//	/* properties */
	//	[HtmlAttributeName(Lang_AttributeName)]
	//	public string LangData { get; set; }
	//	/* methods */
	//	public override void Process(
	//		TagHelperContext context,
	//		TagHelperOutput output)
	//	{
	//		output.TagName = null;
	//		var content1 = SuppTypograph.GetHtml2Text(GetContent(output));
	//		output.Content.AppendHtml($"<pre><code class=\"language-{LangData ?? "html"} line-numbers\">");
	//		output.Content.AppendHtml(content1);
	//		output.Content.AppendHtml($"</code></pre>");
	//	}
	//}

}
