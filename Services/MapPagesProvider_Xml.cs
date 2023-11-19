using Ans.Net8.Common;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Ans.Net8.Web.Services
{

	/*
	 *	Add_AnsWeb
	 *		builder.Services.AddSingleton<IMapPagesProvider, MapPagesProvider_Xml>();
	 */



	public interface IMapPagesProvider
	{
		MapPages GetMapPages(string node, string hostVirtualPath);
		void Reset(string node);
	}



	public class MapPagesProvider_Xml
		: IMapPagesProvider
	{

		private readonly IWebHostEnvironment _env;
		private string _hostVirtualPath;
		private readonly Dictionary<string, MapPages> _maps = new();


		/* ctor */


		public MapPagesProvider_Xml(
			IWebHostEnvironment env)
		{
			_env = env;
		}


		/* functions */


		public MapPages GetMapPages(
			string node,
			string hostVirtualPath)
		{
			var node1 = string.IsNullOrEmpty(node) ? "_main" : node;
			if (_maps.TryGetValue(node1, out MapPages map1))
				return map1;
			_hostVirtualPath = hostVirtualPath;
			var file1 = Path.Combine(
				_env.ContentRootPath, "Views/Nodes", node1, "_map-pages.xml");
			if (!File.Exists(file1))
			{
				_maps.Add(node1, null);
				Debug.WriteLine($"[Ans.Net8.Web] MapPagesProvider_Xml.GetMap(\"{node1}\") : MISSED");
				return null;
			}
			MapPages _map1;
			try
			{
				var data1 = SuppXml.GetObjectFromXmlFile<MapPagesXmlRoot>(
					file1, "http://tempuri.org/Ans.Net8.Web.Pages.xsd");
				_map1 = new MapPages(_getBranch(data1.Elements), node, _hostVirtualPath);
				Debug.WriteLine($"[Ans.Net8.Web] MapPagesProvider_Xml.GetMap(\"{node1}\") : LOADED");
			}
			catch (Exception)
			{
				throw new Exception("[Ans.Net8.Web] MapPages compile error.");
			}
			_maps.Add(node1, _map1);
			return _map1;
		}


		/* properties */


		public void Reset(
			string node)
		{
			var node1 = string.IsNullOrEmpty(node) ? "_main" : node;
			_ = _maps.Remove(node1);
		}


		/* privates */


		private IEnumerable<MapPagesItem> _getBranch(
			IEnumerable<MapPagesItemXmlElement> elements)
		{
			if (elements == null || !elements.Any())
				return null;
			var items1 = new List<MapPagesItem>();
			foreach (var element1 in elements)
			{
				var item1 = new MapPagesItem(
					element1.Target,
					element1.Face,
					element1.IsHidden,
					element1.Tags);
				items1.Add(item1);
				item1.Slaves = _getBranch(element1.Elements);
			}
			return items1;
		}

	}



	/// <summary>
	/// http://tempuri.org/Ans.Net8.Web.Pages.xsd
	/// </summary>
	[XmlRoot("pages")]
	public class MapPagesXmlRoot
	{
		[XmlElement("item")]
		public List<MapPagesItemXmlElement> Elements { get; set; }
	}



	public class MapPagesItemXmlElement
	{
		[XmlElement("item")]
		public List<MapPagesItemXmlElement> Elements { get; set; }

		[XmlAttribute("target")]
		public string Target { get; set; }

		[XmlAttribute("face")]
		public string Face { get; set; }

		[XmlAttribute("hidden")]
		public bool IsHidden { get; set; }

		[XmlAttribute("tags")]
		public string Tags { get; set; }
	}

}
