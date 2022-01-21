using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppMVCBasica.Models
{
    //Esta é a classe mãe. Sera herdada por produto, endereço e fornecedor
    //Ela e abstract pois ela não deve ser estanciada, e sim herdada.
    public abstract class Entity
    {
        //Como sera herdada, esta classe deve ser protected
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
        //Este ID sera para todos
        public Guid Id { get; set; }
    }
}


