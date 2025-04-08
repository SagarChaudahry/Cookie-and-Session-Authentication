using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dapper;
using DbFirstCRUD.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace DbFirstCRUD.Services
{

    public class JwtAuthenticationRepository : IJwtAuthenticationRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _db;

        public JwtAuthenticationRepository(ApplicationDbContext db, IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _db = db;

        }
        public async Task<Users?> ValidateUserAsync(string username, string password)
        {
            var sql = "SELECT * FROM Users WHERE UserName = @UserName and Password = @Password";
            var user = await _db.CreateConnection().QueryFirstOrDefaultAsync<Users>(sql, new
            {
                UserName = username,
                Password = password
            });

            if (user == null)
                return null;

            //// Verify password using the PasswordHasher

            //var passwordHasher = new PasswordHasher<Users>();
            //var result = passwordHasher.VerifyHashedPassword(user, user.Password, password);

            return user;
        }

        public async Task<string> GenerateTokenAsync(Users user)
        {
            var jwtKey = _configuration["jwt setting:SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role?? "User")
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["jwt setting:Issuer"],
                audience: _configuration["jwt setting:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateToken(string Token)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["jwt setting:SecretKey"]));
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(Token, new TokenValidationParameters
                {

                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = _configuration["jwt setting:Issuer"],
                    ValidAudience = _configuration["jwt setting:Audience"],
                    ValidateLifetime = true
                }, out var validatedToken);

                return true;


            }

            catch
            {
                return false;
            }
        }

        public ClaimsPrincipal GetClaimsFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            return jwtToken != null ? new ClaimsPrincipal(new ClaimsIdentity(jwtToken.Claims)) : null;
        }
    }
}
