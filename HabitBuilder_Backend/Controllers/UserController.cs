using HabitBuilder_Backend.Areas.Identity.Data;
using HabitBuilder_Backend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HabitBuilder_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
 
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly JWTConfig _jwtConfig;
        public UserController( UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptions<JWTConfig> jwtConfig)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtConfig = jwtConfig.Value; 
        }

        [HttpPost("Register")]
        public async Task<object> Register([FromBody] UserInfo model)
        {
            var users = new AppUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,                
            };
            var result = await _userManager.CreateAsync(users, model.Password);

            if (result.Succeeded)
            {
                return await Task.FromResult("User has been Registered");
            }
            //If result does not succeed give description of errors.
            return await Task.FromResult(string.Join(",", result.Errors.Select(x => x.Description).ToArray()));
        }
    
        [HttpPost("Login")]
        public async Task<object> Login([FromBody] Login model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        var appUser = await _userManager.FindByEmailAsync(model.Email);
                        var user = new UserDTO(appUser.FirstName, appUser.LastName, appUser.Email, appUser.DateCreated);
                        user.Token = GenerateToken(appUser);
                        return await Task.FromResult(user);                    
                    }
                }
                return await Task.FromResult("Invalid Email or Password");
            }


            catch (Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }

        }

        //Get Profile Data/Delete/Update 
        [HttpGet("Profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var currentUser = await _userManager.GetUserAsync(User);      
            var users = await _userManager.FindByIdAsync(currentUser.Id);
            var user = new UserDTO(users.FirstName, users.LastName, users.Email, users.DateCreated);        
            return new OkObjectResult(user);
        }

        [HttpPut("Profile/Modify")]

        [Authorize]
        public async Task<IActionResult> PutProfile(AppUser appUser)
        {
           // var currentUser = await _userManager.GetUserAsync(User);

            var users = await _userManager.FindByIdAsync(appUser.Id);
            new UserDTO(users.FirstName, users.LastName, users.Email, users.DateCreated);
            var result = await _userManager.UpdateAsync(users);
            if (result.Succeeded)
            {
                return new OkObjectResult(users);

            }
            return new OkObjectResult("Failed to update");

        }

        //calculate points

        //Subscribe to other users/remove
        private string GenerateToken(AppUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.NameId, user.Id),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, user.Email),
                       
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),


                }),
                Expires = DateTime.UtcNow.AddHours(12), //token will expire after {} hours
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
                
                
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);

        }
    }
}