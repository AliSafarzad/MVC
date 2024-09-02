using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Models;

namespace UseApi.Security
{
    public class SecurityManager
    {
        public async void SignIn(HttpContext httpContext, string _username, string _token)
        {

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(getUserClaims(_username, _token)
               , CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
        }
       
        private IEnumerable<Claim> getUserClaims(string _username,string _token)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("UserName", _username));
            claims.Add(new Claim("Token", _token));
         

            return claims;
        }


        public async void SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
        }
    }
}
