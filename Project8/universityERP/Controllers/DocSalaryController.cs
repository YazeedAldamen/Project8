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
    public class DocSalaryController : Controller
    {
        private universityERPEntities db = new universityERPEntities();

        // GET: DocSalary
        public ActionResult Index()
        {
            var doctors = db.Doctors.Include(d => d.Major);
            return View(doctors.ToList());
        }





        // GET: DocSalary/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            ViewBag.majorId = new SelectList(db.Majors, "majorId", "majorName", doctor.majorId);
            return View(doctor);
        }

        // POST: DocSalary/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "doctorId,doctorName,doctorSalary,majorId")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.majorId = new SelectList(db.Majors, "majorId", "majorName", doctor.majorId);
            return View(doctor);
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
