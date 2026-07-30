using Microsoft.IdentityModel.Tokens;
using myStore.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace myStore.Services
{
    /*public string GenerateToken(UsersModel user)
{
    // 1) البيانات التي ستوضع داخل الـ Token
    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email)
    };

    // 2) المفتاح السري
    var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

    // 3) طريقة التوقيع
    var creds = new SigningCredentials(
        key,
        SecurityAlgorithms.HmacSha256);

    // 4) إنشاء الـ Token
    var token = new JwtSecurityToken(
        issuer: _configuration["JWT:Issuer"],
        audience: _configuration["JWT:Audience"],
        claims: claims,
        expires: DateTime.Now.AddHours(1),
        signingCredentials: creds
    );

    // 5) تحويلها إلى String
    return new JwtSecurityTokenHandler().WriteToken(token);
}*/
    /*
    public interface IJwtservices
    {
        public string GenerateToken(UsersModel usersModel);
    }
    public class jwtServises : IJwtservices
    {
        private readonly IConfiguration config ;
        public jwtServises (IConfiguration configuration){
            config = configuration;
}
        public string GenerateToken(UsersModel usersModel) { 
        var claims= new List<Claim> { 
        new Claim(ClaimTypes.Email,usersModel.userEmail),
        new Claim(ClaimTypes.Name,usersModel.userName),
        
        
        };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["jwt:key"]!));
            var cards=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token=new JwtSecurityToken(
                issuer: config["jwt:Issuer"],
                audience: config["jwt:Audience" ],
                claims:claims,
                expires:DateTime.Now.AddHours(2),
                signingCredentials:cards);
            return new JwtSecurityTokenHandler().WriteToken(token);


        
        
        
        }
        
    }*/
    public interface IJwtServices
    {
        public string GeneratToken(UsersModel user);
    }
    public class JwtServices : IJwtServices
    {
        private readonly IConfiguration _config;
        public JwtServices(IConfiguration config)
        {
            _config = config;
        }
        public string GeneratToken(UsersModel user)
        {
            var claims=new List<Claim> { 
            new Claim(ClaimTypes.Email,user.userEmail),
            new Claim(ClaimTypes.Name,user.userName),
            };

            var kkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:key"]!));
            var crads=new SigningCredentials(kkey,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["jwt:Issuer"],
                audience: _config["jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: crads);
            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
