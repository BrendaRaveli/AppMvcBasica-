using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppMVCBasica.Models
{
    public class Fornecedor : Entity
    //herda de entidade

    {
        //Propriedades
        [Required(ErrorMessage = " O campo {0} é obrigatorio")]
        [StringLength(200, MinimumLength = 2)]
        //[StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2) e {1} caracteres", MinimumLength = 2)] ** ErrorMessage não esta funcionando **
        public string Nome { get; set; }

        [Required(ErrorMessage = " O campo {0} é obrigatorio")]
        [StringLength(14, MinimumLength = 11)]
        public string Documento { get; set; }


        public TipoFornecedor TipoFornecedor { get; set; }


        public Endereco Endereco { get; set; }
        //Relação direta com endereço

        [DisplayName("Ativo?")]
        //Esta é a forma como o MVC ira escrever o nome desta propriedade na tela. É necessario importa o using System.ComponentModel;
        public bool Ativo { get; set; }

        //Declaração da propriedades. Para o entity entender que e o fornecedor tem um relação de 1 pra muitos com produto.
        public IEnumerable<Produto> Produtos { get; set; }
    }
}
