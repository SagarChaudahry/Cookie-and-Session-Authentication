//using Microsoft.AspNetCore.Mvc;
//using DbFirstCRUD.Data.Entities;
//using DbFirstCRUD.Services;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using System.Security.Claims;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Authorization;

//namespace DbFirstCRUD.Controllers
//{
//    public class AuthenticationController : Controller
//    {
//        private readonly IUserRepository _userRepository;

//        public AuthenticationController(IUserRepository userRepository,IJwtAuthenticationRepository jwtAuthenticationRepository)
//        {
//            _userRepository = userRepository;

//        }
//        [AllowAnonymous]

//        [HttpGet]
//        public IActionResult Register()
//        {
//            return View();
//        }

//        [HttpPost("Register")]
//        public async Task<IActionResult> Register(Users users)
//        {
//            if (users == null)
//                return BadRequest("User Not Found");

//            var existingUser = await _userRepository.GetByUserNameAsync(users);
//            if (existingUser != null)
//                return BadRequest("User already exists.");

//            await _userRepository.AddUserAsync(users);

//            return RedirectToAction("Login");
//        }
//        [AllowAnonymous]
//        [HttpGet]
//        public IActionResult Login()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Login(Users users)
//        {
//            if (users == null)
//                return BadRequest("User Not Found");

//            var existingUser = await _userRepository.GetByUserNameAsync(users);
//            if (existingUser == null)
//                return BadRequest("User Does Not Exist.");


//            //Session
//            HttpContext.Session.SetString("UserName", existingUser.UserName);
//            HttpContext.Session.SetInt32("UserId", existingUser.UserId);

//            var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.Name, existingUser.UserName),
//                new Claim(ClaimTypes.NameIdentifier, existingUser.UserId.ToString()),
//                new Claim(ClaimTypes.Role, "User")
                
//            };

//            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
//            var authProperties = new AuthenticationProperties
//            {
//                IsPersistent = true
//            };
//            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity),authProperties);

//            return RedirectToAction("Index", "Home");
//        }

//        [HttpPost]
//        public async Task<IActionResult> Logout()
//        {
//            HttpContext.Session.Clear();

//            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
//            return RedirectToAction("Login");
//        }
//    }
//}
