namespace Ans.Net8.Web
{

	public class FieldHelper
		: _HtmlWrapper_Base
	{

		/* ctors */


		public FieldHelper(
			FormHelper form,
			string name)
			: base(form.Helper)
		{
			Name = name;
			WrapperStart();
		}


		/* readonly properties */


		public string Name { get; private set; }


		/* methods */


		public override void WrapperStart()
		{
			ViewContext.Writer.WriteLine($"<div class=\"form-field my-5\">");
		}


		public override void WrapperStop()
		{
			ViewContext.Writer.WriteLine("</div>");
		}

	}

}
