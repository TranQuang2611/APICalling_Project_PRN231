using APICalling_Project_PRN231.AccessDataFromDatabase;
using APICalling_Project_PRN231.DTO;
using APICalling_Project_PRN231.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APICalling_Project_PRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppSetttings _appSetttings;
        private UserRepository _userRepository;

        public UserController(IMapper mapper, IOptionsMonitor<AppSetttings> optionsMonitor, UserRepository userRepository)
        {
            _mapper = mapper;
            _appSetttings = optionsMonitor.CurrentValue;
            _userRepository = userRepository;
        }

        [HttpPost("Login")]
        public IActionResult ValidateUser(LoginModel model)
        {
            var user = _userRepository.GetUser(model);
            if (user == null)
            {
                return Ok(new ApiRespond
                {
                    Success = false,
                    Message = "Invalid Username or Password"
                });
            }
            else
            {
                UserDTO userDTO = _mapper.Map<UserDTO>(user);   
                //cấp token
                return Ok(new ApiRespond
                {
                    Success = true,
                    Message = "",
                    Token = GenerateToken(user),
                    UserDTO = userDTO
                });
            }
        }

        private string GenerateToken(User user)
        {
            var jwtTokenHandle = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSetttings.SecretKey);
            var tokenDes = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),

                    //role
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = jwtTokenHandle.CreateToken(tokenDes);
            return jwtTokenHandle.WriteToken(token);
        }
    }
}
