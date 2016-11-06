using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using GetStarted.AuthorizeCode.Services;
using Microsoft.AspNet.Identity;
using Thinktecture.IdentityModel.Clients;

namespace GetStarted.AuthorizeCode.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiService apiService;

        public HomeController()
        {
            apiService = new ApiService();
        }

        [HttpGet]
        [Route("", Name = "Index")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("AuthorizationCallback", Name = "AuthorizationCallback")]
        public ActionResult AuthorizationCallback(string code, string state, string error)
        {
            var tokenUri = new Uri("http://localhost:53189/connect/token");
            var client = new OAuth2Client(tokenUri, "getstartedauthorizecode", "secret");

            var requestResult = client.RequestAccessTokenCode(code, new Uri("http://localhost:61393/AuthorizationCallback"));

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
        [Route("Api", Name = "Api")]
        public ActionResult Api()
        {
            var claimsPrincipal = User as ClaimsPrincipal;

            var result = apiService.GetUser(claimsPrincipal.FindFirst("access_token").Value);
            return View(result.Result);
        }

        [HttpGet]
        [Route("Login", Name = "Login")]
        public ActionResult Login()
        {
            var url = "http://localhost:53189/connect/authorize" +
               "?client_id=getstartedauthorizecode" +
               "&redirect_uri=http://localhost:61393/AuthorizationCallback" +
               "&response_type=code" +
               "&scope=openid+profile" +
               "&response_mode=form_post";

            return Redirect(url);
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