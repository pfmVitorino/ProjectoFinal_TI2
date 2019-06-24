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
    public class Detalhes_CompraController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Detalhes_Compra
        public ActionResult Index()
        {
            var detalhes_Compra = db.Detalhes_Compra.Include(d => d.Compras).Include(d => d.Jogos).Include(d => d.Plataformas);
            return View(detalhes_Compra.ToList());
        }

        // GET: Detalhes_Compra/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalhes_Compra detalhes_Compra = db.Detalhes_Compra.Find(id);
            if (detalhes_Compra == null)
            {
                return HttpNotFound();
            }
            return View(detalhes_Compra);
        }

        // GET: Detalhes_Compra/Create
        public ActionResult Create()
        {
            ViewBag.ComprasFK = new SelectList(db.Compras, "Id", "Id");
            ViewBag.JogosFK = new SelectList(db.Jogos, "Id", "Nome");
            ViewBag.PlataformasFK = new SelectList(db.Plataformas, "Id", "Nome");
            return View();
        }

        // POST: Detalhes_Compra/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Quantidade,Preco,PlataformasFK,JogosFK,ComprasFK")] Detalhes_Compra detalhes_Compra)
        {
            if (ModelState.IsValid)
            {
                db.Detalhes_Compra.Add(detalhes_Compra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ComprasFK = new SelectList(db.Compras, "Id", "Id", detalhes_Compra.ComprasFK);
            ViewBag.JogosFK = new SelectList(db.Jogos, "Id", "Nome", detalhes_Compra.JogosFK);
            ViewBag.PlataformasFK = new SelectList(db.Plataformas, "Id", "Nome", detalhes_Compra.PlataformasFK);
            return View(detalhes_Compra);
        }

        // GET: Detalhes_Compra/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalhes_Compra detalhes_Compra = db.Detalhes_Compra.Find(id);
            if (detalhes_Compra == null)
            {
                return HttpNotFound();
            }
            ViewBag.ComprasFK = new SelectList(db.Compras, "Id", "Id", detalhes_Compra.ComprasFK);
            ViewBag.JogosFK = new SelectList(db.Jogos, "Id", "Nome", detalhes_Compra.JogosFK);
            ViewBag.PlataformasFK = new SelectList(db.Plataformas, "Id", "Nome", detalhes_Compra.PlataformasFK);
            return View(detalhes_Compra);
        }

        // POST: Detalhes_Compra/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Quantidade,Preco,PlataformasFK,JogosFK,ComprasFK")] Detalhes_Compra detalhes_Compra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalhes_Compra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ComprasFK = new SelectList(db.Compras, "Id", "Id", detalhes_Compra.ComprasFK);
            ViewBag.JogosFK = new SelectList(db.Jogos, "Id", "Nome", detalhes_Compra.JogosFK);
            ViewBag.PlataformasFK = new SelectList(db.Plataformas, "Id", "Nome", detalhes_Compra.PlataformasFK);
            return View(detalhes_Compra);
        }

        // GET: Detalhes_Compra/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalhes_Compra detalhes_Compra = db.Detalhes_Compra.Find(id);
            if (detalhes_Compra == null)
            {
                return HttpNotFound();
            }
            return View(detalhes_Compra);
        }

        // POST: Detalhes_Compra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Detalhes_Compra detalhes_Compra = db.Detalhes_Compra.Find(id);
            db.Detalhes_Compra.Remove(detalhes_Compra);
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
