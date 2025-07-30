using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Ans.Net8.Web
{

	public class _TagHelper_Base
		: TagHelper
	{

		/* properties */


		public bool UseAutoContent { get; set; }
		public string ClassBefore { get; set; }
		public string Class { get; set; }
		public string ClassAfter { get; set; }
		public string Style { get; set; }
		public string Title { get; set; }
		public string DefaultContent { get; set; } = null;
		public string CustomContent { get; set; } = null;


		/* methods */


		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			var content1 = GetContent(output);
			if (UseAutoContent && content1 == null)
			{
				output.TagName = null;
				output.Content.Clear();
				return;
			}
			output.AddAttributeIfPresent("class", ClassBefore, Class, ClassAfter);
			output.AddAttributeIfPresent("style", Style);
			output.AddAttributeIfPresent("title", Title);
			MakeBefore(output);
			output.Content.AppendHtml(content1);
			MakeAfter(output);
		}


		public virtual void MakeBefore(
			TagHelperOutput output)
		{
		}


		public virtual void MakeAfter(
			TagHelperOutput output)
		{
		}


		/* functions */


		public string GetContent(
			TagHelperOutput output)
		{
			if (CustomContent != null)
				return CustomContent;
			var content1 = output.GetChildContent();
			return (string.IsNullOrWhiteSpace(content1))
				? DefaultContent : content1;
		}


		//public static string GetIconCode(
		//	string icon,
		//	string cssClass = null,
		//	string cssStyle = null)
		//{
		//	if (string.IsNullOrEmpty(icon))
		//		return null;
		//	var css1 = cssClass ?? "me-1";
		//	var style1 = cssStyle ?? "font-size:1.3em;";
		//	var template1 = $"<i class=\"{{0}} {css1}\" style=\"{style1}\"></i>";
		//	return string.Format(template1, icon);
		//}

	}

}
