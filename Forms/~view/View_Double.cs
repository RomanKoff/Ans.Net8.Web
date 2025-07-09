namespace Ans.Net8.Web.Forms
{

	public class View_Double
		: _View_Text_Base,
		IFormViewControl
	{

		public View_Double(
			string name,
			double? value)
			: base(name, value.ToString(), value, _Consts.MW_Double, true)
		{
		}

	}

}
