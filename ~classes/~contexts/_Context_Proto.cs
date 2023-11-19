using Microsoft.AspNetCore.Html;

namespace Ans.Net8.Web
{

	public abstract class _Context_Proto
		: _ContextModule_Base
	{

		private List<LinkBuilder> _parents;
		internal string _resUrl;


		/* ctor */


		public _Context_Proto(
			ICurrentContext current)
			: base(current)
		{
		}


		/* properties */


		public string Title { get; set; }


		public virtual string FullTitle
		{
			get => _fullTitle
				?? Title;
			set => _fullTitle = value;
		}
		internal string _fullTitle;


		public abstract string ResUrl { get; set; }


		/* readonly properties */


		public IEnumerable<LinkBuilder> Breadcrumbs
		{
			get
			{
				if (_breadcrumbs != null)
					return _breadcrumbs;
				var items1 = new List<LinkBuilder>();
				if (_parents?.Any() ?? false)
					items1.AddRange(_parents);
				var item1 = GetItemLink();
				if (item1 != null && !string.IsNullOrEmpty(item1.InnerHtml))
					items1.Add(item1);
				_breadcrumbs = items1;
				return _breadcrumbs;
			}
		}
		private IEnumerable<LinkBuilder> _breadcrumbs;


		public HtmlString TitleHtml
			=> _titleHtml
				??= Title.ToHtml(true);
		private HtmlString _titleHtml;


		public HtmlString FullTitleHtml
			=> _fullTitleHtml
				??= FullTitle.ToHtml(true);
		private HtmlString _fullTitleHtml;


		public bool HasBreadcrumbs
			=> Breadcrumbs.Any();


		/* methods */


		public void AddParent(
			LinkBuilder resource)
		{
			_parents
				??= new List<LinkBuilder>();
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
			var href1 = _current.GetLink(href);
			AddParent(new LinkBuilder(href1, title));
		}


		public void InsertParent(
			LinkBuilder resource,
			int index = 0)
		{
			_parents
				??= new List<LinkBuilder>();
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
			InsertParent(new LinkBuilder(href, title), index);
		}


		public void RemoveParentLast()
		{
			if (_parents?.Any() ?? false)
				_parents.Remove(_parents.Last());
		}


		public void RemoveParent(
			int index)
		{
			if (_parents?.Any() ?? false)
				_parents.RemoveAt(index);
		}


		/* functions */


		public string GetResUrl(
			string filename,
			bool useAbsoluteUrl = false)
		{
			return $"{((useAbsoluteUrl) ? _current.Host.BaseUrl : null)}{ResUrl}/{filename}";
		}


		public abstract LinkBuilder GetItemLink();

	}

}
