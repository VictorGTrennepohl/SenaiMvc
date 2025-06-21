using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using SenaiMvc.Models.Endereco;

namespace SenaiMvc.Models.Escola
{
    public class EscolaModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Campo Nome é obrigatorio")]
        public string Nome { get; set; }

        public EnderecoModel? Endereco { get; set; }

        public List<SelectListItem> Estados { get; set; }

        public List<SelectListItem> Cidades { get; set; }
    }
}
