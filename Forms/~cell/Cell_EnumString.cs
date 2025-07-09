using Ans.Net8.Common;

namespace Ans.Net8.Web.Forms
{

	public class Cell_EnumString
		: _Cell_Text_Base,
		IFormCellControl
	{

		public Cell_EnumString(
			string value,
			RegistryList registry)
			: base(registry.GetValueOrKey(value), true)
		{
		}

	}

}
