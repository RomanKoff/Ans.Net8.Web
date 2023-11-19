using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Ans.Net8.Web.TagHelpers
{
	
	public class FormInTextTagHelper
		: TagHelper
	{

		[HtmlAttributeName("for")]
		public ModelExpression For { get; set; }

		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = null;
			var name1 = For.Name;
			var type1 = For.ModelExplorer.ModelType;
			var isRequired1 = For.Metadata.IsRequired;
			var maxLength1 = For.Metadata.GetMaxLengthAttribute()?.Length ?? 0;
			var regex1 = For.Metadata.GetRegularExpressionAttribute()?.Pattern;
			var range1 = For.Metadata.GetRangeAttribute();
			var min1 = range1?.Minimum.ToString();
			var max1 = range1?.Maximum.ToString();
			var desc1 = For.Metadata.Description;
			var s1 = @$"{name1}:{type1} ({maxLength1}, {isRequired1}, '{regex1}', '{desc1}')";
			output.Content.AppendHtml(s1);
		}

	}

}
