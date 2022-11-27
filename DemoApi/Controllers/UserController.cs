using Infrastructure;
using Infrastructure.Models.User;
using Infrastructure.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, ILogger<UserController> logger, IConfiguration configuration)
        {
            _userService = userService;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var user = _userService.GetById(id);
            return Ok(user);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> CreateAsync(CreateUserDto model)
        {
            _logger.LogInformation($"Registration attepmpted for {model.Email}");

            try
            {
                var result = await _userService.CreateAsync(model);
                return Ok(new { message = "User created" });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateAsync)}");
                return Problem($"Something went wrong in the {nameof(CreateAsync)}", statusCode: 500);
            }
        }


        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginUserDto loginDto)
        {
            _logger.LogInformation($"Login attempted for {loginDto.Email}");

            try
            {
                var user = await _userService.GetByEmailAsync(loginDto);

                if (user == null)
                {
                    return Unauthorized(loginDto);
                }

                if(!user.Active)
                {
                    return Unauthorized("Contul nu este activ!");
                }

                string tokenString = await GenerateToken(user);

                var response = new AuthResponse
                {
                    Email = user.Email,
                    Token = tokenString,
                    UserId = user.Id
                };
                
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Login)}");
                return Problem($"Something went wrong in the {nameof(Login)}", statusCode: 500);
            }
        }

        [HttpDelete]
        [Route("DeactivateUser")]
        public async Task<ActionResult> Deactivate(Guid userId)
        {
            _userService.Delete(userId);
            return Ok();
        }

        private async Task<string> GenerateToken(UserDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userService.GetUserRoles(user);

            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role,q)).ToList();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString()),
                new Claim("active", user.Active.ToString()),
            }
            .Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"], 
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_configuration["JwtSettings:Duration"] != null ? int.Parse(_configuration["JwtSettings:Duration"]) : 1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
