using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using GetStarted.ImplicitClient.Services;

namespace GetStarted.ImplicitClient.Controllers
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
        [Authorize]
        [Route("Logout", Name = "Logout")]
        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }
    }
}