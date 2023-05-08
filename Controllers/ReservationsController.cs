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
    public class ReservationsController : Controller
    {
        private readonly tutoContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public ReservationsController(tutoContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            return _context.Reservation != null ?
                        View(await _context.Reservation.ToListAsync()) :
                        Problem("Entity set 'tutoContext.Reservation'  is null.");
        }

        // GET: 
        public async Task<IActionResult> VerifierDispo(int idChambre)
        {

            if (idChambre == null)
            {
                return NotFound();
            }

            var chambre = await _context.Chambre.FirstOrDefaultAsync(m => m.Id == idChambre);
            if (chambre == null)
            {
                return NotFound();
            }

            List<Reservation> reservationsChambre = _context.Reservation.Where(r => r.IdChambre == idChambre).ToList();            
            return View(reservationsChambre);       


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifierDispo(int idChambre, DateTime dateArrivee, DateTime dateDepart)
        {
            List<Reservation> reservationsChambre = _context.Reservation.Where(r => r.IdChambre == idChambre).ToList();
            ViewBag.Message = "Vous pouvez reserver dans cette date";
            foreach (Reservation reservation in reservationsChambre)
            {
                if( dateArrivee.CompareTo(reservation.DateArrivee) >= 0 && dateArrivee.CompareTo(reservation.DateDepart) <= 0)                   
                {
                    ViewBag.Message = "Cette chambre est reservée (date Debut) !!";
                    break;
                    //return View(reservationsChambre);
                }
                if(dateDepart.CompareTo(reservation.DateArrivee) >= 0 && dateDepart.CompareTo(reservation.DateDepart) <= 0)
                {
                    ViewBag.Message = "Cette chambre est reservée (date Fin ) !!";
                    break;
                }
                if (dateArrivee.CompareTo(reservation.DateArrivee) <= 0 && dateDepart.CompareTo(reservation.DateDepart) >= 0)
                {
                    ViewBag.Message = "Cette chambre est reservée";
                    break;
                }
            }
            
            return View(reservationsChambre);
            //return View();

        }


        // GET: Reservations
        public async Task<IActionResult> MesReservations()
        {
            var idCurrent = _contextAccessor.HttpContext.Session.GetInt32("UserId");
            // Récupérer le client à partir de l'ID fourni
            var client = await _context.Client.Include(c => c.Reservations)
                                                 .FirstOrDefaultAsync(c => c.Id == idCurrent);

            if (client == null)
            {
                return NotFound();
            }

            //// Récupérer les réservations associées au client
            var reservations = client.Reservations.ToList();

            return View(reservations);
            
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateArrivee,DateDepart,IdClient,IdChambre, NbrChambres")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateArrivee,DateDepart,IdClient,IdChambre,NbrChambres")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservation == null)
            {
                return Problem("Entity set 'tutoContext.Reservation'  is null.");
            }
            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservation.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return (_context.Reservation?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // GET: Reservations/ReserverChambre/5
        public async Task<IActionResult> ReserverChambre(int idChambre)
        {

            if (idChambre == null)
            {          
                return NotFound();
            }

            var chambre = await _context.Chambre.FindAsync(idChambre);
            if (chambre == null)
            {
                return NotFound();
            }

            var reservation = new Reservation { 
                Chambre= chambre,
                IdChambre=chambre.Id
            };
            return View(reservation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReserverChambre([Bind("Id,DateArrivee,DateDepart,IdClient,IdChambre,NbrChambres")] Reservation reservation)
        {
            //Verifier Dates avant de creer la reservation
            List<Reservation> reservationsChambre = _context.Reservation.Where(r => r.IdChambre == reservation.IdChambre).ToList();            
            foreach (Reservation res in reservationsChambre)
            {
                if (reservation.DateArrivee.CompareTo(res.DateArrivee) >= 0 && reservation.DateArrivee.CompareTo(res.DateDepart) <= 0)
                {
                    ViewBag.Message = "Cette chambre est reservée (date Debut) !!";
                    return RedirectToAction("ReserverChambre", new { idChambre = reservation.IdChambre } );
                }
                if (reservation.DateDepart.CompareTo(res.DateArrivee) >= 0 && reservation.DateDepart.CompareTo(res.DateDepart) <= 0)
                {
                    ViewBag.Message = "Cette chambre est reservée (date Fin ) !!";
                    //break;
                    return RedirectToAction("ReserverChambre" ,new { idChambre = reservation.IdChambre });
                }
                if ((reservation.DateArrivee.CompareTo(res.DateArrivee) <= 0 && reservation.DateDepart.CompareTo(res.DateDepart) >= 0))
                {
                    ViewBag.Message = "Cette chambre est reservée";
                    //break;
                    return RedirectToAction("ReserverChambre", new { idChambre = reservation.IdChambre });
                }
            }

            var idCurrent = _contextAccessor.HttpContext.Session.GetInt32("UserId");
            // Création de la réservation
            var reservation1 = new Reservation
            {
                Id = reservation.Id,
                IdChambre = reservation.IdChambre,
                //IdClient = reservation.IdClient,
                IdClient = (int) idCurrent,
                DateArrivee = reservation.DateArrivee,
                DateDepart = reservation.DateDepart,
                NbrChambres= reservation.NbrChambres   

                //DateArrivee = DateTime.UtcNow,
                //DateDepart= DateTime.Now.AddDays(3)

            };
            _context.Add(reservation1);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
