using System.ComponentModel.DataAnnotations;

namespace PISeguros.API.Models.DTOs
{
    public class DependenteDTO
    {
        public int Id { get; set; }
        public int SeguradoId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }
    }
}
