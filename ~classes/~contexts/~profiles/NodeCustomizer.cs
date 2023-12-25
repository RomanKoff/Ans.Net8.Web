using Ans.Net8.Common;

namespace Ans.Net8.Web
{

	public class NodeCustomizer
	{

		/* properties */


		/// <summary>
		/// описание (слоган)
		/// </summary>
		public string Abstract { get; set; }


		/// <summary>
		/// место (страна, город)
		/// </summary>
		public string Place { get; set; }


		/// <summary>
		/// дата проведения (дата начала)
		/// </summary>
		public DateOnly? Date { get; set; }


		/// <summary>
		/// дата завершения
		/// </summary>
		public DateOnly? DateEnd { get; set; }


		/// <summary>
		/// логотип
		/// </summary>
		public string LogoUrl { get; set; }


		/// <summary>
		/// логотип для узкого окна
		/// </summary>
		public string LogoMobileUrl { get; set; }


		/// <summary>
		/// логотип для широкого окна
		/// </summary>
		public string LogoWideUrl { get; set; }


		/// <summary>
		/// социальные медиа
		/// </summary>
		public IEnumerable<LinkBuilder> Socials { get; set; }


		/// <summary>
		/// эл. почта
		/// </summary>
		public string Email { get; set; }


		/// <summary>
		/// телефон
		/// </summary>
		public string Phone { get; set; }


		/// <summary>
		/// адрес
		/// </summary>
		public string Address { get; set; }


		/// <summary>
		/// кнопки
		/// </summary>
		public IEnumerable<LinkBuilder> Buttons { get; set; }


		/// <summary>
		/// версии на других языках
		/// </summary>
		public IEnumerable<LinkBuilder> Langs { get; set; }


		public string HeaderCss { get; set; }
		public string HeaderStyle { get; set; }
		public string TitleCss { get; set; }
		public string TitleStyle { get; set; }


		/* readonly properties */


		public bool HasAbstract => string.IsNullOrEmpty(Abstract);
		public bool HasPlace => string.IsNullOrEmpty(Place);
		public bool HasDate => Date != null;
		public bool HasLogoUrl => string.IsNullOrEmpty(LogoUrl);
		public bool HasLogoMobileUrl => string.IsNullOrEmpty(LogoMobileUrl);
		public bool HasLogoWideUrl => string.IsNullOrEmpty(LogoWideUrl);
		public bool HasSocials => Socials?.Count() > 0;
		public bool HasEmail => string.IsNullOrEmpty(Email);
		public bool HasPhone => string.IsNullOrEmpty(Phone);
		public bool HasAddress => string.IsNullOrEmpty(Address);
		public bool HasButtons => Buttons?.Count() > 0;
		public bool HasLangs => Langs?.Count() > 0;


		/* methods */


		public void SetDates(
			DateOnly date,
			DateOnly? dateEnd)
		{
			Date = date;
			DateEnd = dateEnd;
		}


		/// <param name="dates">
		/// "yyyy-mm-dd"
		/// "yyyy-mm-dd|yyyy-mm-dd"
		/// </param>
		public void SetDates(
			string dates)
		{
			var f1 = dates.Length == 10;
			var f2 = dates.Length == 21;
			if (!(f1 || f2))
				return;
			var d1 = new DateOnly(
				dates[0..3].ToInt(0), dates[5..6].ToInt(0), dates[8..9].ToInt(0));
			if (f2)
				SetDates(d1, new DateOnly(
					dates[11..14].ToInt(0), dates[16..17].ToInt(0), dates[19..20].ToInt(0)));
			else
				SetDates(d1, null);
		}


		/// <param name="items">
		/// "url|title";...
		/// </param>
		public void SetSocials(
			string items)
		{
			var items1 = new List<LinkBuilder>();
			foreach (var item1 in items.Split([';', ',']))
			{
				var a1 = new StringParser(item1);
				items1.Add(new LinkBuilder
				{
					Href = a1.Get(0),
					InnerHtml = a1.Get(1)
				});
			}
			if (items1.Count > 0)
				Socials = items1.AsEnumerable();
		}


		/// <param name="items">
		/// "url|html|css";...
		/// </param>
		public void SetButtons(
			string items)
		{
			var items1 = new List<LinkBuilder>();
			foreach (var item1 in items.Split([';', ',']))
			{
				var a1 = new StringParser(item1);
				items1.Add(new LinkBuilder
				{
					Href = a1.Get(0),
					InnerHtml = a1.Get(1),
					CssClass = a1.Get(2)
				});
			}
			if (items1.Count > 0)
				Buttons = items1.AsEnumerable();
		}


		/// <param name="items">
		/// "lang=url";...
		/// </param>
		public void SetLangs(
			string items)
		{
			var items1 = new List<LinkBuilder>();
			foreach (var item1 in items.Split([';', ',']))
			{
				var a1 = item1.Split('=');
				items1.Add(new LinkBuilder
				{
					InnerHtml = a1[0],
					Href = a1[1]
				});
			}
			if (items1.Count > 0)
				Langs = items1.AsEnumerable();
		}

	}

}