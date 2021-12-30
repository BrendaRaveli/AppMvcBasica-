using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppMVCBasica.Models
{
    public class Produto :Entity
        //herda de entidade
    {
        public Guid FornecedorId { get; set; }
        //Foreign Key, Chave estrangeira, para informa que este produto pertence a um fornecedor.O forneceder é identificado atraves do id, e este Id e herdado da classe entity.
    

        [Required(ErrorMessage = " O campo {0} é obrigatorio")]
        //Utilizo o required para exigencia de preenchimento. Para utilizar o Required preciso importa o using System.ComponentModel.DataAnnotations;
        //Utilizo o {0} para interpolação, onde o 0 e o nome do campo. Neste caso o sistema ira substituir o {0} por Nome.
        [StringLength(200, MinimumLength =2)]
        //O tamanho minomo e maximo de preenchimento. Para interpolação o campo nome {0} é o primeiro paramentro, 200 é o {1} e 2 e {2}.
        public string Nome { get; set; }

        [Required(ErrorMessage = " O campo {0} é obrigatorio")]
        [StringLength(1000, /*ErrorMessage = "O campo {0} precisa ter entre {2) e {1} caracteres",*/ MinimumLength = 2)]
        public string Descricao { get; set; }

        [Required(ErrorMessage = " O campo {0} é obrigatorio")]
        public string Imagem { get; set; }

        [Required(ErrorMessage = " O campo {0} é obrigatorio")]
        public decimal Valor { get; set; }

        public DateTime DataCadastro { get; set; }
        //A data cadastro não sera requerida pois sera preenchida no momento do cadastro no banco

        public bool Ativo { get; set; }

        //Estabelecendo um relacionamento visivel de um para muito para o entity. 1 para N.
        //Dizendo ao entity que " Eu produto tenho uma relação com o fornecedor, e o fornecedor tem um relação comigo. Este fornecedor tem muitos produtos, e estes produtos tem apenas um fornecedor.
   
        public Fornecedor Fornecedor { get; set; }
    }
}
