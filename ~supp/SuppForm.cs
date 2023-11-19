using Ans.Net8.Common;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace Ans.Net8.Web
{

	public static partial class SuppForm
	{

		/* 
		 * HtmlString Input(string type, string name, string value, string cssClass, int maxLength, bool isReadonly, bool isDisabled, object attributes);
		 * HtmlString InputHidden(string name, object value);
		 * HtmlString Textarea(string name, string value, string cssClass, int rows, bool isReadonly, bool isDisabled, object attributes);
		 * HtmlString Option(string value, string inner, bool isSelected, bool isDisabled, object attributes);
		 * HtmlString Select(string name, string value, Registry items, string cssClass, int size, bool isMultiple, bool isReadonly, bool isDisabled, object attributes);
		 */


		public static HtmlString Input(
			string type,
			string name,
			string value,
			string cssClass,
			int maxLength,
			bool isReadonly,
			bool isDisabled,
			object attributes)
		{
			var tag1 = new TagBuilder("input")
			{
				TagRenderMode = TagRenderMode.SelfClosing
			};
			tag1.MergeAttribute("type", type ?? "text");
			tag1.MergeAttribute("name", name);
			tag1.MergeAttribute("id", name);
			tag1.MergeAttributeIfPresent("value", value);
			tag1.AddCssClassIfPresent(cssClass);
			tag1.MergeAttributeIfNot0("maxlength", maxLength);
			tag1.MergeAttribute(isReadonly, "readonly");
			tag1.MergeAttribute(isDisabled, "disabled");
			return _getTagWithAttributes(tag1, attributes);
		}


		public static HtmlString InputHidden(
			string name,
			object value)
		{
			if (value == null)
				return HtmlString.Empty;
			var value1 = value.ToString();
			if (string.IsNullOrEmpty(value1))
				return HtmlString.Empty;
			return Input("hidden", name, value1, null, 0, false, false, null);
		}


		public static HtmlString Textarea(
			string name,
			string value,
			string cssClass,
			int rows,
			bool isReadonly,
			bool isDisabled,
			object attributes)
		{
			var tag1 = new TagBuilder("textarea")
			{
				TagRenderMode = TagRenderMode.Normal
			};
			tag1.MergeAttribute("name", name);
			tag1.MergeAttribute("id", name);
			tag1.MergeAttributeIfPresent("value", value);
			tag1.AddCssClassIfPresent(cssClass);
			tag1.MergeAttributeIfNot0("rows", rows);
			tag1.MergeAttribute(isReadonly, "readonly");
			tag1.MergeAttribute(isDisabled, "disabled");
			tag1.InnerHtml.AppendHtml(value);
			return _getTagWithAttributes(tag1, attributes);
		}


		public static HtmlString Option(
			string value,
			string inner,
			bool isSelected,
			bool isDisabled,
			object attributes)
		{
			var tag1 = new TagBuilder("option")
			{
				TagRenderMode = TagRenderMode.Normal
			};
			tag1.MergeAttribute("value", value);
			tag1.MergeAttribute(isSelected, "selected");
			tag1.MergeAttribute(isDisabled, "disabled");
			tag1.InnerHtml.AppendHtml(inner);
			return _getTagWithAttributes(tag1, attributes);
		}


		public static HtmlString Select(
			string name,
			string value,
			Registry items,
			string cssClass,
			int size,
			bool isMultiple,
			bool isReadonly,
			bool isDisabled,
			object attributes)
		{
			var tag1 = new TagBuilder("select")
			{
				TagRenderMode = TagRenderMode.Normal
			};
			tag1.MergeAttribute("name", name);
			tag1.MergeAttribute("id", name);
			tag1.AddCssClassIfPresent(cssClass);
			tag1.MergeAttributeIfNot0("size", size);
			tag1.MergeAttribute(isMultiple, "multiple");
			tag1.MergeAttribute(isReadonly, "readonly");
			tag1.MergeAttribute(isDisabled, "disabled");
			if (items.Count > 0)
			{
				var sb1 = new StringBuilder();
				bool f1 = false;
				value ??= string.Empty;
				var a1 = isMultiple
					? value.Split(';') : new[] { value };
				foreach (var item1 in items)
					if (item1.IsLabel)
					{
						if (f1)
							sb1.Append("</optgroup>");
						sb1.AppendLine($"<optgroup label=\"{item1.Value}\">");
						f1 = true;
					}
					else
					{
						string s1 = (item1.Level == 0)
							? item1.Value
							: "&nbsp;&nbsp;&nbsp; ".MakeRepeats(item1.Level) + item1.Value;
						sb1.AppendLine(Option(
							item1.Key, s1, (a1?.Contains(item1.Key) ?? false), false, null)
								.ToString());
					}
				if (f1)
					sb1.AppendLine("</optgroup>");
				tag1.InnerHtml.AppendHtml($"\n{sb1}");
			}
			return _getTagWithAttributes(tag1, attributes);
		}


		/* privates */


		private static HtmlString _getTagWithAttributes(
			this TagBuilder tag,
			object attributes)
		{
			if (attributes != null)
				foreach (var item1 in attributes.GetType().GetProperties())
					tag.MergeAttribute(
						item1.Name.Replace('_', '-'),
						item1.GetValue(attributes).ToString());
			return tag.ToHtml();
		}

	}

}
