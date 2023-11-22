using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using school_system_api.config;
using school_system_api.Dto;
using school_system_api.Helpers;
using school_system_api.interfaces;

namespace school_system_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthController(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); ;
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpPost("sign-in")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult SignIn([FromBody] AuthDto payload)
        {
            if (payload == null)
                return BadRequest(ModelState);

            var user = _userRepository.GetUsers()
                .Where(c => c.Email.Trim().ToUpper() == payload.Email.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (user == null)
            {
                ModelState.AddModelError("error", "Invalid credentials");
                return StatusCode(401, ModelState);
            }

            var encrypt = new Encrypt();
            bool isPasswordValid = encrypt.VerifyPassword(payload.Password, user.Password);

            if (!isPasswordValid)
            {
                ModelState.AddModelError("error", "Invalid credentials");
                return StatusCode(401, ModelState);
            }

            var JWT = new Token();
            var token = JWT.SignInToken(user.Id.ToString());

            var config = new Config();
            var cookies = new Cookies();

            cookies.Set(Response, config.CookieName, token);

            return Ok(new
            {
                ok = true,
                token
            });
        }
        [HttpPost("logout")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Logout()
        {
            var config = new Config();
            var cookies = new Cookies();

            cookies.Clear(Response, config.CookieName);


            return Ok(new { message = "Successfully done" });
        }
    }
}