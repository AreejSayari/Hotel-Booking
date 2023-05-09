using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tuto.Data;
using tuto.Models;

namespace tuto.Controllers
{
    public class TypeChambresController : Controller
    {
        private readonly tutoContext _context;

        public TypeChambresController(tutoContext context)
        {
            _context = context;
        }

        // GET: TypeChambres
        public async Task<IActionResult> Index()
        {
              return _context.TypeChambre != null ? 
                          View(await _context.TypeChambre.ToListAsync()) :
                          Problem("Entity set 'tutoContext.TypeChambre'  is null.");
        }

        // GET: TypeChambres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TypeChambre == null)
            {
                return NotFound();
            }

            var typeChambre = await _context.TypeChambre
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeChambre == null)
            {
                return NotFound();
            }

            return View(typeChambre);
        }

        // GET: TypeChambres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeChambres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TypeChambre typeChambre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeChambre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeChambre);
        }

        // GET: TypeChambres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TypeChambre == null)
            {
                return NotFound();
            }

            var typeChambre = await _context.TypeChambre.FindAsync(id);
            if (typeChambre == null)
            {
                return NotFound();
            }
            return View(typeChambre);
        }

        // POST: TypeChambres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TypeChambre typeChambre)
        {
            if (id != typeChambre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeChambre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeChambreExists(typeChambre.Id))
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
            return View(typeChambre);
        }

        // GET: TypeChambres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TypeChambre == null)
            {
                return NotFound();
            }

            var typeChambre = await _context.TypeChambre
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeChambre == null)
            {
                return NotFound();
            }

            return View(typeChambre);
        }

        // POST: TypeChambres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TypeChambre == null)
            {
                return Problem("Entity set 'tutoContext.TypeChambre'  is null.");
            }
            var typeChambre = await _context.TypeChambre.FindAsync(id);
            if (typeChambre != null)
            {
                _context.TypeChambre.Remove(typeChambre);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeChambreExists(int id)
        {
          return (_context.TypeChambre?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
