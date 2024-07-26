using Common;
using DataAcess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using RoyalVilla_API.Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;

namespace RoyalVilla_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly APISettings _aPISettings;

        public AccountController(SignInManager<ApplicationUser> signInManager,
             UserManager<ApplicationUser> userManager,
             RoleManager<IdentityRole> roleManager, IOptions<APISettings> options)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _aPISettings = options.Value;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] UserRequestDTO userRequestDTO)
        {
            if (userRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new ApplicationUser
            {
                UserName = userRequestDTO.Email,
                Email = userRequestDTO.Email,
                Name = userRequestDTO.Name,
                PhoneNumber = userRequestDTO.PhoneNo,
                EmailConfirmed = true
            };
            try
            {
                var result = await _userManager.CreateAsync(user, userRequestDTO.Password);

                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description);
                    return BadRequest(new RegistrationResponseDTO
                    { Errors = errors, IsRegistrationSuccessful = false });
                }
                var roleResult = await _userManager.AddToRoleAsync(user, SD.Role_Customer);
                if (!roleResult.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description);
                    return BadRequest(new RegistrationResponseDTO
                    { Errors = errors, IsRegistrationSuccessful = false });
                }
            }
            catch (Exception ex) { }
            return StatusCode(201);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] AuthenticationDTO authenticationDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(authenticationDTO.UserName, authenticationDTO.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(authenticationDTO.UserName);
                if (user == null)
                {
                    return Unauthorized(new AuthenticationResponseDTO
                    {
                        IsAuthSuccessful = false,
                        Errors = "Invalid Authentication"
                    });
                }

                var signingCredentials = GetSigningCredentials();
                var claims = await GetClaims(user);

                var tokenOptions = new JwtSecurityToken(
                  issuer: _aPISettings.ValidIssuer,
                  audience: _aPISettings.ValidAudience,
                  claims: claims,
                  expires: DateTime.Now.AddDays(30),
                  signingCredentials: signingCredentials);

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new AuthenticationResponseDTO
                {
                    IsAuthSuccessful = true,
                    Token = token,
                    UserDTO = new UserDTO
                    {
                        Name = user.Name,
                        Email = user.Email,
                        PhoneNo = user.PhoneNumber
                    }
                });
            }
            else
            {
                return Unauthorized(new AuthenticationResponseDTO
                {
                    IsAuthSuccessful = false,
                    Errors = "Invalid Authentication"
                });
            }
        }

        private SigningCredentials GetSigningCredentials()
        {
            var Secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_aPISettings.SecretKey));
            return new SigningCredentials(Secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                 new Claim(ClaimTypes.Name, user.Email),
                 new Claim(ClaimTypes.Email, user.Email),
                 new Claim("Id", user.Id),
            };
            var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(user.Email));


            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

    }
}
