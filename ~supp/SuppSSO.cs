using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace Ans.Net8.Web
{

    public static class SuppSSO
    {

        /*
         * SignOutResult GetSignOutResult();
         */


        public static SignOutResult GetSignOutResult()
        {
            return new SignOutResult(
                new[] {
                    OpenIdConnectDefaults.AuthenticationScheme,
                    CookieAuthenticationDefaults.AuthenticationScheme
                });
        }

    }

}
