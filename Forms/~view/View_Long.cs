namespace Ans.Net8.Web.Forms
{

	public class View_Long
		: _View_Text_Base,
		IFormViewControl
	{

		public View_Long(
			string name,
			long? value)
			: base(name, value.ToString(), value, _Consts.MW_Long, true)
		{
		}

	}

}
