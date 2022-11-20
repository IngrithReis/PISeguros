using System.ComponentModel.DataAnnotations;

namespace PISeguros.API.Models.Entities
{
    public class Segurado
    {
        public int Id { get; set; }
        public int ProponenteId { get; set; }

        [Required, MaxLength(100)]
        public string Nome { get; set; }

        [Required, MaxLength(100)]
        public string SobreNome { get; set; }

        [Required]
        public DateTime Nascimento { get; set; }

        public Proponente Proponete { get; set; }
        public List<Dependente> Dependentes { get; set; }

    }
}
