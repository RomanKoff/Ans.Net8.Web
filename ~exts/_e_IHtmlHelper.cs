using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Encodings.Web;

namespace Ans.Net8.Web
{

	public static partial class _e_IHtmlHelper
	{

		public static string GetStringFromHtml(
			this IHtmlHelper helper,
			Func<dynamic, IHtmlContent> html)
		{
			var sb1 = new StringBuilder();
			using TextWriter writer1 = new StringWriter(sb1);
			var encoder1 = (HtmlEncoder)helper.ViewContext.HttpContext.RequestServices
				.GetService(typeof(HtmlEncoder)) ?? HtmlEncoder.Default;
			html("").WriteTo(writer1, encoder1);
			return sb1.ToString();
		}


		public static string GetStringFromHtml(
			this IHtmlHelper helper,
			string template,
			params Func<dynamic, IHtmlContent>[] args)
		{
			var _args1 = new List<string>();
			foreach (var item1 in args)
			{
				var sb1 = new StringBuilder();
				using TextWriter writer1 = new StringWriter(sb1);
				var encoder1 = (HtmlEncoder)helper.ViewContext.HttpContext.RequestServices
					.GetService(typeof(HtmlEncoder)) ?? HtmlEncoder.Default;
				item1("").WriteTo(writer1, encoder1);
				_args1.Add(sb1.ToString());
			}
			return string.Format(template, [.. _args1]);
		}

	}

}
