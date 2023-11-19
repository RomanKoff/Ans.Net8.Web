namespace Ans.Net8.Web
{

	public class MapPagesItem
		: _TreeItem_Base<MapPagesItem>
	{

		/* ctor */


		public MapPagesItem(
			string target,
			string face,
			bool isHidden,
			string tags)
			: base(face ?? target, isHidden, tags)
		{
			if (string.IsNullOrEmpty(target) && string.IsNullOrEmpty(face))
				IsHidden = true;
			_parseTarget(target);
		}


		/* properties */


		public bool IsSubActive { get; set; }


		/* readonly properties */


		public bool UseCatalogStartPage { get; private set; }


		public string Path
		{
			get
			{
				if (_path != null)
					return _path;
				string master1 = $"{Masters?.Last().Path}";
				var f1 = !string.IsNullOrEmpty(master1);
				var f2 = !string.IsNullOrEmpty(Name);
				_path = f1 && f2
					? $"{master1}/{Name}" : f1
						? master1 : Name;
				return _path;
			}
		}
		private string _path;


		/* methods */


		public void InitLink(
			string hostNodePath,
			string hostVirtualPath)
		{
			Link = new LinkBuilder { InnerHtml = ShortTitle };
			if (HasSlaves)
				Type = MapItemTypeEnum.Catalog;
			switch (Type)
			{
				case MapItemTypeEnum.Catalog:
					UseCatalogStartPage = Slaves?.Any(x => string.IsNullOrEmpty(x.Name)) ?? false;
					if (UseCatalogStartPage)
						Link.Href = $"{hostNodePath}{Path}";
					else
						Link.IsDisabled = true;
					break;
				case MapItemTypeEnum.Item:
					Link.Href = string.IsNullOrEmpty(Path) && hostNodePath != hostVirtualPath
						? hostNodePath[..^1] : $"{hostNodePath}{Path}";
					break;
				case MapItemTypeEnum.InternalPath:
					Link.Href = $"{hostVirtualPath}{Target}";
					break;
				default:
					Link.Href = Target;
					Link.IsExternal = true;
					break;
			}
		}


		public void MakeSupActive()
		{
			if (HasMasters)
				foreach (var item1 in Masters)
					item1.IsSubActive = true;
		}


		/* privates */


		private void _parseTarget(
			string target)
		{
			if (string.IsNullOrEmpty(target))
			{
				Type = MapItemTypeEnum.Item;
			}
			else if (target[0] == '/')
			{
				Type = MapItemTypeEnum.InternalPath;
				Name = "#InternalPath#";
				Target = target[1..];
			}
			else if (Common._Consts.G_REGEX_NAME().IsMatch(target))
			{
				Type = MapItemTypeEnum.Item; // предположение
				Name = target;
			}
			else
			{
				Type = MapItemTypeEnum.ExternalLink;
				Name = "#ExternalLink#";
				Target = target;
			}
		}

	}

}
