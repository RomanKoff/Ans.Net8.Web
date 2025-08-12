using Ans.Net8.Common;
using Ans.Net8.Common.Resources;
using Microsoft.AspNetCore.Html;
using System.Resources;

namespace Ans.Net8.Web.Forms
{

	public class FormResources(
		params ResourceManager[] resources)
		: ResourcesHelper(resources)
	{

		/* readonly properties */


		private string _titlePluralize;
		public string TitlePluralize
			=> _titlePluralize ??= GetCalcFaceHelper("_TitlePluralize").Title;

		private HtmlString _titlePluralize_Html;
		public HtmlString TitlePluralize_Html
			=> _titlePluralize_Html ??= TitlePluralize.ToHtml(true);


		private string _titleWhoWhat;
		public string TitleWhoWhat
			=> _titleWhoWhat ??= GetCalcFaceHelper("_TitleWhoWhat").Title;

		private HtmlString _titleWhoWhat_Html;
		public HtmlString TitleWhoWhat_Html
			=> _titleWhoWhat_Html ??= TitleWhoWhat.ToHtml(true);


		public string ListPageTitle
			=> string.Format(Form.Template_PageTitle_List, TitlePluralize);


		public string AddPageTitle
			=> string.Format(Form.Template_PageTitle_Add, TitleWhoWhat);


		public string EditPageTitle
			=> string.Format(Form.Template_PageTitle_Edit, TitleWhoWhat);


		public string DeletePageTitle
			=> string.Format(Form.Template_PageTitle_Delete, TitleWhoWhat);


		private HtmlString _text_EmptyItems_Html;
		public HtmlString Text_EmptyItems_Html
			=> _text_EmptyItems_Html ??= Form.Text_EmptyItems.ToHtml(true);


		private HtmlString _text_Cancel_Html;
		public HtmlString Text_Cancel_Html
			=> _text_Cancel_Html ??= Form.Text_Cancel.ToHtml(true);


		private HtmlString _text_Add_Html;
		public HtmlString Text_Add_Html
			=> _text_Add_Html ??= Form.Text_Add.ToHtml(true);


		private HtmlString _text_Save_Html;
		public HtmlString Text_Save_Html
			=> _text_Save_Html ??= Form.Text_Save.ToHtml(true);


		private HtmlString _text_SubmitAdd_Html;
		public HtmlString Text_SubmitAdd_Html
			=> _text_SubmitAdd_Html ??= Form.Text_SubmitAdd.ToHtml(true);


		private HtmlString _text_SubmitSave_Html;
		public HtmlString Text_SubmitSave_Html
			=> _text_SubmitSave_Html ??= Form.Text_SubmitSave.ToHtml(true);


		private HtmlString _text_SubmitDelete_Html;
		public HtmlString Text_SubmitDelete_Html
			=> _text_SubmitDelete_Html ??= Form.Text_SubmitDelete.ToHtml(true);


		private HtmlString _title_Edit_Html;
		public HtmlString Title_Edit_Html
			=> _title_Edit_Html ??= Form.Title_Edit.ToHtml(true);


		private HtmlString _title_Delete_Html;
		public HtmlString Title_Delete_Html
			=> _title_Delete_Html ??= Form.Title_Delete.ToHtml(true);

	}

}
