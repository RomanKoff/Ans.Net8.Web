using Ans.Net8.Common;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Ans.Net8.Web.Services
{

	/*
	 *	Add_AnsWeb
	 *		builder.Services.AddSingleton<IMapNodesProvider, MapNodesProvider_Xml>();
	 */



	public interface IMapNodesProvider
	{
		MapNodes GetMapNodes(string hostVirtualPath);
		void Reset();
	}



	public class MapNodesProvider_Xml
		: IMapNodesProvider
	{

		private readonly IWebHostEnvironment _env;
		private MapNodes _map;
		private bool _missed;


		/* ctor */


		public MapNodesProvider_Xml(
			IWebHostEnvironment env)
		{
			_env = env;
		}


		/* functions */


		public MapNodes GetMapNodes(
			string hostVirtualPath)
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
				Debug.WriteLine("[Ans.Net8.Web] MapNodesProvider_Xml.GetMap() : MISSED");
				return null;
			}
			try
			{
				var data1 = SuppXml.GetObjectFromXmlFile<MapNodesXmlRoot>(
					file1, "http://tempuri.org/Ans.Net8.Web.Nodes.xsd");
				_map = new MapNodes(_getBranch(data1.Elements), hostVirtualPath);
				Debug.WriteLine("[Ans.Net8.Web] MapNodesProvider_Xml.GetMap() : LOADED");
			}
			catch (Exception)
			{
				throw new Exception("[Ans.Net8.Web] MapNodes compile error.");
			}
			return _map;
		}


		/* methods */


		public void Reset()
		{
			_map = null;
			_missed = false;
		}


		/* privates */


		private IEnumerable<MapNodesItem> _getBranch(
			IEnumerable<MapNodesItemXmlElement> elements)
		{
			if (elements == null || !elements.Any())
				return null;
			var items1 = new List<MapNodesItem>();
			foreach (var element1 in elements)
			{
				var item1 = new MapNodesItem(
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
	/// http://tempuri.org/Ans.Net8.Web.Nodes.xsd
	/// </summary>
	[XmlRoot("nodes")]
	public class MapNodesXmlRoot
	{
		[XmlElement("item")]
		public List<MapNodesItemXmlElement> Elements { get; set; }
	}



	public class MapNodesItemXmlElement
	{
		[XmlElement("item")]
		public List<MapNodesItemXmlElement> Elements { get; set; }

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
