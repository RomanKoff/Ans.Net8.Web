namespace Ans.Net8.Web.Forms
{

	public class Edit_Email
		: _Edit_Text_Base,
		IFormEditControl
	{

		public Edit_Email(
			string name,
			string value,
			string cssClasses = null)
			: base(name, value, cssClasses, _Consts.MW_Email)
		{
		}

	}

}
