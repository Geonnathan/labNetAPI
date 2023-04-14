using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserWebAPI.Models;
using UserWebAPI.Services;

namespace UserWebAPI.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private APICredentials _credentials;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("{dataSource}/getUser")]
        public ActionResult<ReturnMessage<List<User>>> get_User(string dataSource, [FromBody] UserLogin userData)
        {
            User user = new User(userData.email.ToString(), userData.password.ToString());
            string key = Request.Headers["apiKey"];
            string secret = Request.Headers["secret"];
            ReturnMessage<List<User>> res = new ReturnMessage<List<User>>();
            if (key != null && key.Length > 0 && secret != null && secret.Length > 0)
            {
                _credentials = new APICredentials(key, secret);
                res = _userService.GetUser(dataSource, _credentials, user);
            }
            else
            {
                res.Message = "Error: Credenciales API";
            }
            return res;
        }

        [HttpPost]
        [Route("{dataSource}/upsertUser")]
        public ActionResult<ReturnMessage<List<User>>> upsert_User(string dataSource, [FromBody] Object userData)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(userData.ToString());
            User user = new User();
            if (data.idUser == null)
            {
                user = new User(data.name.ToString(), data.email.ToString(), data.password.ToString());
            }
            else
            {
                user = new User(int.Parse(data.idUser.ToString()), data.name.ToString(), data.email.ToString(), data.password.ToString());
            }
            string key = Request.Headers["apiKey"];
            string secret = Request.Headers["secret"];
            ReturnMessage<List<User>> res = new ReturnMessage<List<User>>();
            if (key != null && key.Length > 0 && secret != null && secret.Length > 0)
            {
                _credentials = new APICredentials(key, secret);
                res = _userService.UpsertUser(dataSource, _credentials, user);
            }
            else
            {
                res.Message = "Error: Credenciales API";
            }
            return res;
        }
    }
}
