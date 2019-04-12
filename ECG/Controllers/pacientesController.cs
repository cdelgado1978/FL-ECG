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
    public class pacientesController : Controller
    {
        private EcgEntities db = new EcgEntities();

        // GET: pacientes
        public ActionResult Index()
        {
            return View(db.pacientes.ToList());
        }

        // GET: pacientes/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paciente paciente = db.pacientes.Find(id);

            var EcgRealizados = paciente.ecgs.AsEnumerable();

            ViewBag.Estudios = EcgRealizados;
            
            if (paciente == null)
            {
                return HttpNotFound();
            }

            return View(paciente);
        }

        // GET: pacientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: pacientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "paciente_id,nombre,apellidos,fecha_nacimiento, genero")] paciente paciente)
        {
            if (ModelState.IsValid)
            {
                paciente.activo = true;
                paciente.fecha_creacion = DateTime.Now;
                paciente.creado_por = User.Identity.Name;
                paciente.fecha_ultimamodificacion = DateTime.Now;
                paciente.modificado_por = User.Identity.Name;

                db.pacientes.Add(paciente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paciente);
        }

        // GET: pacientes/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paciente paciente = db.pacientes.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // POST: pacientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "paciente_id,nombre,apellidos,fecha_nacimiento, genero,activo")] paciente paciente)
        {
            if (ModelState.IsValid)
            {
                paciente.fecha_ultimamodificacion = DateTime.Now;
                paciente.modificado_por = User.Identity.Name;

                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paciente);
        }

        // GET: pacientes/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paciente paciente = db.pacientes.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // POST: pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            paciente paciente = db.pacientes.Find(id);
            db.pacientes.Remove(paciente);
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
