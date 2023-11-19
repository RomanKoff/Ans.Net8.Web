using Ans.Net8.Common;
using Ans.Net8.Common.Services;
using Ans.Net8.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Globalization;

namespace Ans.Net8.Web
{

	/*
	 *	Add_AnsWeb
	 *		builder.Services.AddScoped<ICurrentContext, CurrentContext>();
	 */



	public interface ICurrentContext
	{
		/* readonly properties */

		IConfiguration Configuration { get; }
		HttpContext HttpContext { get; }
		IHttpClientFactory HttpClientFactory { get; }
		IMemoryCache MemoryCache { get; }
		IAnsCacheMap CacheMap { get; }
		IUrlHelper UrlHelper { get; }

		LibOptions Options { get; }
		CultureInfo Culture { get; }
		DateTimeHelper DateTimeHelper { get; }

		HostData Host { get; }
		RequestData Request { get; }
		SitemapData Sitemap { get; }

		AuthModule Auth { get; }
		CacheModule Cache { get; }
		CookiesModule Cookies { get; }
		MetaModule Meta { get; }
		NetworkModule Network { get; }
		QueryStringModule QueryString { get; }
		WebApiModule WebApi { get; }

		SiteContext Site { get; }
		NodeContext Node { get; }
		PageContext Page { get; }

		IEnumerable<LinkBuilder> Breadcrumbs { get; }
		bool HasBreadcrumbs { get; }
		bool AllowBreadcrumbs { get; }

		/* properties */

		string DefaultBrowserIconType { get; set; }
		string DefaultBrowserIconHref { get; set; }
		string DefaultManifestJsonHref { get; set; }
		string SystemLayout { get; set; }
		string ErrorsLayout { get; set; }

		/* functions */

		string GetLink(string target);
		HtmlString RenderBrowserTitle();
		HtmlString RenderBrowserIcon();
		HtmlString RenderManifest();
		HtmlString RenderAddonStylesheet();
		HtmlString RenderPageLink(string name, string title);
		HtmlString RenderPageLink(string name);
	}



