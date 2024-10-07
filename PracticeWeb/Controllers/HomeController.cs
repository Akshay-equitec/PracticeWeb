using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticeWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public  ActionResult  Caste(string name, int num)
        {
            ViewBag.message = name + " how are u?";
            ViewBag.number = num;
            return View();
            //check
        }

        
    }
}