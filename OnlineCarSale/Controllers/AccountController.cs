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
            if (Session["SId"] != null)
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
            ViewBag.Message = "Your Information Saved!";
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

            var sellerDetail = (from c in db.Cars
                                join s in login on c.SId equals s.SId
                                orderby c.CId
                                select new
                                {
                                    c.CId,
                                    c.Price,
                                    c.Company,
                                    c.Year,
                                    c.Model,
                                    c.BodyType,
                                    c.Location,
                                    s.SId,
                                    s.Name,
                                    s.Email,
                                    s.Phone,
                                    s.Address,
                                    s.Username
                                }).ToList();            

            if (sellerDetail.Count() <= 0)
            {
                ModelState.AddModelError("Username", "Login details incorrect.");
                return View("Login");
            }
            else
            {
                //Add session
                Session["SId"] = sellerDetail.FirstOrDefault().SId;
                Session["Name"] = sellerDetail.FirstOrDefault().Name;
                Session["Address"] = sellerDetail.FirstOrDefault().Address;
                Session["Phone"] = sellerDetail.FirstOrDefault().Phone;
                Session["Email"] = sellerDetail.FirstOrDefault().Email;
                Session["Username"] = sellerDetail.FirstOrDefault().Username;
                Session["Year"] = sellerDetail.FirstOrDefault().Year;
                Session["Company"] = sellerDetail.FirstOrDefault().Company;
                Session["Model"] = sellerDetail.FirstOrDefault().Model;
                Session["BodyType"] = sellerDetail.FirstOrDefault().BodyType;
                Session["Price"] = sellerDetail.FirstOrDefault().Price;
                Session["Location"] = sellerDetail.FirstOrDefault().Location;
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
        public ActionResult AddCar(string msg)
        {
            ViewBag.Message = msg;
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
                return RedirectToAction("AddCar", new { msg = "Car saved!" });
            }
            return View("AddCar", car);
        }
    }
}