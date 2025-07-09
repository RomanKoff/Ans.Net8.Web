using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ans.Net8.Web
{

	public class CheckboxHtml
		: TagBuilderExt
	{

		public CheckboxHtml(
			string name,
			string id,
			string value,
			string title,
			bool isInline,
			bool isChecked)
			: base("div", TagRenderMode.Normal)
		{
			Name = name;
			Id = id;
			Value = value;
			Title = title ?? Value;
			IsInline = isInline;
			IsChecked = isChecked;
			AddCssClass("form-check");
			if (IsInline)
				AddCssClass("form-check-inline");
			var ctrl1 = new InputCheckboxTag(Name, Id, Value, IsChecked);
			var label1 = new TagBuilderExt("label", TagRenderMode.Normal);
			label1.AddCssClass("form-check-label");
			label1.MergeAttribute("for", Id);
			label1.InnerHtml.AppendHtml(Title);
			InnerHtml.AppendHtmlLine(ctrl1.ToString());
			InnerHtml.AppendHtmlLine(label1.ToString());
		}


		/* readonly properties */


		public string Name { get; }
		public string Id { get; }
		public string Value { get; }
		public string Title { get; }
		public bool IsInline { get; }
		public bool IsChecked { get; }

	}

}
