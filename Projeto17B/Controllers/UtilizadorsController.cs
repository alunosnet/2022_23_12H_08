using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Projeto17B.Data;
using Projeto17B.Models;

namespace Projeto17B.Controllers
{
    public class UtilizadorsController : Controller
    {
        private Projeto17BContext db = new Projeto17BContext();
        [Authorize(Roles = "Administrador")]
        // GET: Utilizadors
        public ActionResult Index()
        {
            return View(db.Utilizadors.ToList());
        }
        [Authorize(Roles = "Administrador")]
        // GET: Utilizadors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilizador utilizador = db.Utilizadors.Find(id);
            if (utilizador == null)
            {
                return HttpNotFound();
            }
            return View(utilizador);
        }
        
        // GET: Utilizadors/Create
        public ActionResult Create()
        {
            var utilizador = new Utilizador();
            utilizador.Perfis = new[]
            {
                new SelectListItem{Value="0", Text="Administrador"},
                new SelectListItem{Value="1", Text="Funcionário"}
            };
            return View(utilizador);
        }
        
        // POST: Utilizadors/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]  
        [ValidateAntiForgeryToken]
        
        public ActionResult Create([Bind(Include = "Id,Nome,Email,Password,Perfil,Estado")] Utilizador utilizador)
        {
            
            utilizador.Perfis = new[]
            {
                new SelectListItem{Value="0", Text="Administrador"},
                new SelectListItem{Value="1", Text="Funcionário"}
            };
            if (ModelState.IsValid)
            {
                //verificar se o nome de utilizador ja existe
                var temp = db.Utilizadors.Where(u => u.Nome == utilizador.Nome).ToList();
                if (temp != null && temp.Count > 0)
                {
                    ModelState.AddModelError("Nome", "Já existe um utilizador com esse nome");
                    return View(utilizador);
                }
                //validar a password
                if (utilizador.Password.Trim().Length < 5)
                {
                    ModelState.AddModelError("Password", "A palavra passe deve ter pelo menos 5 letras");
                    return View(utilizador);
                }
                //hash password
                HMACSHA512 hMACSHA512 = new HMACSHA512(new byte[] { 2 });
                var password = hMACSHA512.ComputeHash(Encoding.UTF8.GetBytes(utilizador.Password));
                utilizador.Password = Convert.ToBase64String(password);
                utilizador.Perfil = 1;
                db.Utilizadors.Add(utilizador);
                db.SaveChanges();
                return RedirectToAction("index","Login");
            }

            return View(utilizador);
        }
        [Authorize]
        // GET: Utilizadors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilizador utilizador = db.Utilizadors.Find(id);
            if (utilizador == null)
            {
                return HttpNotFound();
            }
            return View(utilizador);
        }

        // POST: Utilizadors/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,Nome,Email,Password,Perfil,Estado")] Utilizador utilizador)
        {


            utilizador.Perfis = new[]
            {
                new SelectListItem{Value="0", Text="Administrador"},
                new SelectListItem{Value="1", Text="Funcionário"}
            };

            if (ModelState.IsValid)
            {
                //validar a password
                if (utilizador.Password.Trim().Length < 5)
                {
                    ModelState.AddModelError("Password", "A palavra passe deve ter pelo menos 5 letras");
                    return View(utilizador);
                }

                //hash password
                if (User.IsInRole("Administrador"))
                {
                    HMACSHA512 hMACSHA512 = new HMACSHA512(new byte[] { 2 });
                    var password = hMACSHA512.ComputeHash(Encoding.UTF8.GetBytes(utilizador.Password));
                    utilizador.Password = Convert.ToBase64String(password);

                    db.Entry(utilizador).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else //guardar password
                {
                    Utilizador utilizadores = db.Utilizadors.Find(utilizador.Id);
                    utilizadores.Nome = utilizador.Nome;
                    utilizadores.Email = utilizador.Email;

                    db.Entry(utilizadores).State = EntityState.Modified;
                    db.SaveChanges();
                }


                FormsAuthentication.SetAuthCookie(utilizador.Nome, false);

                if (User.IsInRole("Administrador"))
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Index", "Home");
            }
            if (User.IsInRole("Administrador") == false)
            {
                utilizador.Perfis = new[]
                {
                    new SelectListItem{Value="1", Text="Funcionário"}
                };
            }
            
            return View(utilizador);
        }

        // GET: Utilizadors/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilizador utilizador = db.Utilizadors.Find(id);
            if (utilizador == null)
            {
                return HttpNotFound();
            }
            return View(utilizador);
        }

        // POST: Utilizadors/Delete/5
        [Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utilizador utilizador = db.Utilizadors.Find(id);
            db.Utilizadors.Remove(utilizador);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