	public class CurrentContext
		: ICurrentContext
	{

		/* ctor */


		public CurrentContext(
			IConfiguration configuration,
			IHttpContextAccessor httpContextAccessor,
			IHttpClientFactory httpClientFactory,
			IMemoryCache memoryCache,
			IAnsCacheMap cacheMap,
			IUrlHelper urlHelper,
			IViewRenderService viewRender,
			IMapNodesProvider nodesProvider,
			IMapPagesProvider pagesProvider,
			IAuthorizationPolicyProvider policyProvider,
			IPolicyEvaluator policyEvaluator)
		{
			Debug.WriteLine($"[Ans.Net8.Web] CurrentContext()");

			Configuration = configuration;
			HttpContext = httpContextAccessor.HttpContext;
			HttpClientFactory = httpClientFactory;
			MemoryCache = memoryCache;
			CacheMap = cacheMap;

			UrlHelper = urlHelper;

			Options = Configuration.GetLibOptions();
			Culture = CultureInfo.CurrentCulture;
			DateTimeHelper = new();

			Host = new(this);
			Request = new(this, viewRender);
			Sitemap = new(this, nodesProvider, pagesProvider);

			Auth = new(this, policyProvider, policyEvaluator);
			Cache = new(this);
			Cookies = new(this);
			Meta = new(this);
			Network = new(this);
			QueryString = new(this);
			WebApi = new(this);

			Site = new(this);
			Node = new(this);
			Page = new(this);
		}


		/* readonly properties */


		public IConfiguration Configuration { get; private set; }
		public HttpContext HttpContext { get; private set; }
		public IHttpClientFactory HttpClientFactory { get; private set; }
		public IMemoryCache MemoryCache { get; private set; }
		public IAnsCacheMap CacheMap { get; private set; }
		public IUrlHelper UrlHelper { get; private set; }

		public LibOptions Options { get; private set; }
		public CultureInfo Culture { get; private set; }
		public DateTimeHelper DateTimeHelper { get; private set; }

		public HostData Host { get; private set; }
		public RequestData Request { get; private set; }
		public SitemapData Sitemap { get; private set; }

		public AuthModule Auth { get; private set; }
		public CacheModule Cache { get; private set; }
		public CookiesModule Cookies { get; private set; }
		public MetaModule Meta { get; private set; }
		public NetworkModule Network { get; private set; }
		public QueryStringModule QueryString { get; private set; }
		public WebApiModule WebApi { get; private set; }

		public SiteContext Site { get; private set; }
		public NodeContext Node { get; private set; }
		public PageContext Page { get; private set; }


		public IEnumerable<LinkBuilder> Breadcrumbs
			=> Site.Breadcrumbs.Concat(
				Node.Breadcrumbs.Concat(
					Page.Breadcrumbs));
		//private IEnumerable<LinkBuilder> _breadcrumbs;


		public bool HasBreadcrumbs
			=> Breadcrumbs?.Any() ?? false;


		public bool AllowBreadcrumbs
			=> !Page.HideBreadcrumbs && HasBreadcrumbs;


		/* properties */


		public string DefaultBrowserIconType { get; set; }
		public string DefaultBrowserIconHref { get; set; }
		public string DefaultManifestJsonHref { get; set; }


		public string SystemLayout
		{
			get => _systemLayout
				?? Options.SystemLayout
				?? "_Layout";
			set => _systemLayout = value;
		}
		private string _systemLayout;


		public string ErrorsLayout
		{
			get => _errorsLayout
				?? Options.ExceptionHandler?.Layout
				?? SystemLayout;
			set => _errorsLayout = value;
		}
		private string _errorsLayout;


		/* functions */


		public HtmlString RenderBrowserTitle()
		{
			if (Page.CustomBrowserTitle != null)
				return Page.CustomBrowserTitle.ToHtml();
			var items1 = new List<LinkBuilder>();
			if (Site.HasBreadcrumbs)
				items1.AddRange(Site.Breadcrumbs);
			if (Node.HasBreadcrumbs)
				items1.Add(Node.Breadcrumbs.Last());
			if (Page.HasBreadcrumbs)
			{
				if (Page.HideParentInTitle)
					items1.Add(Page.Breadcrumbs.Last());
				else
					items1.AddRange(Page.Breadcrumbs);
			}
			var a1 = items1.Select(x => x.InnerHtml).Reverse().ToArray();
			if (Page.CustomTitle != null)
				a1[0] = Page.CustomTitle;
			return SuppString.Join(null, null, " – ", a1)
				.ToHtml();
		}


		public HtmlString RenderBrowserIcon()
		{
			var type1 = Site.BrowserIconType
				?? DefaultBrowserIconType;
			var href1 = Site.BrowserIconHref
				?? DefaultBrowserIconHref;
			if (string.IsNullOrEmpty(href1))
				return HtmlString.Empty;
			return $"<link rel=\"icon\" type=\"{type1}\" href=\"{href1}\" />".ToHtml();
		}


		public HtmlString RenderManifest()
		{
			var href1 = Site.ManifestJsonHref
				?? DefaultManifestJsonHref;
			if (string.IsNullOrEmpty(href1))
				return HtmlString.Empty;
			return $"<link rel=\"manifest\" href=\"{href1}\" />".ToHtml();
		}


		public HtmlString RenderAddonStylesheet()
		{
			if (string.IsNullOrEmpty(Site.AddonStylesheetHref))
				return HtmlString.Empty;
			return $"<link href=\"{Site.AddonStylesheetHref}\" rel=\"stylesheet\" {Site.AddonStylesheetCrossorigin.Make("crossorigin =\"{0}\"")}/>".ToHtml();
		}


		public string GetLink(
			string target)
		{
			if (string.IsNullOrEmpty(target))
				return string.Empty;
			if (target[0] == '/')
				return $"{Host.VirtualPath}{target[1..]}";
			return target;
		}


		public HtmlString RenderPageLink(
			string name,
			string title)
		{
			var link1 = new LinkBuilder(
				$"{Request.RequestPath}/{name}", title);
			return link1.GetTag().ToHtml();
		}


		public HtmlString RenderPageLink(
			string name)
		{
			return RenderPageLink(name, name);
		}

	}

}
