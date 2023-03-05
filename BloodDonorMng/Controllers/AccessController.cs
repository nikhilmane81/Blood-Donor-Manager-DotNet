using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using AuthProject.Models;
using Microsoft.Data.SqlClient;

namespace AuthProject.Controllers
{
    public class AccessController : Controller
    {

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, Register reg)
        {
            try
            {
                Register.CreateR(reg);
                return RedirectToAction(nameof(Login));
            }
            catch
            {
                throw;
            }
        }


        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login modelLogin)
        {
            string emailcheck="";
            string passwordcheck="";
            //List<Register> registrations = new List<Register>();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DotNetProject;Integrated Security=True;";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select * from Register where Email = @email AND Password = @password ", cn);
                cmd.Parameters.AddWithValue("@email", modelLogin.Email);
                cmd.Parameters.AddWithValue("@password", modelLogin.PassWord);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        emailcheck = reader.GetString(1);
                        passwordcheck = reader.GetString(2);

                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
            

            if (modelLogin.Email == emailcheck &&
                modelLogin.PassWord == passwordcheck
                )
            { 
                List<Claim> claims = new List<Claim>() { 
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.Email),
                    new Claim("OtherProperties","Example Role")
                
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme );

                AuthenticationProperties properties = new AuthenticationProperties() { 
                
                    AllowRefresh = true,
                    IsPersistent = modelLogin.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);
            
                return RedirectToAction("Index", "Home");
            }



            ViewData["ValidateMessage"] = "Oooops! Incorrect Username/Password";
            return View();
        }
    }
}
