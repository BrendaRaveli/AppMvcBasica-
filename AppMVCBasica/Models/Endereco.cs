using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppMVCBasica.Models
{
    public class Endereco : Entity
    //Eendereco herda de entidade
    //Endereco pertence a um fornecedor apenas com todos os dados a baixo.
    {
        public Guid FornecedorId { get; set; }

        [Required(ErrorMessage = " O campo {0} é obrigatorio")]
        [StringLength(200/*, ErrorMessage = "O campo {0} precisa ter entre {2) e {1} caracteres"*/, MinimumLength = 2)]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = " O campo {0} é obrigatorio")]
        [StringLength(50/*, ErrorMessage = "O campo {0} precisa ter entre {2) e {1} caracteres"*/, MinimumLength = 2)]
        public string Numero { get; set; }

        public string Complemento { get; set; }

        [Required(ErrorMessage = " O campo {0} é obrigatorio")]
        [StringLength(8/*, ErrorMessage = "O campo {0} precisa ter entre {2) e {1} caracteres"*/, MinimumLength = 8)]
        //O cep tem que ter 8 numeros. Então o maximo e minimo serão 8.
        public string Cep { get; set; }

        [Required(ErrorMessage = " O campo {0} é obrigatorio")]
        [StringLength(100/*, ErrorMessage = "O campo {0} precisa ter entre {2) e {1} caracteres"*/, MinimumLength = 2)]
        public string Bairro { get; set; }

        [Required(ErrorMessage = " O campo {0} é obrigatorio")]
        [StringLength(100/*, ErrorMessage = "O campo {0} precisa ter entre {2) e {1} caracteres"*/, MinimumLength = 2)]
        public string Cidade { get; set; }

        [Required(ErrorMessage = " O campo {0} é obrigatorio")]
        [StringLength(50/*, ErrorMessage = "O campo {0} precisa ter entre {2) e {1} caracteres"*/, MinimumLength = 2)]
        public string Estado { get; set; }

        public Fornecedor Fornecedor { get; set; }

    }
}
