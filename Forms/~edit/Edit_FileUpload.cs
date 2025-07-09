namespace Ans.Net8.Web.Forms
{

	public class Edit_FileUpload
		: IFormEditControl
	{

		/* ctor */


		public Edit_FileUpload(
			string name)
		{
			Name = name;
			Control = new(Name);
			Control.AddCssClass("form-control");
		}


		/* readonly properties */


		public string Name { get; }
		public InputFileTag Control { get; }


		/* functions */


		public override string ToString()
		{
			return $"{Control}";
		}

	}

}
