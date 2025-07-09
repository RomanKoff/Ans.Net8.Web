namespace Ans.Net8.Web.Forms
{

	public class Edit_Long
		: _Edit_Text_Base,
		IFormEditControl
	{

		public Edit_Long(
			string name,
			long? value,
			string cssClasses = null)
			: base(name, value.ToString(), cssClasses, _Consts.MW_Long)
		{
		}

	}

}
