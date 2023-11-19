namespace Ans.Net8.Web
{

	public interface ITreeItem<T>
	{
		IEnumerable<T> Masters { get; set; }
		IEnumerable<T> Slaves { get; set; }
		LinkBuilder Link { get; }
		string Target { get; }
		string Name { get; }
		string Title { get; }
		string ShortTitle { get; }
		string[] Tags { get; }
		MapItemTypeEnum Type { get; }
		bool IsHidden { get; }
		bool HasMasters { get; }
		bool HasSlaves { get; }
		bool HasShortTitle { get; }

		LinkBuilder GetNavLink();
	}



	public class _TreeItem_Base<T>
		: ITreeItem<T>
	{

		/* ctor */


		public _TreeItem_Base(
			string face,
			bool isHidden,
			string tags)
		{
			_parseFace(face);
			IsHidden = isHidden;
			Tags = tags?.Split(',', ';');
		}


		/* properties */


		public IEnumerable<T> Masters { get; set; }
		public IEnumerable<T> Slaves { get; set; }


		/* readonly properties */


		public string Target { get; internal set; }
		public MapItemTypeEnum Type { get; internal set; }
		public string Name { get; internal set; }
		public string Title { get; private set; }

		public string ShortTitle
			=> _shortTitle ?? Title;
		private string _shortTitle;

		public bool IsHidden { get; internal set; }

		public string[] Tags { get; private set; }

		public LinkBuilder Link { get; internal set; }

		public bool HasMasters
			=> Masters?.Any() ?? false;

		public bool HasSlaves
			=> Slaves?.Any() ?? false;

		public bool HasShortTitle
			=> _shortTitle != null;


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

	}



	public enum MapItemTypeEnum
	{
		Item,
		Catalog,
		InternalPath,
		ExternalLink
	}

}
