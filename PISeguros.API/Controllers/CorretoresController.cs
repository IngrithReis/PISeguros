using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PISeguros.API.Database;
using PISeguros.API.Models.DTOs;
using PISeguros.API.Models.Entities;
using PISeguros.API.Models.Requests;
using PISeguros.API.Models.Responses;

namespace PISeguros.API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Authorize(Roles = "admin")]
    public class CorretoresController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CorretoresController(AppDbContext appDbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<List<CorretorDTO>>>> Get()
        {
            var corretores = await _appDbContext.Corretores.ToListAsync();

            return Ok(new  BaseResponse<List<CorretorDTO>>(corretores.Select(x => new CorretorDTO
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Telefone = x.Telefone,
                    Corretagem = x.Corretagem
                }).ToList())
            );
        }

        [HttpPost]
        public async Task<ActionResult> Post(InserirCorretorRequest corretorDTO)
        {
            var role = await _roleManager.FindByNameAsync("corretor");
            if (role == null)
                await _roleManager.CreateAsync(new IdentityRole("corretor"));

            var corretor = new Corretor
            {
                Nome = corretorDTO.Nome,
                Telefone = corretorDTO.Telefone,
                Corretagem = corretorDTO.Corretagem,
            };

            _appDbContext.Corretores.Add(corretor);

            var createResult = await _userManager.CreateAsync(new IdentityUser {
                    UserName = corretorDTO.Usuario,
                    Email = corretorDTO.Email
                },
                corretorDTO.Senha
            );
            
            if (!createResult.Succeeded)
            {
                return BadRequest(new ErroResponse(createResult.Errors.Select(x => x.Description)));
            }

            var user = await _userManager.FindByNameAsync(corretorDTO.Usuario);
            corretor.IdentityUserId = user.Id;
            await _appDbContext.SaveChangesAsync();

            await _userManager.AddToRoleAsync(user, "corretor");

            return Ok();
        }
    }
}