using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GamingStore_Projectoti2.Models;

namespace GamingStore_Projectoti2.Controllers
{
    public class JogosController : Controller
    {
        private JogosDB db = new JogosDB();

        // GET: Jogos
        public ActionResult Index()
        {
            return View(db.Jogos.ToList());
        }

        // GET: Jogos/Details/5
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

        // GET: Jogos/Create
        public ActionResult Create()
        {
            ViewBag.Plataformas = db.Plataformas;
            return View();
        }

        // POST: Jogos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,Preco,Plataforma")] Jogos jogos, HttpPostedFileBase uploadFotografia)
        {

            //Escreve a fotografia que foi carregada - O save é efetuado na pasta das imagens do disco rigido
            //Especificar id do jogo
            //testar se há registos na tabela dos jogos

            // vars aux

            string caminho = "";
            bool ficheiroValido = false;




            /// 1º será que foi enviado um ficheiro?
            if (uploadFotografia == null)

            {   // volta ao index porque tem de adicionar foto
                return RedirectToAction("Index");

            }
            else
            {


                /// 2º será que o ficheiro, se foi fornecido, é do tipo correto?

                string mimeType = uploadFotografia.ContentType;
                if (mimeType == "image/jpeg" || mimeType == "image/png")
                {
                    // o ficheiro é do tipo correto

                    /// 3º qual o nome que devo dar ao ficheiro?
                    Guid g;
                    g = Guid.NewGuid(); // obtem os dados para o nome do ficheiro
                    // e qual a extensão do ficheiro?
                    string extensao = Path.GetExtension(uploadFotografia.FileName).ToLower();
                    // montar novo nome
                    string nomeFicheiro = g.ToString() + extensao;
                    // onde guardar o ficheiro?
                    caminho = Path.Combine(Server.MapPath("~/fotografias/"), nomeFicheiro);
                    /// 4º como o associar ao novo Jogo?
                    jogos.Fotografia = nomeFicheiro;

                    // marcar o ficheiro como válido
                    ficheiroValido = true;




                }
                else
                {
                    // o ficheiro fornecido nao é válido 
                    // atributo por defeito ao jogo
                    jogos.Fotografia = "no-user.jpg";
                }
            }



            // confronta os dados que vem da view com a forma que os dados devem  ter .
            // ie, valida os dados com o modelo
            if (ModelState.IsValid)
            {
                try
                {
                    db.Jogos.Add(jogos);
                    db.SaveChanges();


                    /// 5º como o guardar no disco rígido?
                    if (ficheiroValido)
                    {
                        uploadFotografia.SaveAs(caminho);
                    }
                    return RedirectToAction("Index");

                }
                catch (Exception)
                {


                }
                ViewBag.Plataformas = db.Plataformas;

            }

            return View(jogos);



        }

        // GET: Jogos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jogos jogos = db.Jogos.Find(id);
            if (jogos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Plataformas = db.Plataformas;
            return View(jogos);
        }

        // POST: Jogos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Preco,Fotografia,Plataforma")] Jogos jogos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jogos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Plataformas = db.Plataformas;
            return View(jogos);
        }

        // GET: Jogos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Jogos jogos = db.Jogos.Find(id);
            if (jogos == null)
            {
                return RedirectToAction("Index");
            }
            Session["Id"] = jogos.Id;
            Session["Metodo"] = "Jogos/Delete";
            ViewBag.Plataformas = db.Plataformas;
            return View(jogos);
        }

        // POST: Jogos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jogos jogos = db.Jogos.Find(id);
            db.Jogos.Remove(jogos);
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