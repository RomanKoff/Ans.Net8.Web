using Ans.Net8.Common;
using Microsoft.AspNetCore.Html;

namespace Ans.Net8.Web
{

	public static class SuppRender
	{

		/* functions */


		public static HtmlString SampleRu()
		{
			return SuppLangRu.Sample().ToHtml(true);
		}


		public static HtmlString SampleSmallRu()
		{
			return SuppLangRu.SampleSmall().ToHtml(true);
		}


		public static HtmlString SampleSmallerRu()
		{
			return SuppLangRu.SampleSmaller().ToHtml(true);
		}

	}

}
