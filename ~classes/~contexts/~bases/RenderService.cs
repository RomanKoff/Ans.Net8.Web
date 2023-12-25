using Ans.Net8.Common;
using Microsoft.AspNetCore.Html;
using System.Text;

namespace Ans.Net8.Web
{

	public class RenderService(
		ICurrentContext current)
		: _Current_Base(current)
	{

		/* properties */


		// meta
		public string Description { get; set; }
		public string Keywords { get; set; }
		public string Og_SiteName { get; set; }
		public string Og_Title { get; set; }
		public string Og_Description { get; set; }
		public string Og_Image { get; set; }
		public string Og_Url { get; set; }
		public string Og_Type { get; set; }


		/* functions */


		public HtmlString BrowserTitle()
		{
			if (_current.Page.CustomBrowserTitle != null)
				return _current.Page.CustomBrowserTitle.ToHtml();
			var items1 = new List<LinkBuilder>();
			if (_current.Site.HasBreadcrumbs)
				items1.AddRange(_current.Site.Breadcrumbs);
			if (_current.Node.HasBreadcrumbs)
				items1.Add(_current.Node.Breadcrumbs.Last());
			if (_current.Page.HasBreadcrumbs)
			{
				if (_current.Page.HideParentInTitle)
					items1.Add(_current.Page.Breadcrumbs.Last());
				else
					items1.AddRange(_current.Page.Breadcrumbs);
			}
			var a1 = items1.Select(x => x.InnerHtml).Reverse().ToArray();
			if (_current.Page.CustomTitle != null)
				a1[0] = _current.Page.CustomTitle;
			return SuppString.Join(null, null, " – ", a1)
				.ToHtml();
		}


		public HtmlString FaviconLink()
		{
			if (string.IsNullOrEmpty(_current.Site.CustomFaviconHref))
				return null;
			return $"<link rel=\"icon\" type=\"{_current.Site.CustomFaviconType}\" href=\"{_current.Site.CustomFaviconHref}\" />".ToHtml();
		}


		public HtmlString ManifestLink()
		{
			if (string.IsNullOrEmpty(_current.Site.ManifestJsonHref))
				return null;
			return $"<link rel=\"manifest\" href=\"{_current.Site.ManifestJsonHref}\" />".ToHtml();
		}


		public HtmlString AddonStylesheetLink()
		{
			if (string.IsNullOrEmpty(_current.Site.AddonStylesheetHref))
				return null;
			return $"<link href=\"{_current.Site.AddonStylesheetHref}\" rel=\"stylesheet\" {_current.Site.AddonStylesheetCrossorigin.Make("crossorigin =\"{0}\"")}/>".ToHtml();
		}


		public HtmlString Meta()
		{
			Og_SiteName ??= _current.Site.Title;
			Og_Title ??= _current.Page.ComplexTitle;
			Og_Description ??= Description;
			Og_Url ??= $"{_current.Host.ApplicationUrl[..^1]}{_current.Request.Url}";
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
			return sb1.ToString().ToHtml();
		}

	}

}
