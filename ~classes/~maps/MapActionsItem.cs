namespace Ans.Net8.Web
{

	public class MapActionsItem
		: _TreeItem_Base<MapActionsItem>
	{

		/* ctor */


		public MapActionsItem(
			string target,
			string face,
			bool isHidden,
			string tags)
		{
			_parseFace(face);
			_parseTarget(target);
			IsHidden = isHidden;
			Tags = tags?.Split(',', ';');
			if (string.IsNullOrEmpty(target) && string.IsNullOrEmpty(face))
				IsHidden = true;
		}


		/* readonly properties */


		public LinkBuilder Link { get; private set; }
		public string Name { get; private set; }
		public string Target { get; private set; }
		public bool UseCatalogStartPage { get; private set; }

		public string Title { get; private set; }

		public string ShortTitle
			=> _shortTitle ?? Title;
		private string _shortTitle;

		public bool HasShortTitle
			=> _shortTitle != null;

		public bool IsHidden { get; }
		public string[] Tags { get; }


		/* properties */


		public string Path { get; set; }
		public bool IsSubActive { get; set; }


		/* methods */


		public void InitLink(
			string hostVirtualPath)
		{
			Link = new LinkBuilder { InnerHtml = ShortTitle };
			if (HasSlaves)
			{
				UseCatalogStartPage = Slaves?.Any(x => string.IsNullOrEmpty(x.Name)) ?? false;
				if (UseCatalogStartPage)
					Link.Href = $"{hostVirtualPath}{Path}";
				else
					Link.IsDisabled = true;
			}
			else
			{
				Link.Href = $"{hostVirtualPath}{Path}";
			}
		}


		public void MakeSupActive()
		{
			if (HasMasters)
				foreach (var item1 in Masters)
					item1.IsSubActive = true;
		}


		/* functions */


		public LinkBuilder GetNavLink()
		{
			return (HasSlaves)
				? new LinkBuilder
				{
					CssClass = Link.CssClass,
					Href = Link.Href,
					Id = Link.Id,
					InnerHtml = Link.InnerHtml,
					IsActive = Link.IsActive,
					IsExternal = Link.IsExternal,
					IsDisabled = false
				}
				: Link;
		}


		/* privates */


		private void _parseFace(
			string face)
		{
			if (!string.IsNullOrEmpty(face))
			{
				int i1 = face.IndexOf('|');
				if (i1 == -1)
					Title = face;
				else
				{
					_shortTitle = face[..i1];
					Title = string.Format(face[(i1 + 1)..], _shortTitle);
				}
			}
		}


		private void _parseTarget(
			string target)
		{
			if (Common._Consts.G_REGEX_NAME().IsMatch(target))
				Name = target;
			Title ??= target;
		}

	}

}
