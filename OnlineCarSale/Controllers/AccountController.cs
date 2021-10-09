using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineCarSale.Models;

namespace OnlineCarSale.Controllers
{
    public class AccountController : Controller
    {
        private OnlineCarSaleEntities db = new OnlineCarSaleEntities();

        // GET: Seller Detail
        public ActionResult Index()
        {
            if (Session["Name"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult SellerDetail(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Login");
            }
            Seller seller = db.Sellers.Find(id);
            if (seller == null)
            {
                return RedirectToAction("Login");
            }
            return View(seller);
        }

        //GET: Register
        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", seller);
            }
            if (db.Sellers.Where(s => s.Username == seller.Username).Any())
            {
                ModelState.AddModelError("Username", "This user name already exists.");
                return View("Register", seller);
            }

            db.Sellers.Add(seller);
            db.SaveChanges();

            return RedirectToAction("Login");
        }

        //GET: Login
        public ActionResult Login()
        {
            return View();
        }

        //POST: Login
        [HttpPost]
        public ActionResult Login(string Username, string Passowrd)
        {
            if (!ModelState.IsValid)
            {
                return View("Login");
            }

            var login = db.Sellers.Where(u => u.Username.Equals(Username) && u.Passowrd.Equals(Passowrd));

            if (login.Count() <= 0)
            {
                ModelState.AddModelError("Username", "Login details incorrect.");
                return View("Login");
            }
            else
            {
                Session["Name"] = login.FirstOrDefault().Name;
                Session["Address"] = login.FirstOrDefault().Address;
                Session["Phone"] = login.FirstOrDefault().Phone;
                Session["Email"] = login.FirstOrDefault().Email;
                Session["Username"] = login.FirstOrDefault().Username;
                Session["Passowrd"] = login.FirstOrDefault().Passowrd;
                Session["SId"] = login.FirstOrDefault().SId;
                return RedirectToAction("Index", "Account");
            }
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        //GET
        public ActionResult AddCar()
        {
            return View();
        }

        //POST
        [HttpPost]
        public ActionResult AddCar(Car car)
        {
            if (ModelState.IsValid)
            {
                db.Cars.Add(car);
                db.SaveChanges();
                ViewBag.Message = "Car Information Saved!";
                return RedirectToAction("Index");
            }
            return View("AddCar", car);
        }
    }
}