using PISeguros.API.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace PISeguros.API.Models.Requests
{
    public class InserirProponenteRequest
    {
        [Required, MaxLength]
        public string Nome { get; set; }

        [Required, MaxLength]
        public string CNPJ { get; set; }

        [Required, MaxLength]
        public string Email { get; set; }
        public int? SeguroId { get; set; }
        public List<SeguradoDTO> Segurados { get; set; }
    }
}
