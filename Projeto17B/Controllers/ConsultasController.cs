using Projeto17B.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto17B.Controllers
{
    [Authorize]
    public class ConsultasController : Controller
    {
        private Projeto17BContext db = new Projeto17BContext();

        // GET: Consultas
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult PesquisaCliente()
        {
            string nome = Request.Form["nome"];
            var utilizadores = db.Utilizadors.Where(c => c.Nome.Contains(nome));
            return View("PesquisaCliente", utilizadores.ToList());
        }
        public ActionResult PesquisaDinamica()
        {
            return View();
        }

        public JsonResult PesquisaNome(string nome)
        {
            var clientes = db.Utilizadors.Where(c => c.Nome.Contains(nome)).ToList();
            var lista = new List<Campos>();
            foreach (var c in clientes)
                lista.Add(new Campos() { nome = c.Nome });
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MelhorCliente()
        {
            string sql = @"SELECT nome,sum(valor_pagar) as valor
                            FROM Compras INNER JOIN Utilizadors
                            ON Compras.UtilizadorId=Utilizadors.ID
                            GROUP BY Compras.UtilizadorId,nome
                            ORDER BY valor DESC";

            var melhor = db.Database.SqlQuery<Campos>(sql);
            if (melhor != null && melhor.ToList().Count > 0)
                ViewBag.melhor = melhor.ToList()[0];
            else
            {
                Campos temp = new Campos();
                temp.nome = "Não foram encontrados registos";
                ViewBag.melhor = temp;
            }
            return View();
        }

        public ActionResult ComprasDeUmCliente()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ComprasDeUmCliente(string nome)
        {
            string sql = @"Select nome,count(*) as n_compras
                            from Compras INNER JOIN Utilizadors
                            ON Compras.UtilizadorId=Utilizadors.Id
                            where nome like @p0
                            GROUP By nome";

            // SqlParameter parametro = new SqlParameter("@p1", "%" + nome + "%");
            var compras = db.Database.SqlQuery<Campos>(sql, "%" + nome + "%");

            if (compras != null && compras.ToList().Count > 0)
                ViewBag.compras = compras.ToList()[0];
            else
            {
                Campos temp = new Campos();
                temp.nome = "Não foram encontrados registos";
                ViewBag.compras = temp;
            }
            return View();
        }

        public class Campos
        {
            public string nome { get; set; }
         
            public decimal valor { get; set; }
            public int n_compras { get; set; }
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