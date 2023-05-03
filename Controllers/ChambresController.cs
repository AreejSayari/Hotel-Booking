using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tuto.Data;
using tuto.Models;

namespace tuto.Controllers
{
    public class ChambresController : Controller
    {
        private readonly tutoContext _context;

        public ChambresController(tutoContext context)
        {
            _context = context;
        }

        // GET: Chambres
        public async Task<IActionResult> Index()
        {
              return _context.Chambre != null ? 
                          View(await _context.Chambre.ToListAsync()) :
                          Problem("Entity set 'tutoContext.Chambre'  is null.");
        }

        // GET: Chambres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Chambre == null)
            {
                return NotFound();
            }

            var chambre = await _context.Chambre
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chambre == null)
            {
                return NotFound();
            }

            return View(chambre);
        }

        // GET: Chambres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chambres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumeroChambre,Type,Prix")] Chambre chambre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chambre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chambre);
        }

        // GET: Chambres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Chambre == null)
            {
                return NotFound();
            }

            var chambre = await _context.Chambre.FindAsync(id);
            if (chambre == null)
            {
                return NotFound();
            }
            return View(chambre);
        }

        // POST: Chambres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumeroChambre,Type,Prix")] Chambre chambre)
        {
            if (id != chambre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chambre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChambreExists(chambre.Id))
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
            return View(chambre);
        }

        // GET: Chambres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Chambre == null)
            {
                return NotFound();
            }

            var chambre = await _context.Chambre
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chambre == null)
            {
                return NotFound();
            }

            return View(chambre);
        }

        // POST: Chambres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Chambre == null)
            {
                return Problem("Entity set 'tutoContext.Chambre'  is null.");
            }
            var chambre = await _context.Chambre.FindAsync(id);
            if (chambre != null)
            {
                _context.Chambre.Remove(chambre);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChambreExists(int id)
        {
          return (_context.Chambre?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> ReserverChambre(int idChambre)
        {
            Chambre chambre = new Chambre();
            chambre.Id = 1;
            chambre.NumeroChambre = 1 ;
            chambre.Prix = 300;
            chambre.Type = "familiale";
            var utilisateur = new Client
            {
                Id = 1,
                Firstname = "Areej",
                Lastname = "sayari",
                Email = "areej@gmail",
                Password = "password",
            };


            // Création de la réservation
            var reservation = new Reservation
            {
                IdChambre= chambre.Id,
                IdClient=utilisateur.Id,
                //Chambre = chambre,
                //Client = utilisateur,
                DateArrivee = DateTime.UtcNow,
                DateDepart= DateTime.Now.AddDays(2)

            };

            _context.Reservation.Add(reservation);
            await _context.SaveChangesAsync();

            //return Ok("Chambre réservée avec succès !");
            return View(reservation);
        }




    }
}
