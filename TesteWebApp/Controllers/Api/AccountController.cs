using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TesteWebApp.App_Start;
using TesteWebApp.Models;
using TesteWebApp.ViewModels;

namespace TesteWebApp.Controllers.Api
{
    [ApiController]
    [Area("api")]
    [Route("[area]/[controller]")]
    public class AccountController : ControllerBase
    {
        public UserManager<AppUserModel> UserManager { get; set; }
        public SignInManager<AppUserModel> SignInManager { get; set; }
        public AccountController(UserManager<AppUserModel> userManager, SignInManager<AppUserModel> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] JwtTokenViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.UserName);
                var signInResult = await SignInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (signInResult.Succeeded)
                {
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppJwtTokens.Key));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, model.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, model.UserName)
                    };

                    var token = new JwtSecurityToken(
                        AppJwtTokens.Issuer,
                        AppJwtTokens.Audience,
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(20),
                        signingCredentials: creds
                        );

                    var results = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    };

                    return Created("", results);
                }
                return BadRequest();
            }
            return BadRequest();
        }
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    return NotFound(model);
                }
                var code = await UserManager.GeneratePasswordResetTokenAsync(user);
                return Ok(new { UserId = user.Id, user.UserName, user.Email, code});
            }
            return BadRequest(model);
        }
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            AppUserModel user = UserManager.FindByIdAsync(model.Id).Result;
            var result = UserManager.ResetPasswordAsync(user, model.Code, model.Password).Result;
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }
    }

}
