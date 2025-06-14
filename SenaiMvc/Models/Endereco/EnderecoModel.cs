using System.ComponentModel.DataAnnotations;

namespace SenaiMvc.Models.Endereco
{
    public class EnderecoModel
    {
        public string Rua { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }
        public int Numero { get; set; }

    }
}
