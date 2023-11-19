using Ans.Net8.Common;
using Microsoft.AspNetCore.Authorization;

namespace Ans.Net8.Web.Attributes
{

	public class AuthAppAttribute
		: AuthorizeAttribute
	{
		public AuthAppAttribute()
			: base(_Consts.AUTH_POLICY_APP) { }
	}



	public class AuthAdminAttribute
		: AuthorizeAttribute
	{
		public AuthAdminAttribute()
			: base(_Consts.AUTH_POLICY_APP_ADMINS) { }
	}



	public class AuthModeratorAttribute
		: AuthorizeAttribute
	{
		public AuthModeratorAttribute()
			: base(_Consts.AUTH_POLICY_APP_MODERATORS) { }
	}



	public class AuthEditorAttribute
		: AuthorizeAttribute
	{
		public AuthEditorAttribute()
			: base(_Consts.AUTH_POLICY_APP_EDITORS) { }
	}



	public class AuthReaderAttribute
		: AuthorizeAttribute
	{
		public AuthReaderAttribute()
			: base(_Consts.AUTH_POLICY_APP_READERS) { }
	}



	public class AuthUserAttribute
		: AuthorizeAttribute
	{
		public AuthUserAttribute()
			: base(_Consts.AUTH_POLICY_APP_USERS) { }
	}

}
