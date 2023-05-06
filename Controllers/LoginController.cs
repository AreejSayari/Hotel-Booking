using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tuto.Data;
using tuto.Models;

namespace tuto.Controllers
{
    public class LoginController : Controller
    {
        private readonly tutoContext _context;

        public LoginController(tutoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.Client != null ?
                        View(await _context.Client.ToListAsync()) :
                        Problem("Entity set 'tutoContext.Client'  is null.");
        }
        public IActionResult Login()
        {
            var model = new Client();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        async Task<IActionResult> Login ([Bind("Id,Firstname,Lastname,Email,Password")] Client model)
        {
            var Email = model.Email;
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
                       Problem("Entity set 'tutoContext.Client'  is null.");

        }
        /*async Task<IActionResult> Login([Bind("Id,Firstname,Lastname,Email,Password")] Client model)
        {
            if (model != null)
            {
                var user = _context.Client.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                if (user != null)
                {
                    Console.WriteLine("client not null");
                    // Stocker les informations d'utilisateur dans la session
                    HttpContext.Session.SetString("UserId", user.Id.ToString());                    
                   //return RedirectToAction("Success");
                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    Console.WriteLine("client null");
                    //ModelState.AddModelError(string.Empty, "Nom d'utilisateur ou mot de passe incorrect");
                }
            }

            return RedirectToAction(nameof(Index));
            //return View(model);
        }
        public IActionResult Success()
        {
            return RedirectToAction(nameof(Index));
        }*/
    }
}
