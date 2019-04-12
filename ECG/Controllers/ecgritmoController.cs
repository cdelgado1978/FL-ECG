using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECG_DB;

namespace ECG.Controllers
{
    public class ecgritmoController : Controller
    {
        private EcgEntities db = new EcgEntities();

        // GET: ecgritmo
        public ActionResult Index()
        {
            return View(db.ecg_ritmo.ToList());
        }

        // GET: ecgritmo/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ecg_ritmo ecg_ritmo = db.ecg_ritmo.Find(id);
            if (ecg_ritmo == null)
            {
                return HttpNotFound();
            }
            return View(ecg_ritmo);
        }

        // GET: ecgritmo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ecgritmo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ritmo_id,ritmo")] ecg_ritmo ecg_ritmo)
        {
            if (ModelState.IsValid)
            {
                db.ecg_ritmo.Add(ecg_ritmo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ecg_ritmo);
        }

        // GET: ecgritmo/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ecg_ritmo ecg_ritmo = db.ecg_ritmo.Find(id);
            if (ecg_ritmo == null)
            {
                return HttpNotFound();
            }
            return View(ecg_ritmo);
        }

        // POST: ecgritmo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ritmo_id,ritmo")] ecg_ritmo ecg_ritmo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ecg_ritmo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ecg_ritmo);
        }

        // GET: ecgritmo/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ecg_ritmo ecg_ritmo = db.ecg_ritmo.Find(id);
            if (ecg_ritmo == null)
            {
                return HttpNotFound();
            }
            return View(ecg_ritmo);
        }

        // POST: ecgritmo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            ecg_ritmo ecg_ritmo = db.ecg_ritmo.Find(id);
            db.ecg_ritmo.Remove(ecg_ritmo);
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
