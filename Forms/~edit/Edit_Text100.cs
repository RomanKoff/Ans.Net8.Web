namespace Ans.Net8.Web.Forms
{

	public class Edit_Text100
		: _Edit_Text_Base,
		IFormEditControl
	{

		public Edit_Text100(
			string name,
			string value,
			string cssClasses = null)
			: base(name, value, cssClasses, _Consts.MW_Text100)
		{
		}

	}

}
