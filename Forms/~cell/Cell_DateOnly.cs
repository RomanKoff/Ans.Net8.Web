namespace Ans.Net8.Web.Forms
{

	public class Cell_DateOnly
		: _Cell_Text_Base,
		IFormCellControl
	{

		public Cell_DateOnly(
			DateOnly? value)
			: base(value?.ToString(), true)
		{
		}

	}

}
