using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoDAW_hemeroteca_MDK.Data;
using ProyectoDAW_hemeroteca_MDK.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoDAW_hemeroteca_MDK.Controllers
{
    public class CarrerasController : Controller
    {
        private readonly ProyectoDAW_hemeroteca_MDKContext _context;

        public CarrerasController(ProyectoDAW_hemeroteca_MDKContext context)
        {
            _context = context;
        }

        // GET: Carreras
        public async Task<IActionResult> Index(String sortOrder, string searchString)
        {
            ViewData["NombreSortParam"] = String.IsNullOrEmpty(sortOrder) ? "Nombre_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            ViewData["UserCount"] = _context.Carrera.Count();
            var categorias = from s in _context.Carrera select s;
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
            //return View(await _context.Carrera.ToListAsync());
        }

        // GET: Carreras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrera = await _context.Carrera
                .FirstOrDefaultAsync(m => m.IdCarrera == id);
            if (carrera == null)
            {
                return NotFound();
            }

            return View(carrera);
        }

        // GET: Carreras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carreras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCarrera,Nombre,IdFacultad")] Carrera carrera)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carrera);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carrera);
        }

        // GET: Carreras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrera = await _context.Carrera.FindAsync(id);
            if (carrera == null)
            {
                return NotFound();
            }
            return View(carrera);
        }

        // POST: Carreras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCarrera,Nombre,IdFacultad")] Carrera carrera)
        {
            if (id != carrera.IdCarrera)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carrera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarreraExists(carrera.IdCarrera))
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
            return View(carrera);
        }

        // GET: Carreras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrera = await _context.Carrera
                .FirstOrDefaultAsync(m => m.IdCarrera == id);
            if (carrera == null)
            {
                return NotFound();
            }

            return View(carrera);
        }

        // POST: Carreras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carrera = await _context.Carrera.FindAsync(id);
            _context.Carrera.Remove(carrera);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarreraExists(int id)
        {
            return _context.Carrera.Any(e => e.IdCarrera == id);
        }
    }
}
