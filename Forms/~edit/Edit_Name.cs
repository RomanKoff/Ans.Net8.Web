namespace Ans.Net8.Web.Forms
{

	public class Edit_Name
		: _Edit_Text_Base,
		IFormEditControl
	{

		public Edit_Name(
			string name,
			string value,
			string cssClasses = null)
			: base(name, value, cssClasses, _Consts.MW_Name)
		{
		}

	}

}
