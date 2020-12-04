using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoDAW_hemeroteca_MDK.Data;
using ProyectoDAW_hemeroteca_MDK.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoDAW_hemeroteca_MDK.Controllers
{
    public class TipoProyectoesController : Controller
    {
        private readonly ProyectoDAW_hemeroteca_MDKContext _context;

        public TipoProyectoesController(ProyectoDAW_hemeroteca_MDKContext context)
        {
            _context = context;
        }

        // GET: TipoProyectoes
        public async Task<IActionResult> Index(String sortOrder, string searchString)
        {
            ViewData["NombreSortParam"] = String.IsNullOrEmpty(sortOrder) ? "Nombre_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            ViewData["UserCount"] = _context.TipoProyecto.Count();
            var categorias = from s in _context.TipoProyecto select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                categorias = categorias.Where(s => s.Nombre.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Nombre_desc":
                    categorias = categorias.OrderByDescending(s => s.Nombre);
                    break;
                default:
                    categorias = categorias.OrderBy(s => s.Nombre);
                    break;
            }
            return View(await categorias.AsNoTracking().ToListAsync());
            //return View(await _context.TipoProyecto.ToListAsync());
        }

        // GET: TipoProyectoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoProyecto = await _context.TipoProyecto
                .FirstOrDefaultAsync(m => m.IdTipoProyecto == id);
            if (tipoProyecto == null)
            {
                return NotFound();
            }

            return View(tipoProyecto);
        }

        // GET: TipoProyectoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoProyectoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoProyecto,Nombre")] TipoProyecto tipoProyecto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoProyecto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoProyecto);
        }

        // GET: TipoProyectoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoProyecto = await _context.TipoProyecto.FindAsync(id);
            if (tipoProyecto == null)
            {
                return NotFound();
            }
            return View(tipoProyecto);
        }

        // POST: TipoProyectoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoProyecto,Nombre")] TipoProyecto tipoProyecto)
        {
            if (id != tipoProyecto.IdTipoProyecto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoProyecto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoProyectoExists(tipoProyecto.IdTipoProyecto))
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
            return View(tipoProyecto);
        }

        // GET: TipoProyectoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoProyecto = await _context.TipoProyecto
                .FirstOrDefaultAsync(m => m.IdTipoProyecto == id);
            if (tipoProyecto == null)
            {
                return NotFound();
            }

            return View(tipoProyecto);
        }

        // POST: TipoProyectoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoProyecto = await _context.TipoProyecto.FindAsync(id);
            _context.TipoProyecto.Remove(tipoProyecto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoProyectoExists(int id)
        {
            return _context.TipoProyecto.Any(e => e.IdTipoProyecto == id);
        }
    }
}
