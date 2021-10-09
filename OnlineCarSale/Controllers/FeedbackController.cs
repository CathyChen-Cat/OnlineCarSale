using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineCarSale.Models;

namespace OnlineCarSale.Controllers
{
    public class FeedbackController : Controller
    {
        // GET: Feedback
        public ActionResult Index(string msg)
        {
            ViewBag.Message = msg;
            return View();
        }

        public ActionResult Create([Bind(Include = "Name, Email, InfoRadio, NeedRadio, Comment")] Feedback feedback)
        {
            OnlineCarSaleEntities db = new OnlineCarSaleEntities();

            if (ModelState.IsValid)
            {
                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                return RedirectToAction("Index", new { msg = "Thanks for your feedback!" });
            }
            else
            {
                ModelState.AddModelError("", "Unable to save changes.");
                return View("Create", feedback);
            }
        }
    }
}