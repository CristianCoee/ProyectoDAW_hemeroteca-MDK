using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoDAW_hemeroteca_MDK.Data;
using ProyectoDAW_hemeroteca_MDK.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoDAW_hemeroteca_MDK.Controllers
{
    public class FacultadsController : Controller
    {
        private readonly ProyectoDAW_hemeroteca_MDKContext _context;

        public FacultadsController(ProyectoDAW_hemeroteca_MDKContext context)
        {
            _context = context;
        }

        // GET: Facultads
        public async Task<IActionResult> Index(String sortOrder, string searchString)
        {
            ViewData["NombreSortParam"] = String.IsNullOrEmpty(sortOrder) ? "Nombre_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            ViewData["UserCount"] = _context.Facultad.Count();
            var categorias = from s in _context.Facultad select s;
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
            //return View(await _context.Facultad.ToListAsync());
        }

        // GET: Facultads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultad = await _context.Facultad
                .FirstOrDefaultAsync(m => m.IdFacultad == id);
            if (facultad == null)
            {
                return NotFound();
            }

            return View(facultad);
        }

        // GET: Facultads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Facultads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFacultad,Nombre")] Facultad facultad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facultad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facultad);
        }

        // GET: Facultads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultad = await _context.Facultad.FindAsync(id);
            if (facultad == null)
            {
                return NotFound();
            }
            return View(facultad);
        }

        // POST: Facultads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFacultad,Nombre")] Facultad facultad)
        {
            if (id != facultad.IdFacultad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facultad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultadExists(facultad.IdFacultad))
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
            return View(facultad);
        }

        // GET: Facultads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultad = await _context.Facultad
                .FirstOrDefaultAsync(m => m.IdFacultad == id);
            if (facultad == null)
            {
                return NotFound();
            }

            return View(facultad);
        }

        // POST: Facultads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facultad = await _context.Facultad.FindAsync(id);
            _context.Facultad.Remove(facultad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacultadExists(int id)
        {
            return _context.Facultad.Any(e => e.IdFacultad == id);
        }
    }
}
