using Ans.Net8.Common;
using Ans.Net8.Web.Services;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Routing;
using System.Text;

namespace Ans.Net8.Web
{

	public class RequestData
	{

		private readonly CurrentContext _current;
		

		/* ctor */


		public RequestData(
			CurrentContext current)
		{
			_current = current;
			RelativeUrl = _current.HttpContext.Request.Path.ToString().ToLower().TrimEnd('/');
			AbsoluteUrl = $"{_current.Host.ApplicationUrl}{RelativeUrl}";
			Params = _current.HttpContext.Request.QueryString.Value;
			AreaName = current.HttpContext.GetRouteValue("Area")?.ToString();
			RazorPage = current.HttpContext.GetRouteValue("Page")?.ToString();
			ControllerName = current.HttpContext.GetRouteValue("Controller")?.ToString();
			ActionName = current.HttpContext.GetRouteValue("Action")?.ToString();
			ActionUID = _getActionUID();
		}


		/* readonly properties */


		public string RelativeUrl { get; }
		public string AbsoluteUrl { get; }
		public string Params { get; }
		public string AreaName { get; }
		public string ControllerName { get; }
		public string ActionName { get; }
		public string RazorPage { get; }
		public string ActionUID { get; }


		/// <summary>
		/// Это стартовая страница сайта
		/// </summary>
		public bool IsStartSite { get; private set; }


		/// <summary>
		/// Это стартовая страница узла
		/// </summary>
		public bool IsStartNode { get; private set; }


		/// <summary>
		/// Это стартовая страница
		/// </summary>
		public bool IsStartPage { get; private set; }


		/// <summary>
		/// Имя узла
		/// </summary>
		public string NodeName { get; private set; }


		/// <summary>
		/// Имя страницы
		/// </summary>
		public string PageName { get; private set; }


		/// <summary>
		/// Ссылочный путь страницы
		/// </summary>
		public string PagePath { get; private set; }


		/// <summary>
		/// Ресурсный путь страницы
		/// </summary>
		public string PageResources { get; private set; }


		/// <summary>
		/// Полный путь запроса
		/// </summary>
		public string QueryPath { get; private set; }


		/// <summary>
		/// Путь представления
		/// </summary>
		public string ViewPath { get; private set; }


		/* functions */


		public string NodesParsePath(
			string path)
		{
			ViewEngineResult engine1;
			if (string.IsNullOrEmpty(path))
			{
				// стартовая страница сайта (узел по умолчанию (_main))				
				_initPaths(null, "_main/start");
			}
			else
			{
				var s1 = _fixPath(path);
				if (s1 != path) // path.EndsWith('/')
				{
					// исправление пути запроса
					return $"{_current.Host.VirtualPath}/{s1}{Params}";
				}

				//var path1 = path; // path.TrimEnd('/');
				engine1 = _current.ViewRender.GetViewEngineResult($"Nodes/{path}");
				if (engine1.View != null)
				{
					// обычная страница сайта, узла или каталога
					_initPaths(null, path);
				}
				else
				{
					var view1 = $"{path}/start";
					engine1 = _current.ViewRender.GetViewEngineResult($"Nodes/{view1}");
					if (engine1.View != null)
					{
						// стартовая страница узла или каталога
						_initPaths(path, view1);
					}
					else
					{
						var view2 = $"_main/{path}";
						engine1 = _current.ViewRender.GetViewEngineResult($"Nodes/{view2}");
						if (engine1.View != null)
						{
							// обычная страница узла по умолчанию (_main)
							_initPaths(path, view2);
						}
						else
						{
							var view3 = $"_main/{path}/start";
							engine1 = _current.ViewRender.GetViewEngineResult($"Nodes/{view3}");
							if (engine1.View != null)
							{
								// стартовая страница каталога узла по умолчанию (_main)
								_initPaths(path, view3);
							}
							else
								return null;
						}
					}
				}
			}
			_nodeRelease();
			_pageRelease();
			IsStartSite = string.IsNullOrEmpty(RelativeUrl);
			IsStartNode = RelativeUrl == _current.Node.Url;
			IsStartPage = ViewPath.EndsWith("/start");
			return "";
		}


		/* privates */


		private static string _fixPath(
			string path)
		{
			var s1 = path.Replace("//", "/").Trim('/');
			if (s1 == "start")
				return null;
			if (s1.EndsWith("/start"))
				return s1[..^5];
			return s1;
		}


		private string _getActionUID()
		{
			var sb1 = new StringBuilder();
			if (AreaName != null)
				sb1.Append($"/{AreaName}");
			if (RazorPage != null)
				sb1.Append($"{RazorPage}");
			if (ControllerName != null)
				sb1.Append($"/{ControllerName}");
			if (ActionName != null)
				sb1.Append($"/{ActionName}");
			return sb1.ToString().ToLower();
		}


		private void _initPaths(
			string queryPath,
			string viewPath)
		{
			ViewPath = $"/{viewPath}";

			var first1 = viewPath.IndexOf('/');
			var hasFirst1 = first1 > -1;
			var last2 = hasFirst1 ? viewPath.LastIndexOf('/') : -1;
			var hasLast2 = last2 > -1;
			NodeName = hasFirst1 ? viewPath[..first1] : viewPath;
			PageName = hasLast2 ? viewPath[(last2 + 1)..] : viewPath;
			PageResources = hasFirst1 ? viewPath[(first1 + 1)..] : null;
			PagePath = PageResources == "start" ? null : PageResources.GetTrimEnd("/start");

			var s1 = (queryPath ?? viewPath).ReplaceIfEqual("start", "");
			QueryPath = s1 == "_main/start"
				? _current.Host.VirtualPath
				: $"{_current.Host.VirtualPath}/{s1}";
		}


		private void _nodeRelease()
		{
			if (_current.Site.MapNodes == null)
				return;
			_current.Site.MapNodes.ClearActives();
			var node1 = _current.Site.MapNodes.GetItem(NodeName);
			if (node1 == null)
				return;
			node1.SetActive();
			_current.Node.NodeItem = node1;
			if (node1.HasMasters)
				foreach (var item1 in node1.Masters
					.Where(x => x.Type != MapItemTypeEnum.Group)
					.Reverse())
					_current.Node.InsertParent(item1.Link);
		}


		private void _pageRelease()
		{
			if (_current.Node.MapPages == null)
				return;
			_current.Node.MapPages.ClearActives();
			var page1 = _current.Node.MapPages.GetItem(QueryPath);
			if (page1 == null)
				return;
			page1.SetActive();
			_current.Page.PageItem = page1;
			if (page1.HasMasters)
				foreach (var item1 in page1.Masters
					.Reverse())
					_current.Page.InsertParent(item1.Link);
		}

	}

}


