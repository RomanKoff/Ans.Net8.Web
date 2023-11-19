using Ans.Net8.Common;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Reflection.Emit;
using System.Resources;

namespace Ans.Net8.Web.TagHelpers
{

	public class AnsFromResourcesTagHelper
		: TagHelper
	{
		[HtmlAttributeNotBound]
		[ViewContext]
		public ViewContext ViewContext { get; set; }

		public ResourceManager Entity { get; set; }
		public ResourceManager Common { get; set; }

		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			output.TagMode = TagMode.SelfClosing;
			output.TagName = null;
			ViewContext.HttpContext.Items.Add(_Consts.FORM_RESOURCES_KEY, new ResourceManager[] { Entity, Common });
		}
	}



	public class AnsFieldTagHelper
		: TagHelper
	{
		[HtmlAttributeNotBound]
		[ViewContext]
		public ViewContext ViewContext { get; set; }

		public ModelExpression For { get; set; }

		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			var name1 = For.Name;
			//var form2=For.Model.

			var res1 = new ResourceHelper(ViewContext.HttpContext.Items, _Consts.FORM_RESOURCES_KEY);
			var face1 = res1.GetFace(name1);

			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = null;
			output.Content.AppendHtmlLine($"<div class=\"form-field my-5\">");

			// label
			output.Content.AppendHtml($"<label class=\"form-label\" for=\"{name1}\">");
			if (For.Metadata.GetRequiredAttribute() != null)
				output.Content.AppendHtml("<span class=\"text-danger\" title=\"обязательное поле\"><i class=\"bi-exclamation-circle\"></i></span>&nbsp;");
			output.Content.AppendHtml(face1.Title.TypografMin());
			if (face1.HasMoreLink)
				output.Content.AppendHtml($"&nbsp;<a class=\"field-morelink\" target=\"_blank\" href=\"{face1.MoreLink}\" title=\"дополнительная информация\"><i class=\"bi-question-circle\"></i></a>");
			output.Content.AppendHtmlLine("</label>");

			// content
			output.Content.AppendHtml(output.GetChildContentAsync().Result.GetContent());

			// validations			
			var errors1 = ViewContext.ViewData.ModelState.GetFieldErrors(name1);
			if (errors1?.Any() ?? false)
				foreach (var item1 in errors1)
					output.Content.AppendHtmlLine(@$"<div class=""field-errors text-danger lh-sm ps-1 pt-2"">{item1}</div>");

			if (face1.HasDescription || face1.HasSample)
			{
				output.Content.AppendHtmlLine("<div class=\"small opacity-75 lh-sm ps-1 pt-2\" style=\"max-width:40rem;\">");
				// description
				if (face1.HasDescription)
					output.Content.AppendHtmlLine($"<div class=\"field-description mb-1\">{face1.Description.TypografMin()}</div>");
				// sample
				if (face1.HasSample)
					output.Content.AppendHtmlLine($"<div class=\"field-sample mb-1\">Пример: {face1.Sample}</div>");
				output.Content.AppendHtmlLine("</div>");
			}

			output.Content.AppendHtmlLine("</div>");
		}
	}









	public class AnsListDisplayTagHelper
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
			var type1 = For.ModelExplorer.ModelType.GetCSharpTypeName(true);
			var value1 = For.Model;
			if (value1 == null)
				output.Content.AppendHtml("");
			else
			{
				var display1 = type1 switch
				{
					"bool" => ((bool)value1) ? "ДА" : "нет",
					"DateTime" => ((DateTime)value1).ToString(),
					_ => value1.ToString()
				};
				output.Content.AppendHtml(display1);
			}
		}

	}









	[HtmlTargetElement("span", Attributes = ValidationForAttributeName)]
	public class _ValidationMessageTagHelper0
		: TagHelper
	{
		private const string DataValidationForAttributeName = "data-valmsg-for";
		private const string ValidationForAttributeName = "asp-validation-for";

		public _ValidationMessageTagHelper0(
			IHtmlGenerator generator)
		{
			Generator = generator;
		}

		public override int Order
			=> -1000;

		[HtmlAttributeNotBound]
		[ViewContext]
		public ViewContext ViewContext { get; set; }

		protected IHtmlGenerator Generator { get; }

		[HtmlAttributeName(ValidationForAttributeName)]
		public ModelExpression For { get; set; }

		public override async Task ProcessAsync(
			TagHelperContext context,
			TagHelperOutput output)
		{
			ArgumentNullException.ThrowIfNull(context);
			ArgumentNullException.ThrowIfNull(output);
			if (For != null)
			{
				IDictionary<string, object> htmlAttributes = null;
				if (string.IsNullOrEmpty(For.Name) &&
					string.IsNullOrEmpty(ViewContext.ViewData.TemplateInfo.HtmlFieldPrefix) &&
					output.Attributes.ContainsName(DataValidationForAttributeName))
				{
					htmlAttributes = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
					{
						{ DataValidationForAttributeName, "-non-empty-value-" },
					};
				}
				string message = null;
				if (!output.IsContentModified)
				{
					var tagHelperContent = await output.GetChildContentAsync();
					if (!tagHelperContent.IsEmptyOrWhiteSpace)
					{
						message = tagHelperContent.GetContent();
					}
				}
				var tagBuilder = Generator.GenerateValidationMessage(
					ViewContext,
					For.ModelExplorer,
					For.Name,
					message: message,
					tag: null,
					htmlAttributes: htmlAttributes);
				if (tagBuilder != null)
				{
					output.MergeAttributes(tagBuilder);
					if (!output.IsContentModified && tagBuilder.HasInnerHtml)
					{
						output.Content.SetHtmlContent(tagBuilder.InnerHtml);
					}
				}
			}
		}

	}








}
