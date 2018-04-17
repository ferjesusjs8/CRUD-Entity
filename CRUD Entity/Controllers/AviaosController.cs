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
        //public ActionResult Index()
        //{
        //    var aviao = db.Aviao.Include(a => a.Pilotos);
        //    return View(aviao.ToList());
        //}

        // GET: Aviaos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aviao aviao = db.Aviao.Include(a => a.Pilotos).FirstOrDefault(a => a.IdAviao.Equals(id.Value));
            if (aviao == null)
            {
                return HttpNotFound();
            }
            return View(aviao);
        }

        // GET: Aviaos/Create
        public ActionResult Create()
        {
            var dropPilotos = new List<SelectListItem>();

            foreach (var piloto in db.Piloto.Where(o => o.Ativo).ToList())
                dropPilotos.Add(new SelectListItem { Text = piloto.Nome, Value = piloto.IdPiloto.ToString() });

            dropPilotos.Add(new SelectListItem { Text = "Selecione...", Selected = true });

            ViewBag.Piloto = dropPilotos;

            return View();
        }

        // POST: Aviaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAviao,PilotoRefId,Modelo,Marca")] Aviao aviao)
        {
                if (ModelState.IsValid)
                {
                    db.Aviao.Add(aviao);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }            

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

            var dropPilotos = new List<SelectListItem>();

            foreach (var piloto in db.Piloto.Where(o => o.Ativo).ToList())
                dropPilotos.Add(new SelectListItem { Text = piloto.Nome, Value = piloto.IdPiloto.ToString() });

            dropPilotos.Add(new SelectListItem { Text = "Selecione...", Selected = true });

            ViewBag.Piloto = dropPilotos;

            return View(aviao);
            //ViewBag.PilotoRefId = new SelectList(db.Piloto, "IdPiloto", "Nome", aviao.PilotoRefId);
            //return View(aviao);
        }

        // POST: Aviaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAviao,PilotoRefId,Modelo,Marca")] Aviao aviao)
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
            Aviao aviao = db.Aviao.Include(a => a.Pilotos).FirstOrDefault(a => a.IdAviao.Equals(id.Value));
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
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Modelo" : "";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var pilotos = from s in db.Aviao.Include(o => o.Pilotos)
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                pilotos = pilotos.Where(s => s.Modelo.Contains(searchString)
                                       || s.Marca.Contains(searchString) 
                                       || s.Pilotos.Nome.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Piloto":
                    pilotos = pilotos.OrderByDescending(s => s.Pilotos.Nome);
                    break;
                case "Modelo":
                    pilotos = pilotos.OrderByDescending(s => s.Modelo);
                    break;
                case "Marca":
                    pilotos = pilotos.OrderBy(s => s.Marca);
                    break;
                default:
                    pilotos = pilotos.OrderBy(s => s.Modelo);
                    break;
            }
            return View(pilotos.ToList());
        }
    }
}
