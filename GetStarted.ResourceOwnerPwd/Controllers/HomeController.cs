using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Thinktecture.IdentityModel.Clients;

namespace GetStarted.ResourceOwnerPwd.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("", Name = "Index")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Login", Name = "Login_Get")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        [Route("Private", Name = "Private")]
        public ActionResult Private()
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            return View(claimsPrincipal.Claims);
        }

        [HttpPost]
        [Route("Login", Name = "Login_Post")]
        public ActionResult Login(string username, string password)
        {
            var tokenUri = new Uri("http://localhost:53189/connect/token");
            var client = new OAuth2Client(tokenUri, "getstartedresourceownerpassword", "secret");

            var requestResult = client.RequestAccessTokenUserName(username, password, "openid profile");

            var claims = new List<Claim>
            {
                new Claim("access_token", requestResult.AccessToken)
            };

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            Request.GetOwinContext().Authentication.SignIn(identity);

            return Redirect("/");
        }

        [HttpGet]
        [Authorize]
        [Route("Logout", Name = "Logout")]
        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }
    }
}