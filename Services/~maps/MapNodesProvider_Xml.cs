using Ans.Net8.Common;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Ans.Net8.Web.Services
{

	public interface IMapNodesProvider
	{
		MapNodes GetMapNodes(string virtualPath);
		void Refresh();
	}



	public class MapNodesProvider_Xml(
		IWebHostEnvironment env)
		: IMapNodesProvider
	{

		private readonly IWebHostEnvironment _env = env;
		private MapNodes _map;
		private bool _missed;


		/* methods */


		public void Refresh()
		{
			_map = null;
			_missed = false;
		}


		/* functions */


		public MapNodes GetMapNodes(
			string virtualPath)
		{
			if (_map != null)
				return _map;

			if (_missed)
				return null;

			var file1 = Path.Combine(
				_env.ContentRootPath, "Views/Nodes", "_map-nodes.xml");

			if (!File.Exists(file1))
			{
				_missed = true;
				Debug.WriteLine($"[Ans.Net8.Web] MapNodesProvider_Xml.GetMapNodes() : MISSED");
				return null;
			}
			try
			{
				var data1 = SuppXml.GetObjectFromXmlFile<MapNodesXmlRoot>(
					file1, Common._Consts.ENCODING_UTF8,
					"http://tempuri.org/Ans.Net8.Web.MapNodes.xsd");
				_map = new MapNodes(_getBranch(data1.Items), virtualPath);
				Debug.WriteLine($"[Ans.Net8.Web] MapNodesProvider_Xml.GetMapNodes() : LOADED");
			}
			catch (Exception)
			{
				throw new Exception("[Ans.Net8.Web] MapNodes compile error.");
			}
			return _map;
		}


		/* privates */


		private static IEnumerable<MapNodesItem> _getBranch(
			IEnumerable<MapNodeXmlElement> elements)
		{
			if (!(elements?.Count() > 0))
				return null;
			var items1 = new List<MapNodesItem>();
			foreach (var element1 in elements)
			{
				var item1 = element1.GetNode();
				item1.Slaves = _getBranch(element1.Items);
				items1.Add(item1);
			}
			return items1.AsEnumerable();
		}

	}



	/// <summary>
	/// http://tempuri.org/Ans.Net8.Web.MapNodes.xsd
	/// </summary>
	[XmlRoot("nodes")]
	public class MapNodesXmlRoot
	{
		[XmlElement("item")]
		public List<MapNodeXmlElement> Items { get; set; }
	}



	public class MapNodeXmlElement
	{

		/* properties */


		[XmlElement("item")]
		public List<MapNodeXmlElement> Items { get; set; }


		/// <summary>
		/// null -> Group
		/// || ("" || G_REGEX_NAME) -> Node
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


		[XmlAttribute("hidden")]
		public bool IsHidden { get; set; }


		[XmlAttribute("tags")]
		public string Tags { get; set; }


		[XmlAttribute("props")]
		public string Properties { get; set; }


		[XmlAttribute("rem")]
		public string Remark { get; set; }


		/* functions */


		public MapNodesItem GetNode()
		{
			var item1 = new MapNodesItem(
				Target, Face,
				IsHidden, Tags, Properties, Remark);
			return item1;
		}

	}

}