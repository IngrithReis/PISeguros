using System.ComponentModel.DataAnnotations;

namespace PISeguros.API.Models.Entities
{
    public class Seguro
    {
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Nome { get; set; }
        public int VidasMinimas { get; set; }
        public int MaxDependentes { get; set; }
        public decimal ValorVida { get; set; }
    }
}
