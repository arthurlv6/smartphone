using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using APIModel;
using DataModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Web.API.Controllers
{
  public class AuthController : Controller
  {
    private UserDbContext _context;
    private ILogger<AuthController> _logger;
    private SignInManager<ApplicationUser> _signInMgr;
    private UserManager<ApplicationUser> _userMgr;
    private IPasswordHasher<ApplicationUser> _hasher;
    private IConfigurationRoot _config;

    public AuthController(UserDbContext context, 
      SignInManager<ApplicationUser> signInMgr,
      UserManager<ApplicationUser> userMgr, 
      IPasswordHasher<ApplicationUser> hasher,
      ILogger<AuthController> logger,
      IConfigurationRoot config)
    {
      _context = context;
      _signInMgr = signInMgr;
      _logger = logger;
      _userMgr = userMgr;
      _hasher = hasher;
      _config = config;
    }

    [HttpPost("api/auth/token")]
    public async Task<IActionResult> CreateToken([FromBody] CredentialModel model)
    {
      try
      {
        var user = await _userMgr.FindByNameAsync(model.UserName);
        if (user != null)
        {
          if (_hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
          {
            var userClaims = await _userMgr.GetClaimsAsync(user);

            var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
              new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("FooBarQuuxIsTheStandardTypeOfStringWeUse12345"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              issuer: _config["Tokens:Issuer"],
              audience: _config["Tokens:Audience"],
              claims: claims,
              expires: DateTime.UtcNow.AddDays(300),
              signingCredentials: creds
              );

            return Ok(new
            {
              token = new JwtSecurityTokenHandler().WriteToken(token),
              expiration = token.ValidTo
            });
          }
        }

      }
      catch (Exception ex)
      {
        _logger.LogError($"Exception thrown while creating JWT: {ex}");
      }

      return BadRequest("Failed to generate token");
    }
  }
}
