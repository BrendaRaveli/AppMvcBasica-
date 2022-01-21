using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppMVCBasica.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppMVCBasica.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //Para entender que eu estou me referindo as classes de produto, fornecedor e endereço eu preciso importa o using da aplicação. -> using AppMVCBasica.Models;

        public DbSet<Produto> Produtos { get; set; }
        // O Dbset sera formado pelo nome da classe "Produto" e produtos que sera o nome da tabela.
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

    }
}


// Para criação das tabelas ou atualização de acordo com as migrations que possuo é necessario informar no package manage console o comando -> Update-Database -verbose
//Comando para: adicionar uma migration -> add-migration "nome" -Verbose . Remover -> Remove-Migration . Caso apresenter erro de acesso negado, séra necessario abrir o vs como ADM.