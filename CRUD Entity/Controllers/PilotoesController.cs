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
        //public ActionResult Index()
        //{
        //    return View(db.Piloto.ToList());
        //}

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

        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Nome" : "";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var pilotos = from s in db.Piloto
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                pilotos = pilotos.Where(s => s.Nome.Contains(searchString)
                                       || s.RG.Contains(searchString)
                                       || s.NumeroLicenca.Contains(searchString)
                                       || s.CPFCNPJ.Contains(searchString)
                                       || s.Sobrenome.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Nome":
                    pilotos = pilotos.OrderByDescending(s => s.Nome);
                    break;
                case "RG":
                    pilotos = pilotos.OrderBy(s => s.RG);
                    break;
                case "NumeroLicenca":
                    pilotos = pilotos.OrderByDescending(s => s.NumeroLicenca);
                    break;
                case "CPFCNPJ":
                    pilotos = pilotos.OrderByDescending(s => s.CPFCNPJ);
                    break;
                case "Sobrenome":
                    pilotos = pilotos.OrderByDescending(s => s.Sobrenome);
                    break;
                default:
                    pilotos = pilotos.OrderBy(s => s.Nome);
                    break;
            }
            return View(pilotos.ToList());
        }
    }
}
