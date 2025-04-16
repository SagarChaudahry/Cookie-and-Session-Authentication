using DbFirstCRUD.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DbFirstCRUD.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DbFirstCRUD.Controllers
{
    public class JwtAuthController : Controller
    {

        private readonly IUserRepository _userRepository;
        private readonly IJwtAuthenticationRepository _jwtAuthRepository;


        public JwtAuthController(IUserRepository userRepository, IJwtAuthenticationRepository jwtAuthenticationRepository)
        {
            _userRepository = userRepository;
            _jwtAuthRepository = jwtAuthenticationRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Register(Users users)
        {
            if (users == null)
                return BadRequest("User Not Found");

            var existingUser = await _userRepository.GetByUserNameAsync(users);
            if (existingUser != null)
                return BadRequest("User already exists.");

            users.Password = HashPassword(users.Password);


            users.Role = "User"; //

            await _userRepository.AddUserAsync(users);

            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Users user)
        {
            if (user == null)
                return BadRequest("User Not Found");

            var existingUser = await _userRepository.GetByUserNameAsync(user);
            if (existingUser == null)
                return BadRequest("User Does Not Exist.");

            var passwordHasher = new PasswordHasher<Users>();
            var result = passwordHasher.VerifyHashedPassword(existingUser, existingUser.Password, user.Password);

            if (result == PasswordVerificationResult.Failed)
                return BadRequest("Invalid password.");


            var token = await _jwtAuthRepository.GenerateTokenAsync(existingUser);

            Response.Cookies.Append("AuthToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            });

            return RedirectToAction("Index", "Employee");
            return Ok(new
            {
                message = "Logged in Successfully",
                user = existingUser,
                token = token
            });

        }

        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("AuthToken");
            return RedirectToAction("Login");
        }
        private string GenerateToken(Users user)
        {
            var jwtKey = " ";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Role, user.Role ?? "User")
        };
            var token = new JwtSecurityToken(
                issuer: " ",
                audience: " ",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string HashPassword(string password)
        {
            var passwordHasher = new PasswordHasher<Users>();
            return passwordHasher.HashPassword(new Users(), password);
        }

    }
}
