using Ans.Net8.Common;

namespace Ans.Net8.Web.Forms
{

	public class Cell_Enum
		: _Cell_Text_Base,
		IFormCellControl
	{

		public Cell_Enum(
			int value,
			RegistryList registry)
			: base(registry.GetValue(value), true)
		{
		}

	}

}
