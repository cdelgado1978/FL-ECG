using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECG_DB;
using Microsoft.AspNet.Identity;

namespace ECG.Controllers
{
    public class motivos_visitaController : Controller
    {
        private EcgEntities db = new EcgEntities();

        // GET: motivos_visita
        public ActionResult Index()
        {
            return View(db.motivos_visita.ToList());
        }

        // GET: motivos_visita/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            motivos_visita motivos_visita = db.motivos_visita.Find(id);
            if (motivos_visita == null)
            {
                return HttpNotFound();
            }
            return View(motivos_visita);
        }

        // GET: motivos_visita/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: motivos_visita/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "motivo_id,motivo")] motivos_visita motivos_visita)
        {
            if (ModelState.IsValid)
            {

                motivos_visita.fecha_creacion = DateTime.Now;
                motivos_visita.usuario = User.Identity.GetUserName();
                motivos_visita.ultima_actualizacion= DateTime.Now;
                motivos_visita.usuario_ultima_actualizacion = User.Identity.Name;
                motivos_visita.activo = true;

                db.motivos_visita.Add(motivos_visita);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(motivos_visita);
        }

        // GET: motivos_visita/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            motivos_visita motivos_visita = db.motivos_visita.Find(id);
            if (motivos_visita == null)
            {
                return HttpNotFound();
            }
            return View(motivos_visita);
        }

        // POST: motivos_visita/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "motivo_id,motivo,activo")] motivos_visita motivos_visita)
        {
            if (ModelState.IsValid)
            {
                motivos_visita.ultima_actualizacion = DateTime.Now;
                motivos_visita.usuario_ultima_actualizacion = User.Identity.Name;

                db.Entry(motivos_visita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(motivos_visita);
        }

        // GET: motivos_visita/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            motivos_visita motivos_visita = db.motivos_visita.Find(id);
            if (motivos_visita == null)
            {
                return HttpNotFound();
            }
            return View(motivos_visita);
        }

        // POST: motivos_visita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            motivos_visita motivos_visita = db.motivos_visita.Find(id);
            db.motivos_visita.Remove(motivos_visita);
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
