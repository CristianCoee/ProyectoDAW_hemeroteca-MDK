using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoDAW_hemeroteca_MDK.Data;
using ProyectoDAW_hemeroteca_MDK.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoDAW_hemeroteca_MDK.Controllers
{
    public class ProyectoesController : Controller
    {
        private readonly ProyectoDAW_hemeroteca_MDKContext _context;

        public ProyectoesController(ProyectoDAW_hemeroteca_MDKContext context)
        {
            _context = context;
        }

      
        // GET: Proyectoes
        public async Task<IActionResult> Index(String sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["UserCount"] = _context.TipoProyecto.Count();
            ViewData["AñoSortParam"] = String.IsNullOrEmpty(sortOrder) ? "Año_desc" : "";
            ViewData["TituloSortParam"] = sortOrder == "Titulo_asc" ? "Titulo_desc" : "Titulo_asc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;
            var categorias = from s in _context.Proyecto select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                categorias = categorias.Where(s => s.Titulo.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Año_desc":
                    categorias = categorias.OrderByDescending(s => s.Año);
                    break;
                case "Titulo_desc":
                    categorias = categorias.OrderByDescending(s => s.Titulo);
                    break;
                case "Titulo_asc":
                    categorias = categorias.OrderBy(s => s.Titulo);
                    break;
                default:
                    categorias = categorias.OrderBy(s => s.Año);
                    break;
            }
           // int pageSize = 10;
          //  return View(await Paginacion<Proyecto>.CreateAsync(categorias.AsNoTracking()));
            return View(await categorias.AsNoTracking().ToListAsync());
            //return View(await _context.Proyecto.ToListAsync());
        }

        // GET: Proyectoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyecto
                .FirstOrDefaultAsync(m => m.IdProyecto == id);
            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        // GET: Proyectoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proyectoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProyecto,IdTipoProyecto,IdAutor,Año,Titulo")] Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proyecto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proyecto);
        }

        // GET: Proyectoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyecto.FindAsync(id);
            if (proyecto == null)
            {
                return NotFound();
            }
            return View(proyecto);
        }

        // POST: Proyectoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProyecto,IdTipoProyecto,IdAutor,Año,Titulo")] Proyecto proyecto)
        {
            if (id != proyecto.IdProyecto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proyecto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProyectoExists(proyecto.IdProyecto))
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
            return View(proyecto);
        }

        // GET: Proyectoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyecto
                .FirstOrDefaultAsync(m => m.IdProyecto == id);
            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        // POST: Proyectoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proyecto = await _context.Proyecto.FindAsync(id);
            _context.Proyecto.Remove(proyecto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProyectoExists(int id)
        {
            return _context.Proyecto.Any(e => e.IdProyecto == id);
        }
    }
}
