using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace teremeerbani2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult terms()
        {


            return View();
        }

        public ActionResult chooseus()
        {


            return View();
        }

        public ActionResult policy()
        {


            return View();
        }


        public ActionResult layoutCheck()
        {

           

            return View();
        }


        public ActionResult zoomer(string url,string name,string item)
        {
            ViewBag.name = name;
            ViewBag.item = item;
            ViewBag.url = url;
            return View();
        }


    }
}