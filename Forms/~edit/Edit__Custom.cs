namespace Ans.Net8.Web.Forms
{

	public class Edit__Custom
		: IFormEditControl
	{

		/* ctor */


		public Edit__Custom(
			string name,
			string control)
		{
			Name = name;
			Control = control;
		}


		/* readonly properties */


		public string Name { get; }
		public string Control { get; }


		/* functions */


		public override string ToString()
		{
			return $"{Control}";
		}

	}

}
