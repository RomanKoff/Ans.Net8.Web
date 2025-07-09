namespace Ans.Net8.Web.Forms
{

	public class Cell_TimeOnly
		: _Cell_Text_Base,
		IFormCellControl
	{

		public Cell_TimeOnly(
			TimeOnly? value)
			: base(value?.ToString(), true)
		{
		}

	}

}
