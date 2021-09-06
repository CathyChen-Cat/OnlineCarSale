using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineCarSale.Controllers
{
    public class BuyCarController : Controller
    {
        // GET: BuyCar
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SearchResult()
        {
            return View();
        }
    }
}