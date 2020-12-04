using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoDAW_hemeroteca_MDK.Data;
using ProyectoDAW_hemeroteca_MDK.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoDAW_hemeroteca_MDK.Controllers
{
    public class AutorPorProyectoesController : Controller
    {
        private readonly ProyectoDAW_hemeroteca_MDKContext _context;

        public AutorPorProyectoesController(ProyectoDAW_hemeroteca_MDKContext context)
        {
            _context = context;
        }

        // GET: AutorPorProyectoes
        public async Task<IActionResult> Index()
        {
            ViewData["UserCount"] = _context.AutorPorProyecto.Count();
            return View(await _context.AutorPorProyecto.ToListAsync());
        }

        
        // GET: AutorPorProyectoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorPorProyecto = await _context.AutorPorProyecto
                .FirstOrDefaultAsync(m => m.IdAutorProyecto == id);
            if (autorPorProyecto == null)
            {
                return NotFound();
            }

            return View(autorPorProyecto);
        }

        // GET: AutorPorProyectoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AutorPorProyectoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAutorProyecto,IdProyecto,IdAutor")] AutorPorProyecto autorPorProyecto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autorPorProyecto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(autorPorProyecto);
        }

        // GET: AutorPorProyectoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorPorProyecto = await _context.AutorPorProyecto.FindAsync(id);
            if (autorPorProyecto == null)
            {
                return NotFound();
            }
            return View(autorPorProyecto);
        }

        // POST: AutorPorProyectoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAutorProyecto,IdProyecto,IdAutor")] AutorPorProyecto autorPorProyecto)
        {
            if (id != autorPorProyecto.IdAutorProyecto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autorPorProyecto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorPorProyectoExists(autorPorProyecto.IdAutorProyecto))
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
            return View(autorPorProyecto);
        }

        // GET: AutorPorProyectoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorPorProyecto = await _context.AutorPorProyecto
                .FirstOrDefaultAsync(m => m.IdAutorProyecto == id);
            if (autorPorProyecto == null)
            {
                return NotFound();
            }

            return View(autorPorProyecto);
        }

        // POST: AutorPorProyectoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autorPorProyecto = await _context.AutorPorProyecto.FindAsync(id);
            _context.AutorPorProyecto.Remove(autorPorProyecto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutorPorProyectoExists(int id)
        {
            return _context.AutorPorProyecto.Any(e => e.IdAutorProyecto == id);
        }
    }
}
