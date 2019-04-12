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
    public class morfologiaqrsController : Controller
    {
        private EcgEntities db = new EcgEntities();

        // GET: morfologiaqrs
        public ActionResult Index()
        {
            return View(db.morfologia_qrs.ToList());
        }

        // GET: morfologiaqrs/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            morfologia_qrs morfologia_qrs = db.morfologia_qrs.Find(id);
            if (morfologia_qrs == null)
            {
                return HttpNotFound();
            }
            return View(morfologia_qrs);
        }

        // GET: morfologiaqrs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: morfologiaqrs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "morfologiaqrs_id,morfologia_qrs1")] morfologia_qrs morfologia_qrs)
        {
            if (ModelState.IsValid)
            {
                db.morfologia_qrs.Add(morfologia_qrs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(morfologia_qrs);
        }

        // GET: morfologiaqrs/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            morfologia_qrs morfologia_qrs = db.morfologia_qrs.Find(id);
            if (morfologia_qrs == null)
            {
                return HttpNotFound();
            }
            return View(morfologia_qrs);
        }

        // POST: morfologiaqrs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "morfologiaqrs_id,morfologia_qrs1")] morfologia_qrs morfologia_qrs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(morfologia_qrs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(morfologia_qrs);
        }

        // GET: morfologiaqrs/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            morfologia_qrs morfologia_qrs = db.morfologia_qrs.Find(id);
            if (morfologia_qrs == null)
            {
                return HttpNotFound();
            }
            return View(morfologia_qrs);
        }

        // POST: morfologiaqrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            morfologia_qrs morfologia_qrs = db.morfologia_qrs.Find(id);
            db.morfologia_qrs.Remove(morfologia_qrs);
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
