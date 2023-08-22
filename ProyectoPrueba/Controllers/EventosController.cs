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
    public class EventosController : Controller
    {
        private readonly ProyectoContext _context;

        public EventosController(ProyectoContext context)
        {
            _context = context;
        }

        // GET: Eventos
        public async Task<IActionResult> Index()
        {
              return _context.Eventos != null ? 
                          View(await _context.Eventos.ToListAsync()) :
                          Problem("Entity set 'ProyectoContext.Eventos'  is null.");
        }

        // GET: Eventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Eventos == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Eventos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descripcion,Fecha,Ubicacion")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                //Se valida que no hayan eventos el mismo dia
                bool existeEventoMismaFecha = await _context.Eventos.AnyAsync(e => e.Fecha.Date == evento.Fecha.Date);

                if (existeEventoMismaFecha)
                {
                    ModelState.AddModelError("", "Ya existe un evento en la misma fecha.");
                    return View(evento);
                }


                _context.Add(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(evento);
        }

        // GET: Eventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Eventos == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            return View(evento);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descripcion,Fecha,Ubicacion")] Evento evento)
        {
            if (id != evento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //bool existeEventoMismaFecha = await _context.Eventos
                //.Where(e => e.Id != id)
                //.AnyAsync(e => e.Fecha.Date == evento.Fecha.Date);

                //if (existeEventoMismaFecha)
                //{
                //    ModelState.AddModelError("", "Ya existe un evento en la misma fecha.");
                //    return View(evento);
                //}
                try
                {
                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.Id))
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
            return View(evento);
        }

        // GET: Eventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Eventos == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Eventos == null)
            {
                return Problem("Entity set 'ProyectoContext.Eventos'  is null.");
            }
            var evento = await _context.Eventos.FindAsync(id);
            if (evento != null)
            {
                _context.Eventos.Remove(evento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
          return (_context.Eventos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
