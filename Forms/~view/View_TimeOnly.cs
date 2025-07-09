namespace Ans.Net8.Web.Forms
{

	public class View_TimeOnly
		: _View_Text_Base,
		IFormViewControl
	{

		public View_TimeOnly(
			string name,
			TimeOnly? value)
			: base(
				  name,
				  value?.ToString(),
				  value,
				  _Consts.MW_TimeOnly,
				  true)
		{
		}

	}

}
