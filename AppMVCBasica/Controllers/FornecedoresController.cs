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

//Primeira tela. Controllers -> adicionar -> controler

namespace AppMVCBasica.Controllers
{
    [Authorize]
    public class FornecedoresController : Controller
    {
        private readonly ApplicationDbContext _context;
        // Esta controller ja declara um DbContext

        public FornecedoresController(ApplicationDbContext context)
            // Injeta via construtor
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: Fornecedores. Returna todos os fornecedores de forma assincrona
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fornecedores.ToListAsync());
        }

        [AllowAnonymous]
        // GET: Fornecedores/Details/5 Retorna com detalhes
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            return View(fornecedor);
        }

        // GET: Fornecedores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fornecedores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Fornecedor fornecedor)
        {//recebe o fornecedor
            if (ModelState.IsValid)
            {
                //faz a validação e adiciona ao contexto
                _context.Add(fornecedor);

                // Salva de forma assíncrona todas as alterações feitas neste contexto no banco de dados subjacente.
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fornecedor);
        }

        // GET: Fornecedores/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
                //Verifica se não foi informado o id nulo. Em caso de positivo e apresentado erro notFound
            }
            // Iremos obter o fornecer atraves do ID ,findAsync
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor == null)
            {
                return NotFound();
            }
            return View(fornecedor);
            //Verifica se não foi informado o fornecedor nulo

        }

        // POST: Fornecedores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Fornecedor fornecedor)
        {
            if (id != fornecedor.Id)
            // Compara se os dois id são iguais. Se o Id informado e equivalente ao mesmo id informado no formulario. 
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fornecedor);
                    await _context.SaveChangesAsync();
                    // Faz a validação e tenta salvar no banco.
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FornecedorExists(fornecedor.Id))
                        // ira fazer o teste se este fornecedor existe no banco ou não.
                    {
                        return NotFound();
                        // caso não exista. NotFound
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fornecedor);
        }

        // GET: Fornecedores/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // ira devolver esta informação do fornecedor
            var fornecedor = await _context.Fornecedores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            return View(fornecedor);
        }

        // POST: Fornecedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            //Ira excluir este fornecedor 
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            _context.Fornecedores.Remove(fornecedor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FornecedorExists(Guid id)
        {
            return _context.Fornecedores.Any(e => e.Id == id);
        }
    }
}
