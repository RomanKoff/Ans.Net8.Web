namespace Ans.Net8.Web.Forms
{

	public class View_Decimal
		: _View_Text_Base,
		IFormViewControl
	{

		public View_Decimal(
			string name,
			decimal? value)
			: base(name, value.ToString(), value, _Consts.MW_Decimal, true)
		{
		}

	}

}
