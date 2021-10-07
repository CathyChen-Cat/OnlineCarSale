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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create([Bind(Include = "Name, Email, InfoRadio, NeedRadio, Comment")] Feedback feedback)
        {
            OnlineCarSaleEntities db = new OnlineCarSaleEntities();
            try
            {
                if (ModelState.IsValid)
                {
                    db.Feedbacks.Add(feedback);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(feedback);
        }
    }
}