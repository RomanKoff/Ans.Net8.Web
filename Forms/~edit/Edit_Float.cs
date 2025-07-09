namespace Ans.Net8.Web.Forms
{

	public class Edit_Float
		: _Edit_Text_Base,
		IFormEditControl
	{

		public Edit_Float(
			string name,
			float? value,
			string cssClasses = null)
			: base(name, value.ToString(), cssClasses, _Consts.MW_Float)
		{
		}

	}

}
