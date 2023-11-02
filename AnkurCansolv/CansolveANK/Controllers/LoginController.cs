using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace CansolveANK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private DeepAnkuJwtProvider Authtication(DeepAnkuJwtProvider jwtProvider)
        {
            DeepAnkuJwtProvider _user = null;
            if (jwtProvider.Username == "Avinexuser" && jwtProvider.Password == "ama")
            {
                _user = new DeepAnkuJwtProvider
                {
                    Username = "Avinexuser",
                    Password = "ama"
                };
            }
            return _user;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public String GenerateToken(DeepAnkuJwtProvider tokenprovider)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], null, expires: DateTime.Now.AddMinutes(2), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }




        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(DeepAnkuJwtProvider _user)
        {
            IActionResult response = Unauthorized();
            var user = Authtication(_user);
            if (user != null)
            {
                var token = GenerateToken(_user);
                response = Ok(new
                {
                    token = token
                });
            }



            return response;



        }

    }
}
