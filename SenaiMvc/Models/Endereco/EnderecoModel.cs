using System.ComponentModel.DataAnnotations;

namespace SenaiMvc.Models.Endereco
{
    public class EnderecoModel
    {
        public long? Id { get; set; }

        [Required(ErrorMessage = "Campo Rua é Obrigatorio")]
        public string Rua { get; set; }

        [Required(ErrorMessage = "Campo Bairro é Obrigatorio")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Campo Cidade é Obrigatorio")]
        public string Cidade { get; set; }


        [Required(ErrorMessage = "Campo Estado é Obrigatorio"), MaxLength(2)]
        public string Estado { get; set; }

        [Required(ErrorMessage = "Campo Número é Obrigatorio")]
        public int Numero { get; set; }

    }
}
