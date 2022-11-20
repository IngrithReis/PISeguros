using System.ComponentModel.DataAnnotations;

namespace PISeguros.API.Models.Requests
{
    public class InserirCorretorRequest
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public decimal Corretagem { get; set; }

        [Required]
        public string Usuario { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}
