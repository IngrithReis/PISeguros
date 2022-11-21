using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PISeguros.API.Database;
using PISeguros.API.Models.DTOs;
using PISeguros.API.Models.Entities;
using PISeguros.API.Models.Requests;
using PISeguros.API.Models.Responses;

namespace PISeguros.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class SegurosController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public SegurosController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        [Authorize(Roles = "admin,corretor")]
        public async Task<ActionResult<BaseResponse<List<SeguroDTO>>>> Get()
        {
            var seguros = await _appDbContext.Seguros.ToListAsync();
            return Ok(new BaseResponse<List<SeguroDTO>>(seguros.Select(x => new SeguroDTO
            {
                Id = x.Id,
                Nome = x.Nome,
                VidasMinimas = x.VidasMinimas,
                MaxDependentes = x.MaxDependentes,
                ValorVida = x.ValorVida,
                IdadeMaximaSegurado = x.IdadeMaximaSegurado,
            }).ToList())
            );
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,corretor")]
        public async Task<ActionResult<SeguroDTO>> Get(int id)
        {
            var seguro = await _appDbContext.Seguros.FirstOrDefaultAsync(x => x.Id == id);

            if(seguro == null)
            {
                return NotFound();
            }

            return Ok(new SeguroDTO
            {
                Id = seguro.Id,
                Nome = seguro.Nome,
                VidasMinimas = seguro.VidasMinimas,
                MaxDependentes = seguro.MaxDependentes,
                ValorVida = seguro.ValorVida,
                IdadeMaximaSegurado = seguro.IdadeMaximaSegurado,
            });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Post(InserirSeguroRequest seguroDTO)
        {
            var seguro = new Seguro
            {
                Nome = seguroDTO.Nome,
                VidasMinimas = seguroDTO.VidasMinimas,
                ValorVida = seguroDTO.ValorVida,
                MaxDependentes = seguroDTO.MaxDependentes,
                IdadeMaximaSegurado =seguroDTO.IdadeMaximaSegurado
            };

            await _appDbContext.Seguros.AddAsync(seguro);
            await _appDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Post(SeguroDTO seguroDTO)
        {
            var seguro = await _appDbContext.Seguros.FirstOrDefaultAsync(x => x.Id == seguroDTO.Id);
           
            if (seguro == null)
            {
                return BadRequest(new ErroResponse("Seguro não localizado"));
            }

            seguro.Nome = seguroDTO.Nome;
            seguro.VidasMinimas = seguroDTO.VidasMinimas;
            seguro.MaxDependentes = seguroDTO.MaxDependentes;
            seguro.ValorVida = seguroDTO.ValorVida;
            seguro.IdadeMaximaSegurado = seguroDTO.IdadeMaximaSegurado;

            await _appDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var seguro = await _appDbContext.Seguros.FirstOrDefaultAsync(x => x.Id == id);
            if (seguro == null)
            {
                return BadRequest(new ErroResponse("Seguro não localizado"));
            }

             _appDbContext.Seguros.Remove(seguro);
            await _appDbContext.SaveChangesAsync();

            return Ok(seguro);

        }
    }
}
