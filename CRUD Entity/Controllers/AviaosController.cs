using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRUD_Entity.Models;
using PagedList;

namespace CRUD_Entity.Controllers
{
    public class AviaosController : Controller
    {
        private Context db = new Context();

        #region Details        

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

        #endregion

        #region Create

        public ActionResult Create()
        {
            var dropPilotos = new List<SelectListItem>();

            dropPilotos.Add(new SelectListItem { Text = "Selecione...", Selected = true });

            foreach (var piloto in db.Piloto.Where(o => o.Ativo).OrderBy(o => o.Nome).ToList())
                dropPilotos.Add(new SelectListItem { Text = piloto.Nome, Value = piloto.IdPiloto.ToString() });

            ViewBag.Piloto = dropPilotos;

            return View();
        }

        #endregion

        #region Create Post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAviao,Marca,Modelo,Ano,PilotoRefId")] Aviao aviao)
        {
                if (ModelState.IsValid)
                {
                    db.Aviao.Add(aviao);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }            

            return View(aviao);
        }

        #endregion

        #region Edit

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

            dropPilotos.Add(new SelectListItem { Text = "Selecione...", Selected = true });

            foreach (var piloto in db.Piloto.Where(o => o.Ativo).OrderBy(o => o.Nome).ToList())
                dropPilotos.Add(new SelectListItem { Text = piloto.Nome, Value = piloto.IdPiloto.ToString() });

            ViewBag.Piloto = dropPilotos;

            return View(aviao);

        }

        #endregion

        #region Edit Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAviao,Marca,Modelo,Ano,PilotoRefId")] Aviao aviao)
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

        #endregion

        #region Delete

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

        #endregion

        #region DeleteConfirmed

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Aviao aviao = db.Aviao.Find(id);
            db.Aviao.Remove(aviao);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Index

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.IDSortParm = sortOrder == "ID" ? "IDDesc" : "ID";
            ViewBag.ModeloSortParm = sortOrder == "Modelo" ? "ModeloDesc" : "Modelo";
            ViewBag.PilotoSortParm = sortOrder == "Piloto" ? "PilotoDesc" : "Piloto";
            ViewBag.MarcaSortParm = sortOrder == "Marca" ? "MarcaDesc" : "Marca";
            ViewBag.AnoSortParm = sortOrder == "Ano" ? "AnoDesc" : "Ano";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var avioes = from s in db.Aviao.Include(o => o.Pilotos)
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                avioes = avioes.Where(s => s.Modelo.Contains(searchString)
                                       || s.Marca.Contains(searchString) 
                                       || s.Pilotos.Nome.Contains(searchString)
                                       || s.Ano.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {

                case "IDDesc":
                    avioes = avioes.OrderByDescending(s => s.IdAviao);
                    break;

                case "ID":
                    avioes = avioes.OrderBy(s => s.IdAviao);
                    break;

                case "PilotoDesc":
                    avioes = avioes.OrderByDescending(s => s.Pilotos.Nome);
                    break;

                case "Piloto":
                    avioes = avioes.OrderBy(s => s.Pilotos.Nome);
                    break;

                case "Modelo":
                    avioes = avioes.OrderByDescending(s => s.Modelo);
                    break;

                case "ModeloDesc":
                    avioes = avioes.OrderBy(s => s.Modelo);
                    break;

                case "Marca":
                    avioes = avioes.OrderByDescending(s => s.Marca);
                    break;

                case "MarcaDesc":
                    avioes = avioes.OrderBy(s => s.Marca);
                    break;

                case "Ano":
                    avioes = avioes.OrderByDescending(s => s.Marca);
                    break;

                case "AnoDesc":
                    avioes = avioes.OrderBy(s => s.Marca);
                    break;

                default:
                    avioes = avioes.OrderBy(s => s.IdAviao);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(avioes.ToPagedList(pageNumber, pageSize));
        }

        #endregion
    }
}
