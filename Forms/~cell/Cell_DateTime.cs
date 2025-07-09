namespace Ans.Net8.Web.Forms
{

	public class Cell_DateTime
		: _Cell_Text_Base,
		IFormCellControl
	{

		public Cell_DateTime(
			DateTime? value)
			: base(value?.ToString("g"), true)
		{
		}

	}

}
