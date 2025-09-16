using Ans.Net8.Common;
using Ans.Net8.Web.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Net;

namespace Ans.Net8.Web
{

	/*
	 *	LibStartup.Add_AnsNet8Web()
	 *		builder.Services.AddScoped<CurrentContext>();
	 */



	public class CurrentContext
	{

		/* ctor */


		public CurrentContext(
			IConfiguration configuration,
			IHttpContextAccessor httpContextAccessor,
			IHttpClientFactory httpClientFactory,
			IMemoryCache memoryCache,
			IUrlHelper urlHelper,
			IViewRenderService viewRender,
			IMapNodesProvider mapNodesProvider,
			IMapPagesProvider mapPagesProvider)
		{
			Configuration = configuration;
			HttpContext = httpContextAccessor.HttpContext;
			HttpClientFactory = httpClientFactory;
			MemoryCache = memoryCache;
			UrlHelper = urlHelper;

			Options = Configuration.GetOptions_AnsNet8Web();
			Culture = CultureInfo.CurrentCulture;
			DateTimeHelper = new();

			Host = new(this);
			Request = new(this, viewRender);
			Maps = new(this, mapNodesProvider, mapPagesProvider);
			Meta = new(this);

			Site = new(this);
			Node = new(this);
			Page = new(this);

			Cache = new(this);
			Cookies = new(this);
			Network = new(this);
			QueryString = new(this);
			WebApi = new(this);
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
		public MetaData Meta { get; }

		public SiteProfile Site { get; }
		public NodeProfile Node { get; }
		public PageProfile Page { get; }

		public CacheService Cache { get; }
		public CookiesService Cookies { get; }
		public NetworkService Network { get; }
		public QueryStringService QueryString { get; }
		public WebApiService WebApi { get; }


		private IEnumerable<LinkBuilder> _breadcrumbs;
		public IEnumerable<LinkBuilder> Breadcrumbs
			=> _breadcrumbs ??= _getBreadcrumbs();


		private string[] _browserTitleItems;
		public string[] BrowserTitleItems
			=> _browserTitleItems ??= _getBrowserTitleItems();


		public bool HasBreadcrumbs
			=> Breadcrumbs?.Count() > 0;


		public bool HasBrowserTitleItems
			=> BrowserTitleItems?.Length > 0;


		/* properties */


		private string _defaultLayout;
		public string DefaultLayout
		{
			get => _defaultLayout ??= Options.DefaultLayout;
			set { _defaultLayout = value; }
		}


		private string _systemLayout;
		public string SystemLayout
		{
			get => _systemLayout ??= (Options.SystemLayout ?? DefaultLayout);
			set { _systemLayout = value; }
		}


		private string _errorsLayout;
		public string ErrorsLayout
		{
			get => _errorsLayout ??= (Options.Errors.Layout ?? SystemLayout);
			set { _errorsLayout = value; }
		}


		/* methods */


		public void SetOn(
			string key)
		{
			HttpContext.Items.Add(
				_getKeyOn(key), "on");
		}


		public void SetOff(
			string key)
		{
			HttpContext.Items.Add(
				_getKeyOff(key), "off");
		}


		public void SetData(
			string key,
			Func<dynamic, IHtmlContent> value)
		{
			HttpContext.Items[key] = HttpContext.GetStringFromRazor(value);
		}


		public void SetData(
			string key,
			string value)
		{
			HttpContext.Items[key] = value;
		}

		public void SetData(
			string key,
			int value)
		{
			HttpContext.Items[key] = value;
		}

		public void SetData(
			string key,
			bool value)
		{
			HttpContext.Items[key] = value;
		}

		public void SetData(
			string key,
			DateTime value)
		{
			HttpContext.Items[key] = value;
		}

		public void SetData(
			string key,
			DateOnly value)
		{
			HttpContext.Items[key] = value;
		}

		public void SetData(
			string key,
			TimeOnly value)
		{
			HttpContext.Items[key] = value;
		}


#pragma warning disable CA1822 // Mark members as static

		public void ThrowNotFound()

			=> throw new AnsHttpException(HttpStatusCode.NotFound);


		public void ThrowForbidden()
			=> throw new AnsHttpException(HttpStatusCode.Forbidden);

#pragma warning restore CA1822 // Mark members as static

		/* functions */


		public HtmlString BrowserTitle()
		{
			if (Page.CustomBrowserTitle != null)
				return Page.CustomBrowserTitle.ToHtml();
			return BrowserTitleItems
				.MakeFromCollection(null, null, " – ")
				.ToHtml();
		}


		public string GetUrl(
			string target)
		{
			if (string.IsNullOrEmpty(target))
				return null;
			if (target[0] == '/')
				return $"{Site.Url}{target}";
			if (target.StartsWith("site:"))
				return $"{Site.Url}{target[5..].Make("/{0}")}";
			if (target.StartsWith("node:"))
				return $"{Node.Url}{target[5..].Make("/{0}")}";
			if (target.StartsWith("page:"))
				return $"{Page.Url}{target[5..].Make("/{0}")}";
			return target;
		}


		public string GetResUrl(
			string target)
		{
			if (string.IsNullOrEmpty(target))
				return null;
			if (target[0] == '/')
				return $"{Site.ResUrl}{target}";
			if (target.StartsWith("site:"))
				return $"{Site.ResUrl}{target[5..].Make("/{0}")}";
			if (target.StartsWith("node:"))
				return $"{Node.ResUrl}{target[5..].Make("/{0}")}";
			if (target.StartsWith("page:"))
				return $"{Page.ResUrl}{target[5..].Make("/{0}")}";
			return target;
		}


		public bool IsOn(
			string key)
		{
			return HttpContext.Items.ContainsKey(
				_getKeyOn(key));
		}


		public bool IsNotOff(
			string key)
		{
			return !HttpContext.Items.ContainsKey(
				_getKeyOff(key));
		}


		public bool HasDataAny(
			params string[] keys)
		{
			foreach (var key1 in keys)
				if (HttpContext.Items.ContainsKey(key1))
					return true;
			return false;
		}


		public bool HasDataAll(
			params string[] keys)
		{
			foreach (var key1 in keys)
				if (!HttpContext.Items.ContainsKey(key1))
					return false;
			return true;
		}


		public string GetData(
			string key,
			string defaultValue = null)
		{
			return (string)HttpContext.Items[key] ?? defaultValue;
		}


		public int GetDataInt(
			string key,
			int defaultValue = 0)
		{
			var value1 = HttpContext.Items[key];
			return value1 == null ? defaultValue : (int)value1;
		}


		public bool GetDataBool(
			string key)
		{
			var value1 = HttpContext.Items[key];
			return value1 != null && (bool)value1;
		}


		public DateTime? GetDataDateTime(
			string key,
			DateTime? defaultValue = null)
		{
			return (DateTime?)HttpContext.Items[key] ?? defaultValue;
		}


		public DateOnly? GetDataDateOnly(
			string key,
			DateOnly? defaultValue = null)
		{
			return (DateOnly?)HttpContext.Items[key] ?? defaultValue;
		}


		public TimeOnly? GetDataTimeOnly(
			string key,
			TimeOnly? defaultValue = null)
		{
			return (TimeOnly?)HttpContext.Items[key] ?? defaultValue;
		}


		private Dictionary<string, Dictionary<string, string>> _appRegs;
		public Dictionary<string, string> GetAppReg(
			string regName)
		{
			_appRegs ??= Configuration.GetOptions_AnsNet8Web().Regs;
			return _appRegs.GetValueOrDefault(regName, null);
		}


		public string GetAppRegValue(
			string regName,
			string key,
			string defaultValue = null)
		{
			return GetAppReg(regName)?.GetValueOrDefault(key, defaultValue);
		}


		public MediaModel GetMediaModel(
			string url)
		{
			if (string.IsNullOrEmpty(url))
				return null;
			var reg1 = GetAppReg("Medias");
			foreach (var item1 in reg1)
			{
				var i1 = url.IndexOf(item1.Key);
				if (i1 != -1)
				{
					if (item1.Value[0] == '@')
						return new MediaModel
						{
							Mode = MediaModeEnum.Other,
							Title = item1.Value[1..],
							Url = url,
						};
					return new MediaModel
					{
						Mode = MediaModeEnum.Known,
						Title = url[(i1 + item1.Key.Length)..],
						Icon = item1.Value,
						Url = url,
					};
				}
			}
			return new MediaModel
			{
				Mode = MediaModeEnum.Other,
				Title = url,
				Url = url,
			};
		}


		public string GetMediaLink(string url)
		{
			var media1 = GetMediaModel(url);
			return media1.Mode switch
			{
				MediaModeEnum.Known => $"<a target=\"_blank\" class=\"text-nowrap icon-link-sm\" href=\"{media1.Url}\"><i class=\"{media1.Icon}\"></i>{media1.Title}</a>",
				MediaModeEnum.Other => $"<a target=\"_blank\" class=\"text-nowrap\" href=\"{media1.Url}\">{media1.Title}</a>",
				_ => null
			};
		}


		/* privates */


		private IEnumerable<LinkBuilder> _getBreadcrumbs()
		{
			var items1 = new List<LinkBuilder>();
			items1.AddRange(Site.ParentsLinks);
			var url1 = Request.IsStartSite
				? null : SuppValues.Default(Site.Url, "/");
			items1.Add(new LinkBuilder(url1, Site.ShortTitle));
			items1.AddRange(Node.ParentsLinks);
			if (Node.NodeItem != null)
				items1.Add(Site.GetNodeLink(Node.NodeItem));
			items1.AddRange(Page.ParentsLinks);
			if (Page.PageItem != null)
				items1.Add(Page.PageItem.Link);
			return items1.AsEnumerable();
		}


		private string[] _getBrowserTitleItems()
		{
			var items1 = new List<string>();
			items1.AddRange(Site.ParentsLinks.Select(x => x.InnerHtml));
			items1.Add(Site.ShortTitle);
			items1.Add(Node.ShortTitle);
			items1.Add($"{Page.ParentsTitles.Make("{0}. ")}{Page.ShortTitle}");
			items1.Reverse();
			return [.. items1];
		}


		private static string _getKeyOn(string key)
		{
			return $"{key}_on";
		}


		private static string _getKeyOff(string key)
		{
			return $"{key}_off";
		}

	}

}
