using Ans.Net8.Common;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Ans.Net8.Web
{

	public static partial class _e_TagHelperOutput
	{

		/* methods */


		public static void AddAttributeIfPresent(
			this TagHelperOutput output,
			string name,
			params string[] values)
		{
			var s1 = values.MakeFromCollection(null, null, " ");
			if (!string.IsNullOrEmpty(s1))
				output.Attributes.Add(name, s1);
		}


		public static void AppendHtml(
			this TagHelperOutput output,
			string encoded)
		{
			output.Content.AppendHtml(encoded);
		}


		/* functions */


		public static string GetChildContent(
			this TagHelperOutput output)
		{
			return output.GetChildContentAsync().Result.GetContent();
		}

	}

}
