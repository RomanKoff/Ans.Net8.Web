namespace Ans.Net8.Web.Forms
{

	public class Cell_Decimal
		: _Cell_Text_Base,
		IFormCellControl
	{

		public Cell_Decimal(
			decimal? value)
			: base(value.ToString(), true)
		{
		}

	}

}
