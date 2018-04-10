using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRUD_Entity.Models;

namespace CRUD_Entity.Controllers
{
    public class PilotoesController : Controller
    {
        private Context db = new Context();

        // GET: Pilotoes
        public ActionResult Index()
        {
            return View(db.Piloto.ToList());
        }

        // GET: Pilotoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Piloto piloto = db.Piloto.Find(id);
            if (piloto == null)
            {
                return HttpNotFound();
            }
            return View(piloto);
        }

        // GET: Pilotoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pilotoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPiloto,RG,NumeroLicenca,CPFCNPJ,Nome,Sobrenome")] Piloto piloto)
        {
            if (ModelState.IsValid)
            {
                db.Piloto.Add(piloto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(piloto);
        }

        // GET: Pilotoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Piloto piloto = db.Piloto.Find(id);
            if (piloto == null)
            {
                return HttpNotFound();
            }
            return View(piloto);
        }

        // POST: Pilotoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPiloto,RG,NumeroLicenca,CPFCNPJ,Nome,Sobrenome")] Piloto piloto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(piloto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(piloto);
        }

        // GET: Pilotoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Piloto piloto = db.Piloto.Find(id);
            if (piloto == null)
            {
                return HttpNotFound();
            }
            return View(piloto);
        }

        // POST: Pilotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Piloto piloto = db.Piloto.Find(id);
            db.Piloto.Remove(piloto);
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
