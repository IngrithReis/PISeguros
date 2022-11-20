using PISeguros.API.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace PISeguros.API.Models.DTOs
{
    public class SeguradoDTO
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nome { get; set; }

        [Required, MaxLength(100)]
        public string SobreNome { get; set; }

        [Required]
        public DateTime Nascimento { get; set; }

        public List<DependenteDTO> Dependentes { get; set; }
    }
}
