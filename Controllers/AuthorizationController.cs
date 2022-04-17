using backend.Controllers.Repositories;
using backend.Dtos;
using backend.Helpers;
using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly JwtService _jwtService;

        public AuthorizationController(IUserRepository repository, JwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

     

        [Route("register")]
        [HttpPost]
        public IActionResult Create(RegisterDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            return Created(uri: "success", value: _repository.Create(user));
            //return Ok("success");
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login(LoginDto dto)
        {
            try
            {
                var user = _repository.GetUserByEmail(dto.Email);

                //if (user == null) return BadRequest(error: new { message = "Invalid Credentials" });

                //if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password)) return BadRequest(error: new { message = "Invalid Credentials" });

                var jwt = _jwtService.Generate(user.Id);
                return Ok(new
                {
                    jwt = jwt,
                    name = user.Name
                });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
           
            //Response.Cookies.Append("jwt", jwt, new CookieOptions
            //{
            //    SameSite = SameSiteMode.Lax,

            //});
            //return Ok(new
            //{
            //    jwt = jwt,
            //    name = user.Name
            //});
        }


        [Route("user")]
        [HttpPost]
        public IActionResult User(string? jwtToken)
        {
            try
            {
                
                var token = _jwtService.Verify(jwtToken);
                int userID = int.Parse(token.Issuer);
                var user = _repository.GetById(userID);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new
            {
                message = "cookies delete"
            });
        }

    }
}
