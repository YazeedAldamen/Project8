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
    public class AccounterController : Controller
    {
        private universityERPEntities db = new universityERPEntities();

        // GET: Accounter
        public ActionResult Index()
        {
            var majors = db.Majors.Include(m => m.Facility).OrderByDescending(x => x.Facility.facilityName);
            return View(majors.ToList());
        }



        // GET: Accounter/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Major major = db.Majors.Find(id);
            if (major == null)
            {
                return HttpNotFound();
            }
            ViewBag.facilityId = new SelectList(db.Facilities, "facilityId", "facilityName", major.facilityId);
            return View(major);
        }

        // POST: Accounter/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "majorId,majorName,majorDescription,majorImage,numberOfHours,majorHourPrice,facilityId")] Major major)
        {
            if (ModelState.IsValid)
            {
                db.Entry(major).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.facilityId = new SelectList(db.Facilities, "facilityId", "facilityName", major.facilityId);
            return View(major);
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
