using Ans.Net8.Common;
using Microsoft.AspNetCore.Html;
using System.Text;

namespace Ans.Net8.Web
{

    public static partial class _e
    {

		/*
		 * HtmlString ToHtml(this string value, bool useTypograf = false);
		 * HtmlString ToHtml(this string value, string template, bool useTypograf = false);
		 * HtmlString ToHtml(this StringBuilder value, bool useTypograf = false);
		 * 
		 * HtmlString Render(this string value, string template, bool expression = true);
		 * HtmlString Render(this string value, string template, string nullValue);
		 * HtmlString Render(this int value, string template, string format = null, int? nullValue = null);
		 * HtmlString Render(this int? value, string template, string format = null);
		 * HtmlString Render(this long value, string template, string format = null, long? nullValue = null);
		 * HtmlString Render(this long? value, string template, string format = null);
		 * HtmlString Render(this double value, string template, string format = null, double? nullValue = null);
		 * HtmlString Render(this double? value, string template, string format = null);
		 * HtmlString Render(this float value, string template, string format = null, float? nullValue = null);
		 * HtmlString Render(this float? value, string template, string format = null);
		 * HtmlString Render(this decimal value, string template, string format = null, decimal? nullValue = null);
		 * HtmlString Render(this decimal? value, string template, string format = null);
		 * HtmlString Render(this DateTime value, string template, string format = null);
		 * HtmlString Render(this DateTime? value, string template, string format = null, string emptyText = null);
		 * HtmlString Render(this DateOnly value, string template, string format = null);
		 * HtmlString Render(this DateOnly? value, string template, string format = null, string emptyText = null);
		 * HtmlString Render(this TimeOnly value, string template, string format = null);
		 * HtmlString Render(this TimeOnly? value, string template, string format = null, string emptyText = null);
		 * HtmlString Render(this bool value, string trueText, string falseText = null);
		 * 
		 * HtmlString RenderIf(this bool expression, string template, params object[] args);
		 * HtmlString RenderUseAddons(this string value, string template, params object[] addons);
		 * HtmlString RenderRepeats(this string value, int count, string resultTemplate = null, string itemTemplate = null, string itemsSeparator = null);
		 * HtmlString RenderFromCollection<T>(this IEnumerable<T> items, Func<T, string> itemExtractor, string resultTemplate, string itemTemplate, string itemsSeparator);
		 * HtmlString RenderFromCollection(this IEnumerable<string> items, string resultTemplate, string itemTemplate, string itemsSeparator);
		 * HtmlString RenderList(this string list, string resultTemplate, string itemTemplate, string itemsSeparator);
		 * HtmlString RenderUrl(this string url, string template);
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


        public static HtmlString ToHtml(
            this StringBuilder value,
            bool useTypograf = false)
        {
            return value.ToString()
                .ToHtml(useTypograf);
        }


        public static HtmlString Render(
            this string value,
            string template,
            bool expression = true)
        {
            return new HtmlString(value.Make(
                template, expression));
        }


        public static HtmlString Render(
            this string value,
            string template,
            string nullValue)
        {
            return new HtmlString(value.Make(
                template, nullValue));
        }


        public static HtmlString Render(
            this int value,
            string template,
            string format = null,
            int? nullValue = null)
        {
            return new HtmlString(value.Make(
                template, format, nullValue));
        }


        public static HtmlString Render(
            this int? value,
            string template,
            string format = null)
        {
            return new HtmlString(value.Make(
                template, format));
        }


        public static HtmlString Render(
            this long value,
            string template,
            string format = null,
            long? nullValue = null)
        {
            return new HtmlString(value.Make(
                template, format, nullValue));
        }


        public static HtmlString Render(
            this long? value,
            string template,
            string format = null)
        {
            return new HtmlString(value.Make(
                template, format));
        }


        public static HtmlString Render(
            this double value,
            string template,
            string format = null,
            double? nullValue = null)
        {
            return new HtmlString(value.Make(
                template, format, nullValue));
        }


        public static HtmlString Render(
            this double? value,
            string template,
            string format = null)
        {
            return new HtmlString(value.Make(
                template, format));
        }


        public static HtmlString Render(
            this float value,
            string template,
            string format = null,
            float? nullValue = null)
        {
            return new HtmlString(value.Make(
                template, format, nullValue));
        }


        public static HtmlString Render(
            this float? value,
            string template,
            string format = null)
        {
            return new HtmlString(value.Make(
                template, format));
        }


        public static HtmlString Render(
            this decimal value,
            string template,
            string format = null,
            decimal? nullValue = null)
        {
            return new HtmlString(value.Make(
                template, format, nullValue));
        }


        public static HtmlString Render(
            this decimal? value,
            string template,
            string format = null)
        {
            return new HtmlString(value.Make(
                template, format));
        }


        public static HtmlString Render(
            this DateTime value,
            string template,
            string format = null)
        {
            return new HtmlString(value.Make(
                template, format));
        }


        public static HtmlString Render(
            this DateTime? value,
            string template,
            string format = null,
            string emptyText = null)
        {
            return new HtmlString(value.Make(
                template, format, emptyText));
        }


        public static HtmlString Render(
            this DateOnly value,
			string template,
			string format = null)
		{
			return new HtmlString(value.Make(
				template, format));
		}


		public static HtmlString Render(
			this DateOnly? value,
			string template,
			string format = null,
			string emptyText = null)
		{
			return new HtmlString(value.Make(
				template, format, emptyText));
		}


		public static HtmlString Render(
			this TimeOnly value,
			string template,
			string format = null)
		{
			return new HtmlString(value.Make(
				template, format));
		}


		public static HtmlString Render(
			this TimeOnly? value,
			string template,
			string format = null,
			string emptyText = null)
		{
			return new HtmlString(value.Make(
				template, format, emptyText));
		}


		public static HtmlString Render(
            this bool value,
            string trueText,
            string falseText = null)
        {
            return new HtmlString(value.Make(
                trueText, falseText));
        }


        public static HtmlString RenderIf(
            this bool expression,
            string template,
            params object[] args)
        {
            return new HtmlString(expression.MakeIf(
                template, args));
        }


        public static HtmlString RenderUseAddons(
            this string value,
            string template,
            params object[] addons)
        {
            return new HtmlString(value.MakeUseAddons(
                template, addons));
        }


        public static HtmlString RenderRepeats(
            this string value,
            int count,
            string resultTemplate = null,
            string itemTemplate = null,
            string itemsSeparator = null)
        {
            return new HtmlString(value.MakeRepeats(
                count, resultTemplate, itemTemplate, itemsSeparator));
        }


        public static HtmlString RenderFromCollection<T>(
            this IEnumerable<T> items,
            Func<T, string> itemExtractor,
            string resultTemplate,
            string itemTemplate,
            string itemsSeparator)
        {
            return new HtmlString(items.MakeFromCollection(
                itemExtractor, resultTemplate, itemTemplate, itemsSeparator));
        }


        public static HtmlString RenderFromCollection(
            this IEnumerable<string> items,
            string resultTemplate,
            string itemTemplate,
            string itemsSeparator)
        {
            return new HtmlString(items.MakeFromCollection(
                resultTemplate, itemTemplate, itemsSeparator));
        }


        public static HtmlString RenderList(
            this string list,
            string resultTemplate,
            string itemTemplate,
            string itemsSeparator)
        {
            return new HtmlString(list.MakeList(
                resultTemplate, itemTemplate, itemsSeparator));
        }


        public static HtmlString RenderUrl(
            this string url,
            string template)
        {
            return new HtmlString(url.MakeUrl(
                template));
        }

    }

}
