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
    public class AviaosController : Controller
    {
        private Context db = new Context();

        // GET: Aviaos
        public ActionResult Index()
        {
            var aviao = db.Aviao.Include(a => a.Pilotos);
            return View(aviao.ToList());
        }

        // GET: Aviaos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aviao aviao = db.Aviao.Find(id);
            if (aviao == null)
            {
                return HttpNotFound();
            }
            return View(aviao);
        }

        // GET: Aviaos/Create
        public ActionResult Create()
        {
            ViewBag.PilotoRefId = new SelectList(db.Piloto, "IdPiloto", "Nome");
            return View();
        }

        // POST: Aviaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAviao,PilotoRefId,Modelo,Marca,Ano")] Aviao aviao)
        {
            if (ModelState.IsValid)
            {
                db.Aviao.Add(aviao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PilotoRefId = new SelectList(db.Piloto, "IdPiloto", "Nome", aviao.PilotoRefId);
            return View(aviao);
        }

        // GET: Aviaos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aviao aviao = db.Aviao.Find(id);
            if (aviao == null)
            {
                return HttpNotFound();
            }
            ViewBag.PilotoRefId = new SelectList(db.Piloto, "IdPiloto", "Nome", aviao.PilotoRefId);
            return View(aviao);
        }

        // POST: Aviaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAviao,PilotoRefId,Modelo,Marca,Ano")] Aviao aviao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aviao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PilotoRefId = new SelectList(db.Piloto, "IdPiloto", "Nome", aviao.PilotoRefId);
            return View(aviao);
        }

        // GET: Aviaos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aviao aviao = db.Aviao.Find(id);
            if (aviao == null)
            {
                return HttpNotFound();
            }
            return View(aviao);
        }

        // POST: Aviaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Aviao aviao = db.Aviao.Find(id);
            db.Aviao.Remove(aviao);
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
