using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.ViewModels;
using MyCvProject.Domain.Consts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyCms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] LoginViewModel user)
        {
            if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest();
            }

            var login = await _userService.LoginUser(user);
            if (login == null)
            {
                return Unauthorized();
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Const.VerifyCodeJwt));

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOption = new JwtSecurityToken(
                issuer: "https://localhost:" + HttpContext.Connection.LocalPort,
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.Name,login.Email),
                    new Claim(ClaimTypes.NameIdentifier,login.UserId.ToString()),
                },
                expires: DateTime.Now.AddDays(7),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOption);

            return Ok(new AccessToken() { Token = tokenString });
        }
    }

    public class AccessToken
    {
        public string Token { get; set; }
    }
}
