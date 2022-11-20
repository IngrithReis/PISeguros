using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.ComponentModel.DataAnnotations;

namespace PISeguros.API.Models.Entities
{
    public class Dependente
    {
        public int Id { get; set; }
        public int SeguradoId { get; set; }

        [Required, MaxLength(100)]
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public Segurado Segurado { get; set; }
    }
}
