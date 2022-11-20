using System.ComponentModel.DataAnnotations;

namespace PISeguros.API.Models.DTOs
{
    public class SeguroDTO
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }
        public int VidasMinimas { get; set; }
        public int MaxDependentes { get; set; }
        public decimal ValorVida { get; set; }
        public int IdadeMaximaSegurado { get; set; }
    }
}
