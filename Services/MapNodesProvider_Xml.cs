using Ans.Net8.Common;
using Microsoft.AspNetCore.Hosting;
using System.Xml.Serialization;

namespace Ans.Net8.Web.Services
{

	/*
	 *	Add_AnsWeb
	 *		builder.Services.AddSingleton<IMapNodesProvider, MapNodesProvider_Xml>();
	 */



	public interface IMapNodesProvider
	{
		MapNodes GetMap(string hostVirtualPath);
		void Reset();
	}



	public class MapNodesProvider_Xml(
		IWebHostEnvironment env)
		: _SingletonProvider_Proto<MapNodes>,
		IMapNodesProvider
	{

		private readonly IWebHostEnvironment _env = env;
		private string _hostVirtualPath;


		public override bool TestMissed(
			string key)
		{
			return !File.Exists(_getFilename());
		}


		public override MapNodes GetItem(
			string key)
		{
			var data1 = SuppXml.GetObjectFromXmlFile<MapNodesXmlRoot>(
				_getFilename(), "http://tempuri.org/Ans.Net8.Web.Nodes.xsd");
			return new MapNodes(_getBranch(data1.Elements), _hostVirtualPath);
		}



		/* functions */


		public MapNodes GetMap(
			string hostVirtualPath)
		{
			_hostVirtualPath = hostVirtualPath;
			return base.Get("");
		}


		/* methods */


		public void Reset()
		{
			base.Remove("");
		}


		/* privates */


		private string _getFilename()
			=> Path.Combine(_env.ContentRootPath, "Views/Nodes", "_map-nodes.xml");


		private static IEnumerable<MapNodesItem> _getBranch(
			IEnumerable<MapNodesItemXmlElement> elements)
		{
			if (!(elements?.Count() > 0))
				return null;
			var items1 = new List<MapNodesItem>();
			foreach (var element1 in elements)
			{
				var item1 = new MapNodesItem(
					element1.Id,
					element1.Target,
					element1.Face,
					element1.IsHidden,
					element1.Tags);
				items1.Add(item1);
				item1.Slaves = _getBranch(element1.Elements);
			}
			return items1.AsEnumerable();
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

		[XmlAttribute("id")]
		public string Id { get; set; }

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
