using AuthProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace AuthProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                List<Donor> list = Donor.GetAll();
                return View(list);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                Donor donor = Donor.GetD(id);
                return View(donor);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public ActionResult Create()
        {
            List<City> list = City.GetAll();
            ViewBag.list = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, Donor donor)
        {
            try
            {
                Donor.CreateD(donor);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw;
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                List<City> list = City.GetAll();
                ViewBag.list = list;
                Donor donor = Donor.GetD(id);
                return View(donor);
            }
            catch
            {
                throw;
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection, Donor donor)
        {
            try
            {
                Donor.EditD(id, donor);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                Donor donor = Donor.GetD(id);
                return View(donor);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Donor.DeleteD(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Access");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}