using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserWebAPI.Models;
using UserWebAPI.Services;

namespace UserWebAPI.Controllers
{
    [ApiController]
    [Route("userWeb")]
    public class UserWebController : Controller
    {
        private readonly IUsersWebsService _userWebService;
        private APICredentials _credentials;

        public UserWebController(IUsersWebsService userWebService)
        {
            _userWebService = userWebService;
        }

        [HttpPost]
        [Route("getUserWebs")]
        public ActionResult<ReturnMessage<List<UserWebs>>> get_User([FromBody] Object userData)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(userData.ToString());
            string key = Request.Headers["apiKey"];
            string secret = Request.Headers["secret"];

            ReturnMessage<List<UserWebs>> res = new ReturnMessage<List<UserWebs>>();
            if (key != null && key.Length > 0 && secret != null && secret.Length > 0)
            {
                _credentials = new APICredentials(key, secret);
                res = _userWebService.GetWebsUser("DN", _credentials, int.Parse(data.idUser.ToString()));
            }
            else
            {
                res.Message = "Error: Credenciales API";
            }
            return res;
        }

        [HttpPost]
        [Route("upsertUserWeb")]
        public ActionResult<ReturnMessage<List<UserWebs>>> upsertProduct([FromBody] UserWebs userData)
        {
            string key = Request.Headers["apiKey"];
            string secret = Request.Headers["secret"];
            ReturnMessage<List<UserWebs>> res = new ReturnMessage<List<UserWebs>>();
            if (key != null && key.Length > 0 && secret != null && secret.Length > 0)
            {
                _credentials = new APICredentials(key, secret);
                res = _userWebService.UpsertWeb("DN", _credentials, userData);
            }
            else
            {
                res.Message = "Error: Credenciales API";
            }
            return res;
        }
    }
}
