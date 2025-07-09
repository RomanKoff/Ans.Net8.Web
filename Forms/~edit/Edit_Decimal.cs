namespace Ans.Net8.Web.Forms
{

	public class Edit_Decimal
		: _Edit_Text_Base,
		IFormEditControl
	{

		public Edit_Decimal(
			string name,
			decimal? value,
			string cssClasses = null)
			: base(name, value.ToString(), cssClasses, _Consts.MW_Decimal)
		{
		}

	}

}
