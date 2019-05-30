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
        private JogosDB db = new JogosDB();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SearchBy"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        // GET: Lista
        public ActionResult Index(string SearchBy, string search)
        {
            // filtar os jogos pelo seu nome e pela sua plataforma
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
            Jogos jogos = db.Jogos.Find(id);
            if (jogos == null)
            {
                return RedirectToAction("Index");
            }
            Session["Metodo"] = "";
            return View(jogos);
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
