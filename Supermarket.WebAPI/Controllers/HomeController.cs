using Autofac;
using Supermarket.Repository.Common;
using Supermarket.Repository;
using Supermarket.Service.Common;
using Supermarket.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Supermarket.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            
            return View();
        }
    }
}
