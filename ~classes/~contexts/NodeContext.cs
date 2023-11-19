using Microsoft.AspNetCore.Html;

namespace Ans.Net8.Web
{

	public class NodeContext
		: _Context_Proto
	{

		/* ctor */


		public NodeContext(
			ICurrentContext current)
			: base(current)
		{
		}


		/* properties */


		// profiles


		public string Description { get; set; }
		public string Place { get; set; }


		public HtmlString PlaceHtml
			=> _placeHtml
				??= Place.ToHtml(true);
		private HtmlString _placeHtml;


		public DateTime? Date { get; set; }
		public DateTime? DateEnd { get; set; }


		public string Dates
		{
			get => _dates
				?? ((Date == null)
					? null : _current.DateTimeHelper.GetSpan(
						Date.Value, DateEnd, showCurrentYear: false));
			set => _dates = value;
		}
		private string _dates;


		public HtmlString DatesHtml
			=> _datesHtml
				??= Dates.ToHtml(true);
		private HtmlString _datesHtml;


		public string Logo { get; set; }
		public string LogoWide { get; set; } // XXL and more
		public string LogoMobile { get; set; } // SM and less ("" - hide)


		public string HeaderCss
		{
			get => _headerCss;
			set => _headerCss
				??= value;
		}
		private string _headerCss;


		public string HeaderStyle
		{
			get => _headerStyle;
			set => _headerStyle
				??= value;
		}
		private string _headerStyle;


		public string TitleCss
		{
			get => _titleCss;
			set => _titleCss
				??= value;
		}
		private string _titleCss;


		public string TitleStyle
		{
			get => _titleStyle;
			set => _titleStyle ??= value;
		}
		private string _titleStyle;


		public string[] Socials { get; set; } // [url|html]
		public string[] Emails { get; set; } // [email|text]
		public string[] Phones { get; set; } // [number|text]
		public string[] Address { get; set; } // [address|text]
		public string[] Buttons { get; set; } // [url|html|css]
		public string[] Langs { get; set; } // [lang=url]
		public string Addons { get; set; } // [html]

		public bool HasDescription => !string.IsNullOrEmpty(Description);
		public bool HasPlace => !string.IsNullOrEmpty(Place);
		public bool HasDates => Date != null;
		public bool HasDatesAndPlace => HasDates || HasPlace;
		public bool HasSocials => Socials?.Any() ?? false;
		public bool HasEmails => Emails?.Any() ?? false;
		public bool HasPhones => Phones?.Any() ?? false;
		public bool HasAddress => Address?.Any() ?? false;
		public bool HasButtons => Buttons?.Any() ?? false;
		public bool HasLangs => Langs?.Any() ?? false;
		public bool HasAddons => !string.IsNullOrEmpty(Addons);


		// end profiles


		public string Name { get; set; }
		public MapNodesItem NodeItem { get; set; }


		public string ResPath
		{
			get => _resPath
				?? _current.Request.NodeName;
			set => _resPath = value;
		}
		private string _resPath;


		public string ContainerCss
		{
			get => _containerCss
				?? _current.Site.ContainerCss;
			set => _containerCss = value;
		}
		private string _containerCss;


		public string DefaultMainCss
		{
			get => _mainCss
				?? _current.Site.DefaultMainCss;
			set => _mainCss = value;
		}
		private string _mainCss;


		public string DefaultMainStyle
		{
			get => _mainStyle
				?? _current.Site.DefaultMainStyle;
			set => _mainStyle = value;
		}
		private string _mainStyle;


		/* readonly properties */


		public MapPages MapPages
			=> _current.Sitemap.GetMapPages(Name);


		/* methods */


		public void InsertMasterNode(
			string title,
			string name)
		{
			InsertParent(new LinkBuilder
			{
				InnerHtml = title,
				Href = $"{_current.Host.VirtualPath}{name}"
			});
		}


		/* overrides */


		public override string ResUrl
		{
			get => _resUrl
				?? $"{_current.Site.ResUrl}/{ResPath}";
			set => _resUrl = value;
		}


		public override LinkBuilder GetItemLink()
		{
			return (NodeItem != null)
				? NodeItem.Link
				: (Title != null)
					? new LinkBuilder { InnerHtml = Title, IsDisabled = true }
					: null;
		}

	}

}
