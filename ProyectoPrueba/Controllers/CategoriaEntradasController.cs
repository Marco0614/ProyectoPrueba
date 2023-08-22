using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoPrueba.Models;

namespace ProyectoPrueba.Controllers
{
    public class CategoriaEntradasController : Controller
    {
        private readonly ProyectoContext _context;

        public CategoriaEntradasController(ProyectoContext context)
        {
            _context = context;
        }

        // GET: CategoriaEntradas
        public async Task<IActionResult> Index()
        {
              return _context.CategoriaEntrada != null ? 
                          View(await _context.CategoriaEntrada.ToListAsync()) :
                          Problem("Entity set 'ProyectoContext.CategoriaEntrada'  is null.");
        }

        // GET: CategoriaEntradas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CategoriaEntrada == null)
            {
                return NotFound();
            }

            var categoriaEntradum = await _context.CategoriaEntrada
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaEntradum == null)
            {
                return NotFound();
            }

            return View(categoriaEntradum);
        }

        // GET: CategoriaEntradas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriaEntradas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Precio,Cantidad")] CategoriaEntradum categoriaEntradum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriaEntradum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaEntradum);
        }

        // GET: CategoriaEntradas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CategoriaEntrada == null)
            {
                return NotFound();
            }

            var categoriaEntradum = await _context.CategoriaEntrada.FindAsync(id);
            if (categoriaEntradum == null)
            {
                return NotFound();
            }
            return View(categoriaEntradum);
        }

        // POST: CategoriaEntradas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Precio,Cantidad")] CategoriaEntradum categoriaEntradum)
        {
            if (id != categoriaEntradum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriaEntradum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaEntradumExists(categoriaEntradum.Id))
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
            return View(categoriaEntradum);
        }

        // GET: CategoriaEntradas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CategoriaEntrada == null)
            {
                return NotFound();
            }

            var categoriaEntradum = await _context.CategoriaEntrada
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaEntradum == null)
            {
                return NotFound();
            }

            return View(categoriaEntradum);
        }

        // POST: CategoriaEntradas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CategoriaEntrada == null)
            {
                return Problem("Entity set 'ProyectoContext.CategoriaEntrada'  is null.");
            }
            var categoriaEntradum = await _context.CategoriaEntrada.FindAsync(id);
            if (categoriaEntradum != null)
            {
                _context.CategoriaEntrada.Remove(categoriaEntradum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaEntradumExists(int id)
        {
          return (_context.CategoriaEntrada?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
