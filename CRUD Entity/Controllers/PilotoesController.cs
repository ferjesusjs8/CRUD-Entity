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
        public ActionResult Create([Bind(Include = "IdPiloto,Nome,RG,CPFCNPJ,DataNascimento,NumeroLicenca,Ativo")] Piloto piloto)
        {
            if (ModelState.IsValid)
            {
                piloto.Ativo = true;
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
        public ActionResult Edit([Bind(Include = "IdPiloto,Nome,RG,CPFCNPJ,DataNascimento,NumeroLicenca,Ativo")] Piloto piloto)
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
            piloto.Ativo = false;
            db.Entry(piloto).State = EntityState.Modified;
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
            ViewBag.NomeSortParm = sortOrder == "Nome" ? "NomeDesc" : "Nome";
            ViewBag.RGSortParm = sortOrder == "RG" ? "RGDesc" : "RG";
            ViewBag.CPFSortParm = sortOrder == "CPFCNPJ" ? "CPFCNPJDesc" : "CPFCNPJ";
            ViewBag.LicencaSortParm = sortOrder == "NumeroLicenca" ? "NumeroLicencaDesc" : "NumeroLicenca";
            ViewBag.DataSortParm = sortOrder == "DataNascimento" ? "DataNascimentoDesc" : "DataNascimento";
            ViewBag.IdSortParm = sortOrder == "Id" ? "IdDesc" : "Id";
            ViewBag.AtivoSortParm = sortOrder == "Ativo" ? "AtivoDesc" : "Ativo";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var pilotos = from s in db.Piloto
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                pilotos = pilotos.Where(s => s.Nome.Contains(searchString)
                                       || s.RG.Contains(searchString)
                                       || s.NumeroLicenca.Contains(searchString)
                                       || s.CPFCNPJ.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "NomeDesc":
                    pilotos = pilotos.OrderByDescending(s => s.Nome);
                    break;

                case "Nome":
                    pilotos = pilotos.OrderBy(s => s.Nome);
                    break;

                case "RGDesc":
                    pilotos = pilotos.OrderByDescending(s => s.RG);
                    break;

                case "RG":
                    pilotos = pilotos.OrderBy(s => s.RG);
                    break;

                case "CPFCNPJDesc":
                    pilotos = pilotos.OrderByDescending(s => s.CPFCNPJ);
                    break;

                case "CPFCNPJ":
                    pilotos = pilotos.OrderBy(s => s.CPFCNPJ);
                    break;

                case "DataNascimentoDesc":
                    pilotos = pilotos.OrderByDescending(s => s.DataNascimento);
                    break;

                case "DataNascimento":
                    pilotos = pilotos.OrderBy(s => s.DataNascimento);
                    break;

                case "NumeroLicencaDesc":
                    pilotos = pilotos.OrderByDescending(s => s.NumeroLicenca);
                    break;

                case "NumeroLicenca":
                    pilotos = pilotos.OrderBy(s => s.NumeroLicenca);
                    break;

                case "IdDesc":
                    pilotos = pilotos.OrderByDescending(s => s.IdPiloto);
                    break;

                case "Id":
                    pilotos = pilotos.OrderBy(s => s.IdPiloto);
                    break;

                case "AtivoDesc":
                    pilotos = pilotos.OrderByDescending(s => s.Ativo);
                    break;

                case "Ativo":
                    pilotos = pilotos.OrderBy(s => s.Ativo);
                    break;

                default:
                    pilotos = pilotos.OrderBy(s => s.IdPiloto);
                    break;
            }
            return View(pilotos.ToList());
        }
    }
}
