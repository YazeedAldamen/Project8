using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using universityERP.Models;

namespace universityERP.Controllers
{

    [Authorize(Roles = "Accountant")]
    public class PaymentsController : Controller
    {
        private universityERPEntities db = new universityERPEntities();

        // GET: Payments
        public ActionResult Index()
        {
            var payments = db.Payments.Include(p => p.Student);
            return View(payments.ToList());
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}