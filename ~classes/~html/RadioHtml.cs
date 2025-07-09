using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ans.Net8.Web
{

	public class RadioHtml
		: TagBuilderExt
	{

		public RadioHtml(
			string name,
			string value,
			string title,
			string key,
			bool isInline,
			bool isChecked)
			: base("div", TagRenderMode.Normal)
		{
			Name = name;
			Value = value;
			Title = title ?? Value;
			Key = key ?? Value;
			Id = $"{Name}_{Key}";
			IsInline = isInline;
			IsChecked = isChecked;
			AddCssClass("form-check");
			if (IsInline)
				AddCssClass("form-check-inline");
			var ctrl1 = new InputRadioTag(Name, Id, Value, IsChecked);
			var label1 = new TagBuilderExt("label", TagRenderMode.Normal);
			label1.AddCssClass("form-check-label");
			label1.MergeAttribute("for", Id);
			label1.InnerHtml.AppendHtml(Title);
			InnerHtml.AppendHtmlLine(ctrl1.ToString());
			InnerHtml.AppendHtmlLine(label1.ToString());
		}


		/* readonly properties */


		public string Name { get; }
		public string Value { get; }
		public string Title { get; }
		public string Key { get; }
		public string Id { get; }
		public bool IsInline { get; }
		public bool IsChecked { get; }

	}

}
