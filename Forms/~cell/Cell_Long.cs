namespace Ans.Net8.Web.Forms
{

	public class Cell_Long
		: _Cell_Text_Base,
		IFormCellControl
	{

		public Cell_Long(
			long? value)
			: base(value.ToString(), true)
		{
		}

	}

}
