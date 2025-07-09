using Ans.Net8.Common;

namespace Ans.Net8.Web.Forms
{

	public class _Cell_Text_Base
		: IFormCellControl
	{

		/* ctor */


		public _Cell_Text_Base(
			string value,
			bool useRaw)
		{
			Value = value;
			UseRaw = useRaw;
		}


		/* readonly properties */


		public string Value { get; }
		public bool UseRaw { get; }


		/* functions */


		public override string ToString()
		{
			return string.IsNullOrEmpty(Value)
				? "&nbsp;"
				: UseRaw
					? Value
					: SuppTypograph.GetText2Html(Value);
		}

	}

}
