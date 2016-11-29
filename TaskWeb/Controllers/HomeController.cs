using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskWeb.Aplicacao;
using TaskWeb.Models;
using TaskWeb.Dominio;
using TaskWeb.Repositorio;


namespace TaskWeb.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {

            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuarios user)
        {
            
            var x = new AplicacaoSql();
            //var MostraLogin = x.VerificaLogin(user).ToString();
            //var resultado = new Usuarios() { Nome = MostraLogin };
            
            x.VerificaLogin(user);
            return RedirectToAction("Geral");

        }

        [HttpGet]
        public ActionResult Geral(Usuarios user)
        {
            //var busca = user.Nome;
            var cad = new AplicacaoSql();
            var mostrarTMA = cad.TmaGeral(user).ToString();
            var resultado = new Indicadores() { tmageral = mostrarTMA };
            return View(resultado);
        }

       
    }
}