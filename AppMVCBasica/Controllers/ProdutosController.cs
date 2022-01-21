using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppMVCBasica.Data;
using AppMVCBasica.Models;
using Microsoft.AspNetCore.Authorization;

namespace AppMVCBasica.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
       //Nesta forma simples, você vai injetar o controller no contexto e começa a seleciona seus modelos passando a view.
        private readonly ApplicationDbContext _context;

        public ProdutosController(ApplicationDbContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Produtos.Include(p => p.Fornecedor);
            // aqui seta um contexto de retorno da query. Pegou o contexto, os produtos e incluiu o fornecedor na relação. Para possui dados tanto de produto quanto fornecedor
            return View(await applicationDbContext.ToListAsync());
            //Aqui ele apresenta o retorno do contexto e chama o Tolist
            //O método ToList tem como objetivo materializar uma lista de elementos em memória. Para o método ser executado assincronamente deve ser chamado com await antes dele e o método que o chama precisa estar marcado como async.
        }
        [AllowAnonymous]
        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(p => p.Fornecedor)
                // Inclui novamente o fornecedor para te trazer informações completas.
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Nome");
            //Ambiente de transporte da controller para a view. A informação de uma view que você seta na controller.
            //A ViewData ira informa que "existe uma viewdata com o nome de fornecedorID, que é um objeto produto de uma estancia do selectlist, onde ele pega a lista de fornecedores e fala que o valor do dado é o id, e o texto da informação é o nome. 
            //ele entende que é uma lista que fornecedores e que você tem que escolher.
            return View();
        }

        // POST: Produtos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Produto produto)
        {
            if (ModelState.IsValid)
            {

                _context.Add(produto); //Ta adicionando via  contexto
                await _context.SaveChangesAsync(); // esta salvando com savechange
                return RedirectToAction(nameof(Index));
            }
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Nome", produto.FornecedorId);
            return View(produto);
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Nome", produto.FornecedorId);
            // se você informa o produto.fornecedorId no final ele ja vai seta o dropdownlist com o Id selecionado. Para saber exatamento qual você selecionou.
            return View(produto);
        }

        // POST: Produtos/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, /*[Bind("FornecedorId,Nome,Descricao,Imagem,Valor,DataCadastro,Ativo,Id")]*/ Produto produto)
            //Com a descrição comenta acima eu limito as informações que sua controller ira receber do form
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Nome", produto.FornecedorId);
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(p => p.Fornecedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(Guid id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}
