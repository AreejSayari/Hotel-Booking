using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tuto.Data;
using tuto.Models;

namespace tuto.Controllers
{
    public class LoginsController : Controller
    {
        private readonly tutoContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public LoginsController(tutoContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _contextAccessor = httpContextAccessor;
        }

        
        public IActionResult Login()
        {            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(LoginViewModel model)
        public async Task<IActionResult> Login ([Bind("Id,Email,Password")] LoginViewModel model)
        {
            var Email = model.Email;
            var Password = model.Password;
            var user = _context.LoginViewModel.FirstOrDefault(u => u.Email == Email && u.Password == Password);
            if (user != null)
            {

                _contextAccessor.HttpContext.Session.SetInt32("UserId", user.Id);
                _contextAccessor.HttpContext.Session.SetString("UserType", user.GetType().ToString());
               
                return RedirectToAction("Index", "Home");

            }
            return RedirectToAction(nameof(Login));
            //return View(model);

        }
       
    }
}
