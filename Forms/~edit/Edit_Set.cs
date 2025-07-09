using Ans.Net8.Common;

namespace Ans.Net8.Web.Forms
{

	public class Edit_Set
		: _Edit_Registry_Base,
		IFormEditControl
	{

		public Edit_Set(
			string name,
			int[] value,
			RegistryList registry,
			string cssClasses = null)
			: base(
				  name,
				  value.MakeFromCollection(x => x.ToString(), null, null, ","),
				  registry,
				  RegistryModeEnum.Auto,
				  cssClasses,
				  true)
		{
		}

	}

}
