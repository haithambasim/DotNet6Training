using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Training.Data.Dtos;
using Training.Data.Entities;
using Training.Data.EntityFrameworkCore;
using Training.Exceptions;

namespace Training.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly CmsContext _cmsContext;
        private readonly IMapper _mapper;
        private IConfiguration _config;

        public AccountController(CmsContext cmsContext, IMapper mapper, IConfiguration config)
        {
            _cmsContext = cmsContext;
            _mapper = mapper;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<UserLoginResultDto> Login(UserLoginDto input)
        {
            if (string.IsNullOrWhiteSpace(input.UserName) || string.IsNullOrWhiteSpace(input.Password))
                throw new UserFriendlyException("Invalid username or password");

            var userAccount = await _cmsContext.UserAccounts.FirstOrDefaultAsync(x => x.UserName == input.UserName && x.Password == input.Password);

            if (userAccount == null) throw new UserFriendlyException("Wrong username or password");

            var tokenInfo = GenerateToken(userAccount);

            return new UserLoginResultDto
            {
                Account = _mapper.Map<UserAccountDto>(userAccount),
                Token = tokenInfo.token,
                Expiry = tokenInfo.expiry,
            };
        }

        private (string token, DateTime expiry) GenerateToken(UserAccount user)
        {
            SymmetricSecurityKey securityKey = new(Convert.FromBase64String(_config.GetSection("Jwt:Key").Get<string>()));

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenExpiry = DateTime.Now.AddDays(10);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Expires = tokenExpiry,
                Subject = new ClaimsIdentity(new[] {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                }),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken stoken = tokenHandler.CreateToken(tokenDescriptor);

            var token = tokenHandler.WriteToken(stoken);

            return (token, tokenExpiry);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<UserAccountDto> Register([FromBody] UserRegisterDto input)
        {
            if (input == null)
                throw new UserFriendlyException("please provide registeration info");

            if (string.IsNullOrWhiteSpace(input.UserName) || !Regex.IsMatch(input.UserName, "^[a-zA-Z][a-zA-Z0-9]*$"))
                throw new UserFriendlyException("Please enter a valid username");

            if (string.IsNullOrWhiteSpace(input.Password))
                throw new UserFriendlyException("Please enter a valid password");

            // check username if duplicated 
            if (await _cmsContext.UserAccounts.AnyAsync(x => x.UserName == input.UserName))
            {
                throw new UserFriendlyException("Username is already Taken");
            }

            var newUserAccount = new UserAccount()
            {
                Role = "User"
            };

            _mapper.Map(input, newUserAccount);

            await _cmsContext.AddAsync(newUserAccount);

            await _cmsContext.SaveChangesAsync();

            return _mapper.Map<UserAccountDto>(newUserAccount);
        }

        [HttpGet("get-my-info")]
        public UserAccountDto GetMyInfo()
        {
            var claims = HttpContext.User.Claims;

            return new UserAccountDto()
            {
                Id = Convert.ToInt64(claims.FirstOrDefault(x => x.Type == "Id")?.Value ?? "0"),
                UserName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value,
                Email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                PhoneNumber = claims.FirstOrDefault(x => x.Type == ClaimTypes.MobilePhone)?.Value,
                Role = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value,
            };
        }
    }
}
