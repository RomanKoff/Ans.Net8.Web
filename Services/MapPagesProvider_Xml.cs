using Ans.Net8.Common;
using Microsoft.AspNetCore.Hosting;
using System.Xml.Serialization;

namespace Ans.Net8.Web.Services
{

	/*
	 *	Add_AnsWeb
	 *		builder.Services.AddSingleton<IMapPagesProvider, MapPagesProvider_Xml>();
	 */



	public interface IMapPagesProvider
	{
		MapPages GetMap(string node, string hostVirtualPath);
		void Reset(string node);
	}



	public class MapPagesProvider_Xml(
		IWebHostEnvironment env)
		: _SingletonProvider_Proto<MapPages>,
		IMapPagesProvider
	{

		private readonly IWebHostEnvironment _env = env;
		private string _hostVirtualPath;


		public override bool TestMissed(
			string key)
		{
			return !File.Exists(_getFilename(key));
		}


		public override MapPages GetItem(
			string key)
		{
			var data1 = SuppXml.GetObjectFromXmlFile<MapPagesXmlRoot>(
				_getFilename(key), "http://tempuri.org/Ans.Net8.Web.Pages.xsd");
			return new MapPages(_getBranch(data1.Elements), _fixMainNode(key), _hostVirtualPath);
		}


		/* functions */


		public MapPages GetMap(
			string node,
			string hostVirtualPath)
		{
			_hostVirtualPath = hostVirtualPath;
			return base.Get(node);
		}


		/* methods */


		public void Reset(
			string node)
		{
			base.Remove(node);
		}


		/* privates */


		private static string _fixMainNode(
			string node)
			=> node == "_main" ? "" : node;


		private string _getFilename(
			string node)
		{
			var path1 = Path.Combine(_env.ContentRootPath, "Views/Nodes");
			return node != null
				? $"{path1}/{node}/_map-pages.xml"
				: $"{path1}/_map-pages.xml";
		}


		private static IEnumerable<MapPagesItem> _getBranch(
			IEnumerable<MapPagesItemXmlElement> elements)
		{
			if (!(elements?.Count() > 0))
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
			return items1.AsEnumerable();
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
