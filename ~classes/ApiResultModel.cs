namespace Ans.Net8.Web
{

	public class ApiResultModel
	{
		public ApiResultModel()
		{
		}

		public ApiResultModel(
			string result)
			: this()
		{
			Result = result;
		}

		public string Result { get; set; }
		public string Title { get; set; }
		public string Params { get; set; }
	}

}
