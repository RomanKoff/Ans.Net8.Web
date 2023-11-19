using Microsoft.AspNetCore.Html;
using System.Text;

namespace Ans.Net8.Web
{

	public class MetaModule
		: _ContextModule_Base
	{

		/* ctor */


		public MetaModule(
			ICurrentContext current)
			: base(current)
		{
		}


		/* properties */


		public string Description { get; set; }
		public string Keywords { get; set; }
		public string Og_SiteName { get; set; }
		public string Og_Title { get; set; }
		public string Og_Description { get; set; }
		public string Og_Image { get; set; }
		public string Og_Url { get; set; }
		public string Og_Type { get; set; }


		/* functions */


		public HtmlString Render()
		{
			//System.Diagnostics.Debug.WriteLine("[Ans.Net8.Web] Current.Meta.Render()");

			Og_SiteName ??= _current.Site.Title;
			Og_Title ??= _current.Page.ComplexTitle;
			Og_Description ??= Description;
			Og_Url ??= $"{_current.Host.ApplicationUrl[..^1]}{_current.Request.RequestPath}";
			Og_Type ??= "website";

			var sb1 = new StringBuilder();
			sb1.Append("\n\t");
			if (!string.IsNullOrEmpty(Description))
				sb1.Append(
					$"<meta name=\"description\" content=\"{Description}\"/>\n\t");
			if (!string.IsNullOrEmpty(Keywords))
				sb1.Append(
					$"<meta name=\"keywords\" content=\"{Keywords}\"/>\n\t");
			if (!string.IsNullOrEmpty(Og_SiteName))
				sb1.Append(
					$"<meta property=\"og:site_name\" content=\"{Og_SiteName}\"/>\n\t");
			if (!string.IsNullOrEmpty(Og_Title))
				sb1.Append(
					$"<meta property=\"og:title\" content=\"{Og_Title}\"/>\n\t");
			if (!string.IsNullOrEmpty(Og_Description))
				sb1.Append(
					$"<meta property=\"og:description\" content=\"{Og_Description}\"/>\n\t");
			if (!string.IsNullOrEmpty(Og_Image))
				sb1.Append(
					$"<meta property=\"og:image\" content=\"{Og_Image}\"/>\n\t");
			if (!string.IsNullOrEmpty(Og_Url))
				sb1.Append(
					$"<meta property=\"og:url\" content=\"{Og_Url}\"/>\n\t");
			if (!string.IsNullOrEmpty(Og_Type))
				sb1.Append(
					$"<meta property=\"og:type\" content=\"{Og_Type}\"/>\n\t");
			return new HtmlString(sb1.ToString());
		}

	}

}
