using Ans.Net8.Common;

namespace Ans.Net8.Web.Forms
{

	public class Cell_Set
		: _Cell_Text_Base,
		IFormCellControl
	{

		public Cell_Set(
			string value,
			RegistryList registry)
			: base(
				  value
					.Split(Common._Consts.SEPS_ARRAY)
					.MakeFromCollection(x => registry.GetValue(x), null, "<span>{0}</span>", null),
				  true)
		{
		}

	}

}
