using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskWeb.Aplicacao;
using TaskWeb.Dominio;

namespace TaskWeb.Controllers
{
    public class SqlController : Controller
    {
        // GET: Sql
        public ActionResult Index()
        {
            var cad = new AplicacaoSql();
            var listar = cad.ListarTodos();


            return View(listar);
        }

        public ActionResult Cadastrar()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(CadastroSql sql)
        {
            if (ModelState.IsValid)
            {
                var appSql = new AplicacaoSql();
                appSql.Inserir(sql);
                return RedirectToAction("Index");
            }
            return View(sql);
        } 
    }
}