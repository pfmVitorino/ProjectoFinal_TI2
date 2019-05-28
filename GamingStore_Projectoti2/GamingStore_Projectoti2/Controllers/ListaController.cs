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

        // GET: Lista
        public ActionResult Index()
        {
            return View(db.Jogos.ToList());
        }

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
