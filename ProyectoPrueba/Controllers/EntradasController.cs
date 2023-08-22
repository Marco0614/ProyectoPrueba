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
    public class EntradasController : Controller
    {
        private readonly ProyectoContext _context;

        public EntradasController(ProyectoContext context)
        {
            _context = context;
        }

        // GET: Entradas
        public async Task<IActionResult> Index()
        {
            var proyectoContext = _context.Entrada.Include(e => e.CategoriaEntrada).Include(e => e.Evento).Include(e => e.Usuarios);
            return View(await proyectoContext.ToListAsync());
        }


        // GET: Entradas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Entrada == null)
            {
                return NotFound();
            }

            var entradum = await _context.Entrada
                .Include(e => e.CategoriaEntrada)
                .Include(e => e.Evento)
                .Include(e => e.Usuarios)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entradum == null)
            {
                return NotFound();
            }

            return View(entradum);
        }

        // GET: Entradas/Create
        public IActionResult Create()
        {
            ViewData["CategoriaEntradaId"] = new SelectList(_context.CategoriaEntrada, "Id", "Id");
            ViewData["EventoId"] = new SelectList(_context.Eventos, "Id", "Id");
            ViewData["UsuariosId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: Entradas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EventoId,CategoriaEntradaId,UsuariosId")] Entradum entradum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entradum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaEntradaId"] = new SelectList(_context.CategoriaEntrada, "Id", "Id", entradum.CategoriaEntradaId);
            ViewData["EventoId"] = new SelectList(_context.Eventos, "Id", "Id", entradum.EventoId);
            ViewData["UsuariosId"] = new SelectList(_context.Usuarios, "Id", "Id", entradum.UsuariosId);
            return View(entradum);
        }

        // GET: Entradas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Entrada == null)
            {
                return NotFound();
            }

            var entradum = await _context.Entrada.FindAsync(id);
            if (entradum == null)
            {
                return NotFound();
            }
            ViewData["CategoriaEntradaId"] = new SelectList(_context.CategoriaEntrada, "Id", "Id", entradum.CategoriaEntradaId);
            ViewData["EventoId"] = new SelectList(_context.Eventos, "Id", "Id", entradum.EventoId);
            ViewData["UsuariosId"] = new SelectList(_context.Usuarios, "Id", "Id", entradum.UsuariosId);
            return View(entradum);
        }

        // POST: Entradas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventoId,CategoriaEntradaId,UsuariosId")] Entradum entradum)
        {
            if (id != entradum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entradum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntradumExists(entradum.Id))
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
            ViewData["CategoriaEntradaId"] = new SelectList(_context.CategoriaEntrada, "Id", "Id", entradum.CategoriaEntradaId);
            ViewData["EventoId"] = new SelectList(_context.Eventos, "Id", "Id", entradum.EventoId);
            ViewData["UsuariosId"] = new SelectList(_context.Usuarios, "Id", "Id", entradum.UsuariosId);
            return View(entradum);
        }

        // GET: Entradas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Entrada == null)
            {
                return NotFound();
            }

            var entradum = await _context.Entrada
                .Include(e => e.CategoriaEntrada)
                .Include(e => e.Evento)
                .Include(e => e.Usuarios)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entradum == null)
            {
                return NotFound();
            }

            return View(entradum);
        }

        // POST: Entradas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Entrada == null)
            {
                return Problem("Entity set 'ProyectoContext.Entrada'  is null.");
            }
            var entradum = await _context.Entrada.FindAsync(id);
            if (entradum != null)
            {
                _context.Entrada.Remove(entradum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntradumExists(int id)
        {
          return (_context.Entrada?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
