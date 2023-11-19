using Ans.Net8.Common;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Ans.Net8.Web.Services
{

	/*
	 *	Add_AnsWeb
	 *		builder.Services.AddSingleton<IMapActionsProvider, MapActionsProvider_Xml>();
	 */



	public interface IMapActionsProvider
	{
		MapActions GetMapActions(string hostVirtualPath);
		void Reset();
	}



	public class MapActionsProvider_Xml
		: IMapActionsProvider
	{

		private readonly IWebHostEnvironment _env;
		private MapActions _map;
		private bool _missed;


		/* ctor */


		public MapActionsProvider_Xml(
			IWebHostEnvironment env)
		{
			_env = env;
		}


		/* functions */


		public MapActions GetMapActions(
			string hostVirtualPath)
		{
			if (_map != null)
				return _map;
			if (_missed)
				return null;
			var file1 = Path.Combine(
				_env.ContentRootPath, "_map-actions.xml");
			if (!File.Exists(file1))
			{
				_missed = true;
				Debug.WriteLine("[Ans.Net8.Web] MapActionsProvider_Xml.GetMap() : MISSED");
				return null;
			}
			try
			{
				var data1 = SuppXml.GetObjectFromXmlFile<MapActionsXmlRoot>(
					file1, "http://tempuri.org/Ans.Net8.Web.Actions.xsd");
				_map = new MapActions(_getBranch(data1.Elements), hostVirtualPath);
				Debug.WriteLine("[Ans.Net8.Web] MapActionsProvider_Xml.GetMap() : LOADED");
			}
			catch (Exception)
			{
				throw new Exception("[Ans.Net8.Web] MapActions compile error.");
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


		private IEnumerable<MapActionsItem> _getBranch(
			IEnumerable<MapActionsItemXmlElement> elements)
		{
			if (elements == null || !elements.Any())
				return null;
			var items1 = new List<MapActionsItem>();
			foreach (var element1 in elements)
			{
				var item1 = new MapActionsItem(
					element1.Target,
					element1.Face,
					element1.AccessPolicy,
					element1.AccessRoles,
					element1.IsHidden,
					element1.Tags);
				items1.Add(item1);
				item1.Slaves = _getBranch(element1.Elements);
			}
			return items1;
		}

	}



	/// <summary>
	/// http://tempuri.org/Ans.Net8.Web.Actions.xsd
	/// </summary>
	[XmlRoot("actions")]
	public class MapActionsXmlRoot
	{
		[XmlElement("item")]
		public List<MapActionsItemXmlElement> Elements { get; set; }
	}



	public class MapActionsItemXmlElement
	{
		[XmlElement("item")]
		public List<MapActionsItemXmlElement> Elements { get; set; }

		[XmlAttribute("target")]
		public string Target { get; set; }

		[XmlAttribute("face")]
		public string Face { get; set; }

		[XmlAttribute("policy")]
		public string AccessPolicy { get; set; }

		[XmlAttribute("roles")]
		public string AccessRoles { get; set; }

		[XmlAttribute("hidden")]
		public bool IsHidden { get; set; }

		[XmlAttribute("tags")]
		public string Tags { get; set; }
	}

}
