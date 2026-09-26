using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myStore.DataBase;
using myStore.DTO;
using myStore.Models;
using myStore.ServiceLayer;
using myStore.Services;

namespace myStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
      
        private readonly IUserServiceLayer _userServiceLayer;
        public RegisterController(IUserServiceLayer userServiceLayer, IJwtServices jwtservices) {
            this._userServiceLayer = userServiceLayer;
         }

        [HttpPost("Register")]
        public ActionResult Register(RegisterDTO registerDTO)
        {//usersده الاسم ال في ال دي بي ست  
            _userServiceLayer.Register(registerDTO);
            return Ok("ok");
        }

    
        [HttpPost("Login")]
        public ActionResult Login(LoginDTO loginDTO)
        {
            var result = _userServiceLayer.Login(loginDTO);

            if (result == null)
            {
                return BadRequest("Invalid Email Or Password");
            }

            return Ok(result);
        }
    }
}
