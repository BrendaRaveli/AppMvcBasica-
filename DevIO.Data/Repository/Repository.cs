using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

//Sera uma classe generica. Ela so podera ser herdada, e não podera ser estanciada. Então sera obstract
namespace DevIO.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {  // Coloco o paramento gerenico TEntity, e irei implementar o IRepository de TEntity. Onde informo que TEntity(de IRepository<TEntity>)e filha de Entity. A TEntity de Repository não precisa ser filha de Entity, ja a TEntity de IRepository precisa. Fazendo isto, eu sou obrigada a implementar os metodos ja configurados.
        // A new e para informa que eu posso dar uma new nesta entity. Que esta sendo utilizada em remover.

        protected readonly MeuDbContext Db;
        // Para ter acesso ao DbContext.

        protected readonly DbSet<TEntity> DbSet;
        //Atalho para o dbset. Retorna uma instância não genérica DbSet para acesso a entidades do tipo fornecido no contexto e no repositório subjacente.


        protected Repository(MeuDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
            //Para funcionar este atalho eu preciso injeta-lo realizando o procedimento acima.

        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
            //Retorna uma nova consulta em que as entidades retornadas não serão armazenadas em cache no DbContext ou no ObjectContext , por ser um metodo assincrono sempre utilizo o await. O await ira esperar retorna alguma coisa.
            // Informo " Vá ate o banco de dados para aquela entidade especifica, onde a expressão que eu passar(predicate) retorna um lista assicrona
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
            // Vou ate o banco, localizo pelo id  e retorne a entidade informada.
            // O virtual é para que se caso eu precise implementar um override, seja possivel. O modificador override é necessário para estender ou modificar a implementação abstrata ou virtual de um método, propriedade, indexador ou evento herdado.
            // FindAsync = Localiza de forma assíncrona uma entidade com os valores de chave primária fornecidos. Se uma entidade com os valores de chave primária existentes existir no contexto, ela será retornada imediatamente sem fazer uma solicitação para o repositório. Caso contrário, uma solicitação é feita ao repositório para uma entidade com os valores de chave primária fornecidos e essa entidade, se encontrada, é anexada ao contexto e retornada. Se nenhuma entidade for encontrada no contexto ou na loja, NULL será retornado.

        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();
            // retorne uma lista de todos de TEntity

        }

        public virtual async Task Adicionar(TEntity entity)
        {        //Use o modificador async para especificar que um método, uma expressão lambda ou um método anônimo é assíncrono. Se você usar esse modificador em um método ou expressão, ele será referido como um método assíncrono.

            DbSet.Add(entity);
            await SaveChanges();
            //Adiciona a entidade fornecida ao contexto subjacente ao conjunto no estado adicionado, de modo que ele será inserido no banco de dados quando SaveChanges for chamado.
            //Você aplica o Await operador a um operando em um método assíncrono ou expressão lambda para suspender a execução do método até que a tarefa esperada seja concluída.A tarefa representa um trabalho em andamento.
            //O método no qual Await é usado deve ter um modificador assíncrono . Esse tipo de método, definido pelo uso do modificador Async e, geralmente, contendo uma ou mais expressões Await, é conhecido como um método assíncrono.
            //Normalmente, a tarefa à qual você aplica o Await operador é o valor de retorno de uma chamada para um método que implementa o padrão assíncrono baseado em tarefa, ou seja, um Task ou um Task<TResult>.

        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
            //Atualizo a entidade

        }

        public virtual async Task Remover(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
            // Informo que o Id da TEntity e meu id, e depois removo da entidade. Eu crio a instancia e informo que o id dele e o mesmo id que eu recebi

        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
            // Se eu precisar mexer em algum metodo saveChance, eu posso fazer no metodo especifico, não preciso mexer em todos os savechanges. Este metodo aqui foi criado pensando na usabilidade do codigo.

        }

        public void Dispose()
        {
            Db?.Dispose();
            //Se ele existir faça o dispose, se não existe, não faça.
            //Dispose() = Chama o método Dispose protegido.
        }
    }
}