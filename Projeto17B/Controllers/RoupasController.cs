using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projeto17B.Data;
using Projeto17B.Models;

namespace Projeto17B.Controllers
{
    public class RoupasController : Controller
    {
        private Projeto17BContext db = new Projeto17BContext();
        [Authorize(Roles = "Administrador")]
        // GET: Roupas
        public ActionResult Index()
        {
           
            return View(db.Roupas.ToList());
        }
        [Authorize(Roles = "Administrador")]
        // GET: Roupas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roupas roupas = db.Roupas.Find(id);
            if (roupas == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoupaId = new SelectList(db.Roupas.Where(q => q.Quantidade > 0), "ID", "Name", roupas.ID);
            return View(roupas);
        }
        [Authorize(Roles = "Administrador")]
        // GET: Roupas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roupas/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create([Bind(Include = "ID,Name,Marca,Quantidade,Preco")] Roupas roupas)
        {
            if (ModelState.IsValid)
            {
                db.Roupas.Add(roupas);
                db.SaveChanges();

                HttpPostedFileBase fotografia = Request.Files["fotografia"];
                if (fotografia != null && fotografia.ContentLength > 0)
                {
                    string nome = Server.MapPath("~/Public/") + roupas.ID + ".jpg";
                    fotografia.SaveAs(nome);
                }

                return RedirectToAction("Index");
            }
            ViewBag.RoupaId = new SelectList(db.Roupas.Where(q => q.Quantidade > 0), "ID", "Name", roupas.ID);
            return View(roupas);
        }
        [Authorize(Roles = "Administrador")]
        // GET: Roupas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roupas roupas = db.Roupas.Find(id);
            if (roupas == null)
            {
                return HttpNotFound();
            }
            return View(roupas);
        }

        // POST: Roupas/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit([Bind(Include = "ID,Name,Marca,Quantidade,Preco")] Roupas roupas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roupas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roupas);
        }

        // GET: Roupas/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roupas roupas = db.Roupas.Find(id);
            if (roupas == null)
            {
                return HttpNotFound();
            }
            return View(roupas);
        }
        [Authorize(Roles = "Administrador")]
        // POST: Roupas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Roupas roupas = db.Roupas.Find(id);
            db.Roupas.Remove(roupas);
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
