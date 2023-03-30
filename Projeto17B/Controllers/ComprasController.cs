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
    public class ComprasController : Controller
    {
       
        private Projeto17BContext db = new Projeto17BContext();

        // GET: Compras
        [Authorize]
        public ActionResult Index()
        {
            if (User.IsInRole("Administrador"))
            {
                var lista = db.Compras.Include(c => c.utilizador).Include(r => r.roupa);
                return View("IndexAdmin",lista.ToList());
            }
            var compras = db.Roupas;
            return View(compras.ToList().Where(q => q.Quantidade > 0));
        }

        // GET: Compras/Details/5
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


            return View(roupas);
        }
        [HttpPost]
        public ActionResult RegistaCompra(int? id)
        {

            Compras compra = new Compras();

            compra.RoupaId = (int)id;
            compra.data_compra=DateTime.Now;
            //preço
            Roupas roupa = db.Roupas.Find(id);
            compra.valor_pagar = roupa.Preco;
            
            compra.valor_pagar = roupa.Preco;
            //cliente
            Utilizador cliente = db.Utilizadors.Where(c => c.Nome == User.Identity.Name).First();
            compra.UtilizadorId = cliente.Id;
            //atualizar quantidade da roupa
            var roupas = db.Roupas.Find(compra.RoupaId);
            roupas.Quantidade -= 1;
            db.Entry(roupas).CurrentValues.SetValues(roupas);
            db.Compras.Add(compra);

            db.SaveChanges();
            return RedirectToAction("index","Compras");
        }

        // GET: Compras/Create
        public ActionResult Create()
        {
            ViewBag.ClienteID = new SelectList(db.Utilizadors, "ID", "Nome");
            ViewBag.RoupaId = new SelectList(db.Roupas, "ID", "Name");
            return View();
        }

        // POST: Compras/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompraId,data_compra,valor_pagar,ClienteID,RoupaId")] Compras compras)
        {
            if (ModelState.IsValid)
            {
                db.Compras.Add(compras);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteID = new SelectList(db.Utilizadors, "Id", "Nome", compras.UtilizadorId);
            ViewBag.RoupaId = new SelectList(db.Roupas.Where(q => q.Quantidade > 0), "ID", "Name", compras.RoupaId);
            return View(compras);
        }

        // GET: Compras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteID = new SelectList(db.Utilizadors, "ID", "Nome", compras.UtilizadorId);
            ViewBag.RoupaId = new SelectList(db.Roupas.Where(q => q.Quantidade > 0 || q.ID == compras.RoupaId), "ID", "Name", compras.RoupaId);
            return View(compras);
        }

        // POST: Compras/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompraId,data_compra,valor_pagar,ClienteID,RoupaId")] Compras compras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteID = new SelectList(db.Utilizadors, "ID", "Nome", compras.UtilizadorId);
            ViewBag.RoupaId = new SelectList(db.Roupas.Where(q => q.Quantidade > 0 || q.ID == compras.RoupaId), "ID", "Name", compras.RoupaId);
            return View(compras);
        }

        // GET: Compras/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Compras compras = db.Compras.Find(id);
        //    if (compras == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(compras);
        //}

        //// POST: Compras/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Compras compras = db.Compras.Find(id);
        //    db.Compras.Remove(compras);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
