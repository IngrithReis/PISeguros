using System.ComponentModel.DataAnnotations;

namespace PISeguros.API.Models.Entities
{
    public class Corretor
    {
        public int Id { get; set; }
        public string IdentityUserId { get; set; }

        [Required, MaxLength(255)]
        public string Nome { get; set; }

        public string Telefone { get; set; }
        public decimal Corretagem { get; set; }
    }
}
