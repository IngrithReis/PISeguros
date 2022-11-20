using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PISeguros.API.Models.DTOs;
using PISeguros.API.Models.Requests;
using PISeguros.API.Models.Responses;
using PISeguros.API.Services;
using System.Security.Claims;

namespace PISeguros.API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWTService _jwtService;

        public UsuariosController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, JWTService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Usuario);

            if (user == null)
                return BadRequest(new ErroResponse("Usuário ou senha incorretos"));

            if (!await _userManager.CheckPasswordAsync(user, request.Senha))
                return BadRequest(new ErroResponse("Usuário ou senha incorretos"));

            var claimsIdentity = new ClaimsIdentity();

            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));

            var userClaims = await _userManager.GetClaimsAsync(user);
            claimsIdentity.AddClaims(userClaims);

            var userRoles = await _userManager.GetRolesAsync(user);
            claimsIdentity.AddClaims(userRoles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

            foreach (var userRole in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(userRole);
                var roleClaims = await _roleManager.GetClaimsAsync(role);
                claimsIdentity.AddClaims(roleClaims);
            }

            var jwtToken = _jwtService.CreateToken(claimsIdentity);

            return Ok(new LoginResponse
            {
                Token = jwtToken.Token,
                Expires = jwtToken.Expires
            });
        }
    }
}