namespace Ans.Net8.Web.Forms
{

	public class View_DateTime
		: _View_Text_Base,
		IFormViewControl
	{

		public View_DateTime(
			string name,
			DateTime? value)
			: base(
				  name,
				  value?.ToString("g"),
				  value,
				  _Consts.MW_DateTime,
				  true)
		{
		}

	}

}
