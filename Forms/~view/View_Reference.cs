using Ans.Net8.Common;

namespace Ans.Net8.Web.Forms
{

	public class View_Reference
		: _View_Reference_Base,
		IFormViewControl
	{

		public View_Reference(
			string name,
			int? value,
			RegistryList registry)
			: base(
				  name,
				  value.ToString(),
				  registry)
		{
		}

	}

}
