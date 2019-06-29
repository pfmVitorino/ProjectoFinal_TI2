using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GamingStore_Projectoti2.Models;

namespace GamingStore_Projectoti2.Controllers
{
  
    public class ListaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SearchBy"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        // GET: Lista
        public ActionResult Index(string SearchBy, string search)
        {
            // filtar os jogo pelo seu nome e pela sua plataforma
            if (SearchBy == "Plataforma")


                return View(db.Jogos.Where(x => x.Plataforma == search || search == null).ToList());
            else
                return View(db.Jogos.Where(x => x.Nome.StartsWith(search) || search == null).ToList());

        }
          
        
    
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Lista/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jogos jogo = db.Jogos.Find(id);
            if (jogo == null)
            {
                return RedirectToAction("Index");
            }
            //ViewBag.ComprasFK = new SelectList(db.Compras, "Id", "Id", jogo.Id);
            //ViewBag.JogosFK = new SelectList(db.Jogos, "Id", "Nome",jogo.Nome);
            //ViewBag.PlataformasFK = new SelectList(db.Plataformas, "Id", "Nome", jogo.Plataforma);
          

            Session["Metodo"] = "";
            return View(jogo);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Lista/buy/5
        [Authorize(Roles = "Cliente")] // apenas os clientes têm acesso a esta página
        public ActionResult Buy(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jogos jogo = db.Jogos.Find(id);
            if (jogo == null)
            {
                return RedirectToAction("Index");
            }

            //ViewBag.ComprasFK = new SelectList(db.Compras, "Id", "Id", jogo.Id);
            //ViewBag.JogosFK = new SelectList(db.Jogos, "Id", "Nome",jogo.Nome);
            //ViewBag.PlataformasFK = new SelectList(db.Plataformas, "Id", "Nome", jogo.Plataforma);
          
            Session["Metodo"] = "";
            return View(jogo);
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
