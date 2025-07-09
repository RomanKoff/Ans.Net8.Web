using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Encodings.Web;

namespace Ans.Net8.Web
{

	public static partial class _e_IHtmlHelper
	{

		public static string ToHtmlString(
			this IHtmlHelper source,
			IHtmlContent htmlContent)
		{
			var sb1 = new StringBuilder();
			using var writer1 = new StringWriter(sb1);
			var encoder1 = (HtmlEncoder)source.ViewContext.HttpContext.RequestServices.GetService(typeof(HtmlEncoder)) ?? HtmlEncoder.Default;
			htmlContent.WriteTo(writer1, encoder1);
			return sb1.ToString();
		}


		public static string StringFormat(
			this IHtmlHelper source,
			string format,
			params Func<dynamic, IHtmlContent>[] formatArgs)
		{
			var a1 = new List<string>();
			foreach (var item1 in formatArgs)
			{
				var sb1 = new StringBuilder();
				using var writer1 = new StringWriter(sb1);
				var encoder1 = (HtmlEncoder)source.ViewContext.HttpContext.RequestServices.GetService(typeof(HtmlEncoder)) ?? HtmlEncoder.Default;
				item1("").WriteTo(writer1, encoder1);
				a1.Add(sb1.ToString());
			}
			return string.Format(format, args: [.. a1]);
		}

	}

}
