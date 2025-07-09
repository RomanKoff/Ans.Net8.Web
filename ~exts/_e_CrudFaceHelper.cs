using Ans.Net8.Common;
using Microsoft.AspNetCore.Html;

namespace Ans.Net8.Web
{

	public static partial class _e_CrudFaceHelper
	{

		/* functions */


		public static HtmlString TitleHtml(
			this CrudFaceHelper helper)
		{
			return helper.Title.ToHtml(true);
		}


		public static HtmlString ShortTitleHtml(
			this CrudFaceHelper helper)
		{
			return helper.ShortTitle.ToHtml(true);
		}


		public static HtmlString DescriptionHtml(
			this CrudFaceHelper helper)
		{
			return helper.Description.ToHtml(true);
		}


		public static HtmlString SampleHtml(
			this CrudFaceHelper helper)
		{
			return helper.Sample.ToHtml(true);
		}

	}

}
