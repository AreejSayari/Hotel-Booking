using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace tuto.Controllers
{
    public class ReservationChambre : Controller
    {
        // GET: ReservationChambre
        public ActionResult Index()
        {
            return View();
        }

        // GET: ReservationChambre/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReservationChambre/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReservationChambre/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservationChambre/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReservationChambre/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservationChambre/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReservationChambre/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
