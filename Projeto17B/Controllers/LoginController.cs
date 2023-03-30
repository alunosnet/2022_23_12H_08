using Projeto17B.Models;
using Projeto17B.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Text;

namespace Projeto17B.Controllers
{
    public class LoginController : Controller
    {
        private Projeto17BContext db = new Projeto17BContext();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Utilizador utilizador)
        {
            if (utilizador.Nome != null && utilizador.Password != null)
            {
                //hash password
                HMACSHA512 hMACSHA512 = new HMACSHA512(new byte[] { 2 });
                var password = hMACSHA512.ComputeHash(Encoding.UTF8.GetBytes(utilizador.Password));
                utilizador.Password = Convert.ToBase64String(password);
                foreach (var u in db.Utilizadors.ToList())
                {
                    if (u.Nome.ToLower() == utilizador.Nome.ToLower() &&
                        u.Password == utilizador.Password)
                    {
                        //iniciar sessão
                        FormsAuthentication.SetAuthCookie(utilizador.Nome, false);
                        //redirecionar
                        if (Request.QueryString["ReturnUrl"] == null)
                            return RedirectToAction("Index", "Home");
                        else
                            return Redirect(Request.QueryString["ReturnUrl"].ToString());
                    }
                }
            }
            ModelState.AddModelError("", "Login falhou. Tente novamente.");
            return View(utilizador);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}