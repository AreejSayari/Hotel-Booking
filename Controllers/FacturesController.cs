using System;
using System.Collections;
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
    public class FacturesController : Controller
    {
        private readonly tutoContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public FacturesController(tutoContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        // GET: Factures
        public async Task<IActionResult> Index()
        {
            var tutoContext = _context.Facture.Include(f => f.Client);
            return View(await tutoContext.ToListAsync());
        }

        // GET: Factures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Facture == null)
            {
                return NotFound();
            }

            var facture = await _context.Facture
                .Include(f => f.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facture == null)
            {
                return NotFound();
            }

            return View(facture);
        }

        // GET: Factures/Create
        public IActionResult Create()
        {
            ViewData["IdClient"] = new SelectList(_context.Client, "Id", "Id");
            return View();
        }

        // POST: Factures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Montant,DateFacture,IdClient")] Facture facture)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdClient"] = new SelectList(_context.Client, "Id", "Id", facture.IdClient);
            return View(facture);
        }

        // GET: Factures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Facture == null)
            {
                return NotFound();
            }

            var facture = await _context.Facture.FindAsync(id);
            if (facture == null)
            {
                return NotFound();
            }
            ViewData["IdClient"] = new SelectList(_context.Client, "Id", "Id", facture.IdClient);
            return View(facture);
        }

        // POST: Factures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Montant,DateFacture,IdClient")] Facture facture)
        {
            if (id != facture.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FactureExists(facture.Id))
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
            ViewData["IdClient"] = new SelectList(_context.Client, "Id", "Id", facture.IdClient);
            return View(facture);
        }

        // GET: Factures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Facture == null)
            {
                return NotFound();
            }

            //var facture = await _context.Facture
            //    .Include(f => f.Reservation)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var facture = await _context.Facture
                .Include(f => f.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facture == null)
            {
                return NotFound();
            }

            return View(facture);
        }

        // POST: Factures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Facture == null)
            {
                return Problem("Entity set 'tutoContext.Facture'  is null.");
            }
            var facture = await _context.Facture.FindAsync(id);
            if (facture != null)
            {
                _context.Facture.Remove(facture);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FactureExists(int id)
        {
          return (_context.Facture?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        // GET: Factures/GenerFacture
        public async Task<IActionResult> GenerFacture()
        {
            var idCurrent = _contextAccessor.HttpContext.Session.GetInt32("UserId");
            var client = await _context.Client.Include(c => c.Reservations)
                                                .FirstOrDefaultAsync(c => c.Id == idCurrent);

            if (client == null)
            {
                return NotFound();
            }

            //// Récupérer les réservations associées au client
            var reservations = client.Reservations.ToList();

            float montant = 0;
            foreach (var reservation in reservations)
            {
                var chambre = _context.Chambre.FirstOrDefault(c => c.Id == reservation.IdChambre);
                if (chambre != null)
                {
                    var nombre = reservation.NbrChambres;
                    TimeSpan difference = reservation.DateDepart.Subtract(reservation.DateArrivee); // calcul de la différence entre les deux dates
                    int numberOfDays = difference.Days;
                    montant = montant + (nombre * chambre.Prix * numberOfDays);
                }
                //if (chambre != null)
                //{
                //    montant = montant + chambre.Prix;
                //    //montant += reservation.nombre * chambre.Prix;
                //}
                //else
                //{
                    
                //    montant = montant + chambre.Prix;
                //}
            }

            var facture = new Facture()
            {
                Client = client,
                IdClient = client.Id,
                DateFacture= DateTime.Now,
                Montant = (float)montant
            };
            var existingFacture = _context.Facture.FirstOrDefault(f => f.IdClient == facture.IdClient);

            if (existingFacture != null)
            {
                existingFacture = facture;
                _context.Update(existingFacture);
                await _context.SaveChangesAsync();
                ModelState.AddModelError("", "Une facture existe déjà pour ce client à cette date.");
                return View(facture);
            }
            else
            {
                _context.Add(facture);
                await _context.SaveChangesAsync();

            }

            
            return View(facture);
        }

        
    }
}
