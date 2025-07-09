using Ans.Net8.Common;

namespace Ans.Net8.Web.Forms
{

	public class Cell_Reference
		: _Cell_Text_Base,
		IFormCellControl
	{

		public Cell_Reference(
			int? value,
			RegistryList registry)
			: base(registry.GetValue(value?.ToString()), true)
		{
		}

	}

}
