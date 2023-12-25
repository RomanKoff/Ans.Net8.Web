using Ans.Net8.Common;
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
	 * Add_AnsWeb
	 *		builder.Services.AddScoped<ICurrentContext, CurrentContext>();
	 */



	public interface ICurrentContext
	{
		/* readonly properties */

		IConfiguration Configuration { get; }
		HttpContext HttpContext { get; }
		IHttpClientFactory HttpClientFactory { get; }
		IMemoryCache MemoryCache { get; }
		IUrlHelper UrlHelper { get; }

		LibOptions Options { get; }
		CultureInfo Culture { get; }
		DateTimeHelper DateTimeHelper { get; }

		HostData Host { get; }
		RequestData Request { get; }
		MapsData Maps { get; }

		AuthService Auth { get; }
		CacheService Cache { get; }
		CookiesService Cookies { get; }
		NetworkService Network { get; }
		QueryStringService QueryString { get; }
		RenderService Render { get; }
		WebApiService WebApi { get; }

		SiteProfile Site { get; }
		NodeProfile Node { get; }
		PageProfile Page { get; }

		IEnumerable<LinkBuilder> Breadcrumbs { get; }
		bool HasBreadcrumbs { get; }
		bool AllowBreadcrumbs { get; }

		/* properties */

		string SystemLayout { get; set; }
		string ErrorsLayout { get; set; }

		/* functions */

		string FixUrl(string target, bool useAbsoluteUrl = false);

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
			IUrlHelper urlHelper,
			IAuthorizationPolicyProvider policyProvider,
			IPolicyEvaluator policyEvaluator,
			IViewRenderService viewRender,
			IMapNodesProvider mapNodesProvider,
			IMapPagesProvider mapPagesProvider)
		{
			Debug.WriteLine($"[Ans.Net8.Web] CurrentContext()");

			Configuration = configuration;
			HttpContext = httpContextAccessor.HttpContext;
			HttpClientFactory = httpClientFactory;
			MemoryCache = memoryCache;
			UrlHelper = urlHelper;

			Options = Configuration.GetLibOptions();
			Culture = CultureInfo.CurrentCulture;
			DateTimeHelper = new();

			Host = new(this);
			Request = new(this, viewRender);
			Maps = new(this, mapNodesProvider, mapPagesProvider);

			Auth = new(this, policyProvider, policyEvaluator);
			Cache = new(this);
			Cookies = new(this);
			Network = new(this);
			QueryString = new(this);
			Render = new(this);
			WebApi = new(this);

			Site = new(this);
			Node = new(this);
			Page = new(this);
		}


		/* readonly properties */


		public IConfiguration Configuration { get; }
		public HttpContext HttpContext { get; }
		public IHttpClientFactory HttpClientFactory { get; }
		public IMemoryCache MemoryCache { get; }
		public IUrlHelper UrlHelper { get; }

		public LibOptions Options { get; }
		public CultureInfo Culture { get; }
		public DateTimeHelper DateTimeHelper { get; }

		public HostData Host { get; }
		public RequestData Request { get; }
		public MapsData Maps { get; }

		public AuthService Auth { get; }
		public CacheService Cache { get; }
		public CookiesService Cookies { get; }
		public NetworkService Network { get; }
		public QueryStringService QueryString { get; }
		public RenderService Render { get; }
		public WebApiService WebApi { get; }

		public SiteProfile Site { get; }
		public NodeProfile Node { get; }
		public PageProfile Page { get; }

		public IEnumerable<LinkBuilder> Breadcrumbs
			=> Site.Breadcrumbs.Concat(
				Node.Breadcrumbs.Concat(
					Page.Breadcrumbs));

		public bool HasBreadcrumbs
			=> Breadcrumbs?.Count() > 0;

		public bool AllowBreadcrumbs
			=> !Page.HideBreadcrumbs && HasBreadcrumbs;


		/* properties */


		private string _systemLayout;
		public string SystemLayout
		{
			get => _systemLayout ?? Options.SystemLayout ?? "_Layout";
			set => _systemLayout = value;
		}

		private string _errorsLayout;
		public string ErrorsLayout
		{
			get => _errorsLayout ?? Options.ExceptionHandler?.Layout ?? SystemLayout;
			set => _errorsLayout = value;
		}


		/* functions */


		/// <param name="target">
		/// 's:*' - site resource;
		/// 'n:*' - node resource;
		/// 'p:*' - page resource;
		/// '/*' - site root.
		/// </param>
		public string FixUrl(
			string target,
			bool useAbsoluteUrl = false)
		{
			if (string.IsNullOrEmpty(target))
				return null;
			if (target.Length < 3)
				return target;
			if (target[0] == '/')
				return $"{((useAbsoluteUrl) ? Host.BaseUrl : null)}{Host.VirtualPath}{target[1..]}";
			return target[0..2] switch
			{
				"s:" => Site.GetResUrl(target[2..], useAbsoluteUrl),
				"n:" => Node.GetResUrl(target[2..], useAbsoluteUrl),
				"p:" => Page.GetResUrl(target[2..], useAbsoluteUrl),
				_ => target
			};
		}

	}

}
