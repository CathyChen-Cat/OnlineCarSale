using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineCarSale.Models;

namespace OnlineCarSale.Controllers
{
    public class BuyCarController : Controller
    {
        // GET: BuyCar
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchResult(string searchCompany, string searchModel)
        {
            OnlineCarSaleEntities db = new OnlineCarSaleEntities();
            var cars = from c in db.Cars
                       select c;

            //Search Function
            if (!String.IsNullOrEmpty(searchCompany) && !String.IsNullOrEmpty(searchModel))
            {
                cars = cars.Where(s => s.Company.Contains(searchCompany) && s.Model.Contains(searchModel));                
            }            
            return View(cars);
        }
    }
}