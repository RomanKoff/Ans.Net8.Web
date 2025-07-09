namespace Ans.Net8.Web.Forms
{

	public class View_DateOnly
		: _View_Text_Base,
		IFormViewControl
	{

		public View_DateOnly(
			string name,
			DateOnly? value)
			: base(
				  name,
				  value?.ToString(),
				  value,
				  _Consts.MW_DateOnly,
				  true)
		{
		}

	}

}
