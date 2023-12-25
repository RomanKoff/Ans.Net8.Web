using Ans.Net8.Common;
using Microsoft.AspNetCore.Html;

namespace Ans.Net8.Web
{

	public static partial class _e
	{

		/*
		 * HtmlString ToHtml(this string value, bool useTypograf = false);
		 * HtmlString ToHtml(this string value, string template, bool useTypograf = false);		 
		 * HtmlString ToHtmlIf(this string value, bool expression, bool useTypograf = false);
		 * HtmlString ToHtmlIf(this string value, bool expression, string template, bool useTypograf = false);		 
		 */


		public static HtmlString ToHtml(
			this string value,
			bool useTypograf = false)
		{
			if (string.IsNullOrEmpty(value))
				return HtmlString.Empty;
			return new HtmlString(
				useTypograf ? value.TypografMin() : value);
		}


		public static HtmlString ToHtml(
			this string value,
			string template,
			bool useTypograf = false)
		{
			return value.Make(template)
				.ToHtml(useTypograf);
		}


		public static HtmlString ToHtmlIf(
			this string value,
			bool expression,
			bool useTypograf = false)
		{
			return (expression)
				? value.ToHtml(useTypograf)
				: HtmlString.Empty;
		}


		public static HtmlString ToHtmlIf(
			this string value,
			bool expression,
			string template,
			bool useTypograf = false)
		{
			return (expression)
				? value.ToHtml(template, useTypograf)
				: HtmlString.Empty;
		}

	}

}
