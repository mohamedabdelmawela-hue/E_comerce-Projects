using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myStore.DataBase;
using myStore.DTO;
using myStore.Models;
using myStore.Services;

namespace myStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IJwtServices _jwtServices;
        E_comerceContext db;
        public RegisterController(E_comerceContext db, IJwtServices jwtservices) {
            this.db = db;
            _jwtServices = jwtservices;
        }

        [HttpPost("Register")]
        public ActionResult Register(RegisterDTO registerDTO)
        {//usersده الاسم ال في ال دي بي ست  
            var Result=db.Users.FirstOrDefault(x=>x.userEmail==registerDTO.userEmail);
            if (Result != null)
            {
                return BadRequest("Email is exist !!!");
            }
            else
            {
                //ده object من الموديل الانتيتي
                var user = new UsersModel
                {
                    userName = registerDTO.userName,
                    userPassword = registerDTO.userPassword,
                    userEmail = registerDTO.userEmail,
                    userAddress = registerDTO.userAddress

                };
                db.Users.Add(user);
                db.SaveChanges();
                return Ok("user Created");
}

        }
        [HttpPost("Login")]
        public ActionResult Login(LoginDTO loginDTO)
        {
            var rer = db.Users.FirstOrDefault(X => X.userEmail == loginDTO.Email && X.userPassword == loginDTO.Password);
            if (rer == null)
            {
                return BadRequest("Data not corect");
            }
            var token = _jwtServices.GeneratToken(rer);

            return Ok(token);

        }
    }
}
