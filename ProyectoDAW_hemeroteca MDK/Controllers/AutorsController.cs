using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoDAW_hemeroteca_MDK.Data;
using ProyectoDAW_hemeroteca_MDK.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoDAW_hemeroteca_MDK.Controllers
{
    public class AutorsController : Controller
    {
        private readonly ProyectoDAW_hemeroteca_MDKContext _context;

        public AutorsController(ProyectoDAW_hemeroteca_MDKContext context)
        {
            _context = context;
        }

        // GET: Autors
        public async Task<IActionResult> Index(String sortOrder, string searchString)
        {
            //Ordener autores de forma decendiente de nombre, apellido, carnet y sexo
            //Se ordenara de forma inversa a como esta en los registros de la tabla. 
            ViewData["NombreSortParam"] = String.IsNullOrEmpty(sortOrder) ? "Nombre_desc" : "";
            ViewData["ApellidoSortParam"] = sortOrder == "Apellido_asc" ? "Apellido_desc" : "Apellido_asc";
            ViewData["CarnetSortParam"] = sortOrder == "Carnet_asc" ? "Carnet_desc" : "Carnet_asc";
            ViewData["SexoSortParam"] = sortOrder == "Sexo_asc" ? "Sexo_desc" : "Sexo_asc";
            //Para el filtro y buscar
            ViewData["CurrentFilter"] = searchString;
            //Sentencia para contar y conocer el total de registros de la tabla. 
            ViewData["UserCount"] = _context.Autor.Count();
          //  ViewData["UserCount1"] = ;
            var categorias = from s in _context.Autor select s;
            if (!String.IsNullOrEmpty(searchString))

            {
                //Buscar por nombre, apelldo y sexo
                categorias = categorias.Where(s => s.Nombre.Contains(searchString) | s.Apellido.Contains(searchString) | s.Sexo.Contains(searchString));
            }
            switch (sortOrder)
            {
                //Continuacion para ordenar y configuarar bien el ordenamiento. 
                case "Nombre_desc":
                    categorias = categorias.OrderByDescending(s => s.Nombre);
                    break;
                case "Apellido_desc":
                    categorias = categorias.OrderByDescending(s => s.Apellido);
                    break;
                case "Apellido_asc":
                    categorias = categorias.OrderBy(s => s.Apellido);
                    break;
                case "Sexo_desc":
                    categorias = categorias.OrderByDescending(s => s.Sexo);
                    break;
                case "Sexo_asc":
                    categorias = categorias.OrderBy(s => s.Sexo);
                    break;
                case "Carnet_desc":
                    categorias = categorias.OrderByDescending(s => s.Carnet);
                    break;
                case "Carnet_asc":
                    categorias = categorias.OrderBy(s => s.Carnet);
                    break;
                default:
                    categorias = categorias.OrderBy(s => s.Nombre);
                    break;
            }
            //Actualizar la tabla
            return View(await categorias.AsNoTracking().ToListAsync());
            //return View(await _context.Autor.ToListAsync());
        }

        // GET: Autors/Details/5
        //Muestra el detalle del autor seleccionado, recibiendo el id como referencia. 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.Autor
                .FirstOrDefaultAsync(m => m.IdAutor == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // GET: Autors/Create
        //Comando para crear nuevo registro
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAutor,Nombre,Apellido,Carnet,IdCarrera,Sexo")] Autor autor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(autor);
        }

        // GET: Autors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.Autor.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }

        // POST: Autors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Comando o metodo para editar registro de la tabla
        public async Task<IActionResult> Edit(int id, [Bind("IdAutor,Nombre,Apellido,Carnet,IdCarrera,Sexo")] Autor autor)
        {
            if (id != autor.IdAutor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorExists(autor.IdAutor))
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
            return View(autor);
        }

        // GET: Autors/Delete/5
        //Comando para eliminar registro de la tabla
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.Autor
                .FirstOrDefaultAsync(m => m.IdAutor == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // POST: Autors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //Parte donde se mostrara herramienta de confirmacion
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autor = await _context.Autor.FindAsync(id);
            _context.Autor.Remove(autor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutorExists(int id)
        {
            return _context.Autor.Any(e => e.IdAutor == id);
        }
    }
}
