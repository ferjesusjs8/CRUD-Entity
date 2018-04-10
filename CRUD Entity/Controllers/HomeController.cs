using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Entity.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Sobre o grupo";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contate-nos";

            return View();
        }
    }
}