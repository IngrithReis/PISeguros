using System.ComponentModel.DataAnnotations;

namespace PISeguros.API.Models.Requests
{
    public class InserirSeguroRequest
    {
        [Required]
        public string Nome { get; set; }
        public int VidasMinimas { get; set; }
        public int MaxDependentes { get; set; }
        public decimal ValorVida { get; set; }
        public int IdadeMaximaSegurado { get; set; }
    }
}
