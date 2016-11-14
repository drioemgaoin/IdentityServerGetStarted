using System.Web.Mvc;
using GetStarted.ResourceOwnerPwd.Services;

namespace GetStarted.ResourceOwnerPwd.Controllers
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
        [Route("Api", Name = "Api")]
        public ActionResult Api()
        {
            var username = apiService.GetUser().Result;
            return View(username);
        }
    }
}