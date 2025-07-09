using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;

namespace Ans.Net8.Web
{

	public class TagBuilderExt
		: TagBuilder
	{

		/* ctor */


		public TagBuilderExt(
			string tagName,
			TagRenderMode mode)
			: base(tagName)
		{
			TagRenderMode = mode;
		}


		/* methods */


		public void Prepare(
			string cssClasses,
			string styles,
			string attributes)
		{
			if (!string.IsNullOrEmpty(cssClasses))
				AddCssClass(cssClasses);
			if (!string.IsNullOrEmpty(styles))
				this.AddStyle(styles);
			if (!string.IsNullOrEmpty(attributes))
				foreach (var item1 in attributes.Split(';'))
				{
					var a1 = item1.Split('=');
					MergeAttribute(a1[0], a1[1]);
				}
		}


		/* functions */


		public override string ToString()
		{
			var sw1 = new StringWriter();
			WriteTo(sw1, HtmlEncoder.Default);
			return sw1.ToString();
		}


		public HtmlString ToHtml()
		{
			return new HtmlString(ToString());
		}

	}

}
