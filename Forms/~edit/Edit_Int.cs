namespace Ans.Net8.Web.Forms
{

	public class Edit_Int
		: _Edit_Text_Base,
		IFormEditControl
	{

		public Edit_Int(
			string name,
			int? value,
			string cssClasses = null)
			: base(name, value.ToString(), cssClasses, _Consts.MW_Int)
		{
		}

	}

}
