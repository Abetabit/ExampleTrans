using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Transavia.Core.Entities;
using Transavia.Web.Helpers;
using Transavia.Web.Models;

namespace Transavia.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly AppSettings _appSettings;


        public AccountController(IOptions<AppSettings> appSettings, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {


            try
            {
                var signin = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (!signin.Succeeded)
                    return Ok($"This combo of a user and password does not exist");

                else if (signin.IsLockedOut)
                    return BadRequest("You are locked out");

                User user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                    return BadRequest("This user does not exist");

                return await GenerateToken(user);
            }
            catch
            {
                return BadRequest(model);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok("The user is loged out");
            }
            catch
            {
                return BadRequest("Something went wrong");
            }
        }

        public async Task<IActionResult> GenerateToken(User user)
        {
            try
            {
                IList<string> rolesOfUser = await _userManager.GetRolesAsync(user);

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                List<Claim> claims = new List<Claim>
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("fullName", $"{user.FirstName} {user.LastName}")
                };

                foreach (var roleItem in rolesOfUser)
                {
                    claims.Add(new Claim(ClaimTypes.Role, roleItem.ToString()));
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { Token = tokenString });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Something went wrong");

                var user = new User { UserName = model.UserName, Email = model.UserName };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result == IdentityResult.Success)
                {

                    await _userManager.AddToRoleAsync(user, model.RoleName);

                    var roles = await _userManager.GetRolesAsync(user);

                    return Ok(new
                    {
                        user.Id,
                        user.UserName,
                        Role = model.RoleName
                    });
                }
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong.");
            }
            return BadRequest("Something went wrong.");
        }

        [HttpPost()]
        public async Task<IActionResult> GetToken(UserModel model)
        {
            try
            {
                User user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                    return BadRequest("This user does not exist");

                return await GenerateToken(user);
            }
            catch
            {
                return BadRequest(model);
            }
        }
    }
}
