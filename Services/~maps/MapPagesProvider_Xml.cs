using Ans.Net8.Common;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Ans.Net8.Web.Services
{

	public interface IMapPagesProvider
	{
		MapPages GetMapPages(string node, string virtualPath);
		void Refresh(string node);
	}



	public class MapPagesProvider_Xml(
		IWebHostEnvironment env)
		: IMapPagesProvider
	{

		private readonly IWebHostEnvironment _env = env;
		private readonly Dictionary<string, MapPages> _maps = [];


		/* functions */


		public MapPages GetMapPages(
			string node,
			string virtualPath)
		{
			var node1 = _getNodeName(node);

			if (_maps.TryGetValue(node1, out MapPages map1))
				return map1;

			var file1 = Path.Combine(
				_env.ContentRootPath, "Views/Nodes", node1, "_map-pages.xml");

			if (!File.Exists(file1))
			{
				_maps.Add(node1, null);
				Debug.WriteLine($"[Ans.Net8.Web] MapPagesProvider_Xml.GetMapPages(\"{node1}\") : MISSED");
				return null;
			}

			MapPages map2;
			try
			{
				var data1 = SuppXml.GetObjectFromXmlFile<MapPagesXmlRoot>(
					file1, Common._Consts.ENCODING_UTF8,
					"http://tempuri.org/Ans.Net8.Web.MapPages.xsd");
				map2 = new MapPages(_getBranch(data1.Items), virtualPath, node);
				Debug.WriteLine($"[Ans.Net8.Web] MapPagesProvider_Xml.GetMapPages(\"{node1}\") : LOADED");
			}
			catch (Exception)
			{
				throw new Exception("[Ans.Net8.Web] MapPages compile error.");
			}
			_maps.Add(node1, map2);
			return map2;
		}


		/* methods */


		public void Refresh(
			string node)
		{
			var node1 = _getNodeName(node);
			_ = _maps.Remove(node1);
		}


		/* privates */


		private static string _getNodeName(
			string name)
		{
			//return name;
			return string.IsNullOrEmpty(name)
				? "_main" : name;
		}


		private static IEnumerable<MapPagesItem> _getBranch(
			IEnumerable<MapPageXmlElement> elements)
		{
			if (!(elements?.Count() > 0))
				return null;
			var items1 = new List<MapPagesItem>();
			foreach (var element1 in elements)
			{
				var item1 = element1.GetPage();
				item1.Slaves = _getBranch(element1.Items);
				if (item1.HasSlaves)
				{
					item1.Type = MapItemTypeEnum.Group;
					item1.Link.IsDisabled = !item1.HasStart;
				}
				items1.Add(item1);
			}
			return items1.AsEnumerable();
		}

	}



	/// <summary>
	/// http://tempuri.org/Ans.Net8.Web.MapPages.xsd
	/// </summary>
	[XmlRoot("pages")]
	public class MapPagesXmlRoot
	{
		[XmlElement("item")]
		public List<MapPageXmlElement> Items { get; set; }
	}



	public class MapPageXmlElement
	{

		/* properties */


		[XmlElement("item")]
		public List<MapPageXmlElement> Items { get; set; }


		/// <summary>
		/// null -> Catalog
		/// || ("" || G_REGEX_NAME) -> Page
		/// || "/*" -> InternalPath
		/// || ExternalUrl
		/// </summary>
		[XmlAttribute("target")]
		public string Target { get; set; }


		/// <summary>
		/// "full_title"
		/// || "short_title|full_title_template"
		/// </summary>
		[XmlAttribute("face")]
		public string Face { get; set; }


		[XmlAttribute("start")]
		public bool HasStart { get; set; }


		[XmlAttribute("hidden")]
		public bool IsHidden { get; set; }


		[XmlAttribute("tags")]
		public string Tags { get; set; }


		[XmlAttribute("props")]
		public string Properties { get; set; }


		[XmlAttribute("rem")]
		public string Remark { get; set; }


		/* functions */


		public MapPagesItem GetPage()
		{
			var item1 = new MapPagesItem(
				Target, Face,
				HasStart, IsHidden, Tags, Properties, Remark);
			return item1;
		}

	}

}