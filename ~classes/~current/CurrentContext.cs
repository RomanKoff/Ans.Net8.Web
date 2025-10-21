using Ans.Net8.Common;
using Ans.Net8.Web.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Asn1.X509;
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
			string target)
		{
			/*
			 
				var target=
					"https://vk.com/guap_ru"
					"https://www.scopus.com/authid/detail.uri?authorId=57193163289|SCOPUS"
					"https://max.ru/guap_ru|ГУАП в Max|https://guap.ru/content/logos/max.svg"
					"https://my.saby.ru/page/knowledge-bases/6cee67dc-f6a6-48fe-8bc1-d98f75f63694?article=560bc13d-643d-4bfc-8787-0a479866fdc2&published=null&mode=readList||https://guap.ru/content/logos/saby.svg"
			 
				"Medias": {
					"//t.me/": "gi-logo-telegram",
					"//vk.com/": "gi-logo-vk",
					"//www.youtube.com/": "gi-logo-youtube",
					"//rutube.ru/": "gi-logo-rutube",
					"//dzen.ru/": "gi-logo-dzen",
					"//www.scopus.com/": "TEXT:Scopus",
					"//max.ru/": "IMG:https://guap.ru/content/logos/max.svg",
					".saby.ru/": "IMG:https://guap.ru/content/logos/saby.svg"
				},				

			 */

			var a1 = target.Split('|');
			var media1 = new MediaModel
			{
				Url = a1[0],
				Label = a1.Length > 1
					? a1[1] : null,
				Def = a1.Length > 2
					? new MediaDefModel(a1[2])
					: _getMediaDef(target)
			};
			return media1;
		}


#pragma warning disable CA1822 // Mark members as static
		public string GetMediaLink(
#pragma warning restore CA1822 // Mark members as static
			MediaModel media)
		{
			var label1 = media.Label?.ToHtml("<span>{0}</span>", true);
			return media.Def == null
				? $"<a target=\"_blank\" href=\"{media.Url}\">{label1}</a>"
				: media.Def.Type switch
				{
					MediaDefTypeEnum.Icon =>
						$"<a target=\"_blank\" class=\"text-nowrap icon-link-sm\" href=\"{media.Url}\"><i class=\"{media.Def.Inner}\"></i>{label1}</a>",
					MediaDefTypeEnum.Image =>
						$"<a target=\"_blank\" class=\"text-nowrap icon-link-sm\" href=\"{media.Url}\"><img src=\"{media.Def.Inner}\" />{label1}</a>",
					MediaDefTypeEnum.Text =>
						$"<a target=\"_blank\" class=\"text-nowrap icon-link-sm\" href=\"{media.Url}\">{media.Def.Inner.ToHtml("<span>{0}</span>", true)}{label1}</a>",
					_ => throw new NotImplementedException(),
				};
		}


		public string GetMediaLink(
			string url)
		{
			return GetMediaLink(
				GetMediaModel(url));
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


		private MediaDefModel _getMediaDef(
			string target)
		{
			var reg1 = GetAppReg("Medias");
			foreach (var item1 in reg1)
				if (target.Contains(item1.Key))
					return new MediaDefModel(item1.Value);
			return null;
		}

	}

}
