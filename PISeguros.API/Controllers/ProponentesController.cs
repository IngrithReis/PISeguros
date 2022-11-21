using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PISeguros.API.Database;
using PISeguros.API.Models.DTOs;
using PISeguros.API.Models.Entities;
using PISeguros.API.Models.Requests;
using PISeguros.API.Models.Responses;
using System.ComponentModel;

namespace PISeguros.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]


    public class ProponentesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public ProponentesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        [Authorize(Roles = "admin,corretor")]
        public async Task<ActionResult<BaseResponse<List<ProponenteDTO>>>> Get()
        {
            var proponentes = await _appDbContext.Proponentes.ToListAsync();

            return Ok(new BaseResponse<List<ProponenteDTO>>(proponentes.Select(x => new ProponenteDTO
            {
                Id = x.Id,
                Nome = x.Nome,
                CNPJ = x.CNPJ,
                Email = x.Email,
                SeguroId = x.SeguroId,
                
            }).ToList())
           );
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,corretor")]
        public async Task<ActionResult<ProponenteDTO>> Get(int id)
        {
            var proponente = await _appDbContext.Proponentes
                .Include(x => x.Segurados)
                .ThenInclude(x => x.Dependentes)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (proponente == null)
            {
                return NotFound();
            }

            return Ok(new ProponenteDTO
            {
                Id = proponente.Id,
                Nome = proponente.Nome,
                CNPJ = proponente.CNPJ,
                Email = proponente.Email,
                SeguroId = proponente.SeguroId,
                Segurados = proponente.Segurados.Select(x => new SeguradoDTO
                {   
                    Id = x.Id,
                    Nome = x.Nome,
                    SobreNome = x.SobreNome,
                    Nascimento =x.Nascimento,
                    Dependentes = x.Dependentes.Select(d => new DependenteDTO
                    {
                        Id = d.Id,
                        SeguradoId =d.SeguradoId,
                        Nome = d.Nome,
                        DataNascimento = d.DataNascimento
                    }).ToList()

                }).ToList()
            });
        }
        [HttpPost]
        [Authorize(Roles = "admin,corretor")]
        public async Task<ActionResult> Post(InserirProponenteRequest proponenteDTO)
        {
            var proponente = new Proponente
            {
                Nome = proponenteDTO.Nome,
                CNPJ = proponenteDTO.CNPJ,
                Email = proponenteDTO.Email,
                SeguroId = proponenteDTO.SeguroId,

                Segurados = proponenteDTO.Segurados.Select(x => new Segurado
                {
                    Nome = x.Nome,
                    SobreNome = x.SobreNome,
                    Nascimento = x.Nascimento,

                    Dependentes = x.Dependentes.Select(d => new Dependente
                    {
                        Nome = d.Nome,
                        DataNascimento = d.DataNascimento
                    }).ToList()

                }).ToList()

            };

            await _appDbContext.Proponentes.AddAsync(proponente);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin,corretor")]
        public async Task<ActionResult> Put(ProponenteDTO proponenteDTO)
        {
            var proponente = await _appDbContext.Proponentes
                .Include(x => x.Segurados)
                .ThenInclude(x => x.Dependentes)
                .FirstOrDefaultAsync(x => x.Id == proponenteDTO.Id);

            if (proponente == null)
            {
                return NotFound();
            }

            proponente.Nome = proponenteDTO.Nome;
            proponente.CNPJ = proponenteDTO.CNPJ;
            proponente.Email = proponenteDTO.Email;
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }
        
            
    }
        

}

    

