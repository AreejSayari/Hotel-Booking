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
    public class ClientsController : Controller
    {
        private readonly tutoContext _context;

        public ClientsController(tutoContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
              return _context.Client != null ? 
                          View(await _context.Client.ToListAsync()) :
                          Problem("Entity set 'tutoContext.Client'  is null.");
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Client == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Firstname,Lastname,Email,Password")] Client client)
        {
            
            if (client != null)
            {
                _context.Client.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }            
            return View(client);
        }

        // GET: Clients/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Client == null)
        //    {
        //        return NotFound();
        //    }

        //    var client = await _context.Client.FindAsync(id);
        //    if (client == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(client);
        //}

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit()
        {
            var client = await _context.Client.FindAsync(2); //id du client (session)
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Firstname,Lastname,Email,Password")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            if (client != null)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            return View(client);
        }
       
        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Client == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Client == null)
            {
                return Problem("Entity set 'tutoContext.Client'  is null.");
            }
            var client = await _context.Client.FindAsync(id);
            if (client != null)
            {
                _context.Client.Remove(client);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
          return (_context.Client?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        /********************************************************************************/
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //async Task<IActionResult> Login([Bind("Id,Firstname,Lastname,Email,Password")] Client client)
        async Task<IActionResult> Login(string email, string password)
        {
            /*if (client != null)
            {
                var user = await  _context.Client.FirstOrDefaultAsync(e => e.Email == client.Email && e.Password == client.Password);
                 return RedirectToAction("Index");

            }
            return View(client);*/
            var user = await _context.Client.FirstOrDefaultAsync(e => e.Email == email && e.Password == password);
            return RedirectToAction(nameof(Index));



            /*var Email = model.Email;
            var Password = model.Password;
            var user = _context.Client.FirstOrDefault(u => u.Email == Email && u.Password == Password);
            if (user != null)
            {
                return _context.Client != null ?
                       View(await _context.Client.ToListAsync()) :
                       Problem("Entity set 'tutoContext.Client'  is null.");
                //return RedirectToAction("Index");
            }
            //return View(user);
            return _context.Client != null ?
                       View(await _context.Client.ToListAsync()) :
                       Problem("Entity set 'tutoContext.Client'  is null.");*/

        }
    }



}
