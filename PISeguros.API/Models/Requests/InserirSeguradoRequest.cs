using PISeguros.API.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace PISeguros.API.Models.Requests
{
    public class InserirSeguradoRequest
    {
        public int ProponenteId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string SobreNome { get; set; }

        [Required]
        public DateTime Nascimento { get; set; }
    }
}
