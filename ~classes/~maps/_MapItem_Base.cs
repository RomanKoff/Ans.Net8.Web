using Microsoft.AspNetCore.Html;

namespace Ans.Net8.Web
{

	public enum MapItemTypeEnum
	{
		Item,
		Group,
		InternalPath,
		ExternalUrl
	}



	public interface IMapItem
	{
		IEnumerable<IMapItem> Masters { get; set; }
		IEnumerable<IMapItem> Slaves { get; set; }
		MapItemTypeEnum Type { get; set; }
		IDictionary<string, string> Properties { get; set; }
		LinkBuilder Link { get; set; }
		string Target { get; set; }
		string Remark { get; set; }
		string[] Tags { get; set; }
		bool IsHidden { get; set; }

		string Title { get; }
		string ShortTitle { get; }
		HtmlString TitleHtml { get; }
		HtmlString ShortTitleHtml { get; }
		bool HasMasters { get; }
		bool HasSlaves { get; }
		bool IsShortTitleUnique { get; }

		void SetActive();
		void ClearActive();
	}



	public class _MapItem_Base
		: IMapItem
	{

		/* properties */


		public IEnumerable<IMapItem> Masters { get; set; }
		public IEnumerable<IMapItem> Slaves { get; set; }
		public MapItemTypeEnum Type { get; set; }
		public IDictionary<string, string> Properties { get; set; }
		public LinkBuilder Link { get; set; }

		public string Id { get; set; }
		public string Target { get; set; }
		public string Remark { get; set; }
		public string[] Tags { get; set; }
		public bool IsHidden { get; set; }


		/* readonly properties */


		private string _title;
		public string Title
			=> _title;


		private string _shortTitle;
		public string ShortTitle
			=> _shortTitle ?? Title;


		private HtmlString _titleHtml;
		public HtmlString TitleHtml
			=> _titleHtml ??= Title.ToHtml(true);


		private HtmlString _shortTitleHtml;
		public HtmlString ShortTitleHtml
			=> _shortTitleHtml ??= IsShortTitleUnique
				? ShortTitle.ToHtml(true)
				: TitleHtml;


		public bool HasMasters
			=> Masters?.Count() > 0;


		public bool HasSlaves
			=> Slaves?.Count() > 0;


		public bool IsShortTitleUnique
			=> _shortTitle != null && _shortTitle != _title;


		/* methods */


		public void SetFace(
			string face)
		{
			_title = null;
			_shortTitle = null;
			_titleHtml = null;
			_shortTitleHtml = null;
			if (string.IsNullOrEmpty(face))
				return;
			var i1 = face.IndexOf('|');
			if (i1 >= 0)
			{
				_shortTitle = face[..i1];
				_title = string.Format(face[(i1 + 1)..], _shortTitle);
			}
			else
				_title = face;
		}


		public void SetActive()
		{
			Link.IsActive = true;
			if (HasMasters)
				foreach (var item1 in Masters)
					item1.Link.IsSubActive = true;
		}


		public void ClearActive()
		{
			Link.IsActive = false;
			Link.IsSubActive = false;
		}

	}

}
