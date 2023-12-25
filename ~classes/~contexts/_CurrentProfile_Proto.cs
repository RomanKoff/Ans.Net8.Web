using Microsoft.AspNetCore.Html;

namespace Ans.Net8.Web
{

	public abstract class _CurrentProfile_Proto(
		ICurrentContext current)
		: _Current_Base(current)
	{

		private readonly List<LinkBuilder> _parents = [];

		internal string _containerCss;
		internal string _resUrl;


		/* abstracts properties */


		public abstract string ContainerCss { get; set; }
		public abstract string ResUrl { get; set; }


		/* virtuals properties */


		internal string _fullTitle;
		public virtual string FullTitle
		{
			get => _fullTitle ?? Title;
			set => _fullTitle = value;
		}


		/* properties */


		public string Title { get; set; }


		/* readonly properties */


		private HtmlString _titleHtml;
		public HtmlString TitleHtml
			=> _titleHtml ??= Title.ToHtml(true);

		private HtmlString _fullTitleHtml;
		public HtmlString FullTitleHtml
			=> _fullTitleHtml ??= FullTitle.ToHtml(true);

		public bool HasFullTitle
			=> _fullTitle != null;

		private IEnumerable<LinkBuilder> _breadcrumbs;
		public IEnumerable<LinkBuilder> Breadcrumbs
		{
			get
			{
				if (_breadcrumbs != null)
					return _breadcrumbs;
				var items1 = new List<LinkBuilder>();
				if (_parents?.Count > 0)
					items1.AddRange(_parents);
				var item1 = GetItemLink();
				if (item1 != null && !string.IsNullOrEmpty(item1.InnerHtml))
					items1.Add(item1);
				_breadcrumbs = items1.AsEnumerable();
				return _breadcrumbs;
			}
		}

		public bool HasBreadcrumbs
			=> Breadcrumbs.Any();


		/* abstracts functions */


		public abstract LinkBuilder GetItemLink();


		/* functions */


		public string GetResUrl(
			string filename,
			bool useAbsoluteUrl = false)
		{
			return $"{((useAbsoluteUrl) ? _current.Host.BaseUrl : null)}{ResUrl}/{filename}";
		}


		/* methods */


		public void AddParent(
			LinkBuilder resource)
		{
			_parents.Add(resource);
		}


		public void AddParent(
			string title)
		{
			AddParent(new LinkBuilder(title));
		}


		public void AddParent(
			string href,
			string title)
		{
			AddParent(new LinkBuilder(_current.FixUrl(href), title));
		}


		public void InsertParent(
			LinkBuilder resource,
			int index = 0)
		{
			_parents.Insert(index, resource);
		}


		public void InsertParent(
			string title,
			int index = 0)
		{
			InsertParent(new LinkBuilder(title), index);
		}


		public void InsertParent(
			string href,
			string title,
			int index = 0)
		{
			InsertParent(new LinkBuilder(_current.FixUrl(href), title), index);
		}


		public void RemoveParentLast()
		{
			if (_parents?.Count > 0)
				_parents.Remove(_parents.Last());
		}


		public void RemoveParent(
			int index)
		{
			if (_parents?.Count > 0)
				_parents.RemoveAt(index);
		}

	}

}
