using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PISeguros.API.Models.Entities
{
    public class Proponente
    {
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Nome { get; set; }

        [Required, MaxLength(14)]
        public string CNPJ { get; set; }
        public string Email { get; set; }
        public List<Segurado> Segurados { get; set; }

    }
}
