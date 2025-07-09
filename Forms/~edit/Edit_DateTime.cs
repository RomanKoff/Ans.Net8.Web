namespace Ans.Net8.Web.Forms
{

	public class Edit_DateTime
		: _Edit_Text_Base,
		IFormEditControl
	{

		public Edit_DateTime(
			string name,
			DateTime? value,
			string cssClasses = null)
			: base(
				  name,
				  value?.ToString("g"),
				  cssClasses,
				  _Consts.MW_DateTime)
		{
		}

	}

}
