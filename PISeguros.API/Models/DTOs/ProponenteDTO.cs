using PISeguros.API.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace PISeguros.API.Models.DTOs
{
    public class ProponenteDTO
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }
        [Required]
        public string CNPJ { get; set; }
        public string Email { get; set; }

        public List<SeguradoDTO> Segurados { get; set; }
    }
}
