using Ans.Net8.Common;
using Ans.Net8.Web.Services;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Routing;
using System.Diagnostics;
using System.Text;

namespace Ans.Net8.Web
{

	public class RequestData
		: _Current_Base
	{

		private readonly IViewRenderService _viewRender;


		/* ctor */


		public RequestData(
			ICurrentContext current,
			IViewRenderService viewRender)
			: base(current)
		{
			_viewRender = viewRender;
			Path = current.HttpContext.Request.Path.ToString().ToLower();
			Url = $"{current.Host.ApplicationUrl[..^1]}{Path}";
			AreaName = current.HttpContext.GetRouteValue("Area")?.ToString();
			RazorPage = current.HttpContext.GetRouteValue("Page")?.ToString();
			ControllerName = current.HttpContext.GetRouteValue("Controller")?.ToString();
			ActionName = current.HttpContext.GetRouteValue("Action")?.ToString();
			Action = _getActionPath(AreaName, RazorPage, ControllerName, ActionName);
		}


		/* readonly properties */


		public string Path { get; private set; }
		public string Url { get; private set; }
		public string Action { get; private set; }
		public string AreaName { get; private set; }
		public string ControllerName { get; private set; }
		public string ActionName { get; private set; }
		public string RazorPage { get; private set; }

		public string NodeName { get; private set; }
		public string PageName { get; private set; }
		public string PageResPath { get; private set; }
		public string PagePath { get; private set; }
		public bool PageIsStart { get; private set; }

		public string QueryPath { get; private set; }
		public string ViewPath { get; private set; }


		/* methods */


		public bool ParserPage(
			string path)
		{
			ViewEngineResult engine1;
			if (string.IsNullOrEmpty(path))
			{
				// site start page
				_initPaths(null, "_main/start");
			}
			else
			{
				string path1 = path.TrimEnd('/');
				engine1 = _viewRender.GetViewEngineResult($"Nodes/{path1}");
				if (engine1.View != null)
				{
					// path page
					_initPaths(null, path1);
				}
				else
				{
					string view1 = $"{path1}/start";
					engine1 = _viewRender.GetViewEngineResult($"Nodes/{view1}");
					if (engine1.View != null)
					{
						// path start page
						_initPaths(path1, view1);
					}
					else
					{
						string view2 = $"_main/{path1}";
						engine1 = _viewRender.GetViewEngineResult($"Nodes/{view2}");
						if (engine1.View != null)
						{
							// main path page
							_initPaths(path1, view2);
						}
						else
						{
							string view3 = $"_main/{path1}/start";
							engine1 = _viewRender.GetViewEngineResult($"Nodes/{view3}");
							if (engine1.View != null)
							{
								// main path start page
								_initPaths(path1, view3);
							}
							else
								return false;
						}
					}
				}
			}
			_nodeRelease();
			_pageRelease();
			return true;
		}


		/* privates */


		private void _initPaths(
			string queryPath,
			string viewPath)
		{
			ViewPath = $"/{viewPath}";
			var a1 = viewPath.Split('/');
			NodeName = a1.First();
			PageName = a1.Last();
			PageResPath = a1.Skip(1).MakeFromCollection(null, null, "/");
			PageIsStart = ViewPath.EndsWith("/start");
			PagePath = PageResPath == "start"
				? null
				: PageResPath.TrimEnd("/start");
			string s1 = (queryPath ?? viewPath).ReplaceIfEqual("start", "");
			QueryPath = (s1 == "_main/start")
				? _current.Host.VirtualPath
				: $"{_current.Host.VirtualPath}{s1}".TrimEnd('/');
		}


		private void _nodeRelease()
		{
			if (_current.Site.MapNodes == null)
				return;
			int i1 = ViewPath.IndexOf('/', 1);
			var node1 = _current.Site.MapNodes.GetNode(NodeName);
			if (node1 != null)
			{
				Debug.WriteLine($"[Ans.Net8.Web] NodeRelease()");
				_current.Node.NodeItem = node1;
				_current.Node.Name = node1.Name;
				if (node1.HasShortTitle)
				{
					_current.Node.FullTitle = node1.Title;
					_current.Node.Title = node1.ShortTitle;
				}
				else
					_current.Node.Title = node1.Title;
				if (node1.HasMasters)
					foreach (var item1 in node1.Masters
						.Where(x => x.Type != MapNodesItemType.Group)
						.Reverse())
						_current.Node.InsertParent(item1.Link);
			}
		}


		private void _pageRelease()
		{
			if (_current.Node.MapPages == null)
				return;
			var page1 = _current.Node.MapPages.GetPage(PagePath);
			if (page1 != null)
			{
				Debug.WriteLine($"[Ans.Net8.Web] PageRelease()");
				_current.Page.PageItem = page1;
				if (page1.HasShortTitle)
				{
					_current.Page.FullTitle = page1.Title;
					_current.Page.Title = page1.ShortTitle;
				}
				else
					_current.Page.Title = page1.Title;
				if (page1.HasMasters)
					foreach (var item1 in page1.Masters
						.Where(x => x.Type == MapPagesItemType.Catalog
							|| x.Type == MapPagesItemType.Page)
						.Reverse())
						_current.Page.InsertParent(item1.Link);
			}
		}


		private static string _getActionPath(
			string area,
			string razor,
			string controller,
			string action)
		{
			var sb1 = new StringBuilder();
			if (area != null)
				sb1.Append($"/{area}");
			if (razor != null)
				sb1.Append($"{razor}");
			if (controller != null)
				sb1.Append($"/{controller}");
			if (action != null)
				sb1.Append($"/{action}");
			return sb1.ToString().ToLower();
		}

	}

}
