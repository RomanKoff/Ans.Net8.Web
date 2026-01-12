using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Ans.Net8.Web
{

	public class SendService(
		CurrentContext current)
	{

		private readonly CurrentContext _current = current;


		/* methods */


		public void Email(
			string name,
			string address,
			string subject,
			string viewName,
			object model,
			string bcc = null)
		{
			var to1 = new MimeKit.MailboxAddress(name, address);
			var content1 = _current.ViewRender.RenderViewToStringAsync(
				viewName, model).Result;
			var message1 = new Common.MailMessageModel
			{
				To = to1,
				Subject = subject,
				ContentHtml = content1,
			};
			if (!string.IsNullOrEmpty(bcc))
			{
				var a1 = bcc.Split(';').Select(x => new MimeKit.MailboxAddress(null, x));
				message1.Bcc = [.. a1];
			}
			_current.Mailer.SendAsync(message1);
		}

	}

}
