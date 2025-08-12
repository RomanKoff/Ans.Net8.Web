using Ans.Net8.Common;
using Ans.Net8.Common.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Ans.Net8.Web
{

	public class LabelFieldTag
		: TagBuilderExt
	{

		/* ctor */


		public LabelFieldTag(
			string target,
			bool isRequired,
			CrudFaceHelper face,
			string[] errors)
			: base("label", TagRenderMode.Normal)
		{
			AddCssClass("form-label");
			MergeAttribute("for", target);
			if (isRequired)
				InnerHtml.AppendHtml(
					$"<i class=\"bi-exclamation-circle text-danger me-1\" title=\"{Form.Text_RequiredField}\"></i>");
			if (face.HasHelpLink)
				InnerHtml.AppendHtml(
					$"<a class=\"link-info me-1\" target=\"_blank\" href=\"{face.HelpLink}\" title=\"{Form.Text_Help}\"><i class=\"bi-question-circle\"></i></a>");
			InnerHtml.AppendHtml(face.TitleCalc);
			if (errors?.Length > 0)
				InnerHtml.AppendHtml(
					errors.MakeFromCollection(
						null, "<div class=\"text-danger\">{0}</div>", null));
		}


		/* properties */


		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext ViewContext { get; set; }

	}

}
