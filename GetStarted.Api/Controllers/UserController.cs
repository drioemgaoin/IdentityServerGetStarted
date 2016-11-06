using System.Net;
using System.Web.Http;
using GetStarted.Api.Models;
using Swashbuckle.Swagger.Annotations;

namespace GetStarted.Api.Controllers
{
    [RoutePrefix("users")]
    public class UserController : ApiController
    {
        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, "Get called succesfully")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Get called failed")]
        public IHttpActionResult Get()
        {
            var user = new User { Name = "rdiegoni" };
            return Content(HttpStatusCode.OK, user);
        }

        [HttpGet]
        [Authorize]
        [Route("withAuth", Name = "withAuth")]
        [SwaggerResponse(HttpStatusCode.OK, "Get called succesfully")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Get called failed")]
        public IHttpActionResult GetWithAuth()
        {
            var user = new User { Name = "rdiegoni" };
            return Content(HttpStatusCode.OK, user);
        }
    }
}
