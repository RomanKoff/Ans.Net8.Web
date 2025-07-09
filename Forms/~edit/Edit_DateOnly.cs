namespace Ans.Net8.Web.Forms
{

	public class Edit_DateOnly
		: _Edit_Text_Base,
		IFormEditControl
	{

		public Edit_DateOnly(
			string name,
			DateOnly? value,
			string cssClasses = null)
			: base(
				  name,
				  value?.ToString(),
				  cssClasses,
				  _Consts.MW_DateOnly)
		{
		}

	}

}
