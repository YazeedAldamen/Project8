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
    public class onettwothreeController : Controller
    {
        private universityERPEntities db = new universityERPEntities();

        // GET: onettwothree
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.Doctor).Include(c => c.Major);
            return View(courses.ToList());
        }

        // GET: onettwothree/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // GET: onettwothree/Create
        public ActionResult Create()
        {
            ViewBag.doctorId = new SelectList(db.Doctors, "doctorId", "doctorName");
            ViewBag.majorId = new SelectList(db.Majors, "majorId", "majorName");
            return View();
        }

        // POST: onettwothree/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "courseId,courseName,courseHours,majorId,doctorId")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(cours);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.doctorId = new SelectList(db.Doctors, "doctorId", "doctorName", cours.doctorId);
            ViewBag.majorId = new SelectList(db.Majors, "majorId", "majorName", cours.majorId);
            return View(cours);
        }

        // GET: onettwothree/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            ViewBag.doctorId = new SelectList(db.Doctors, "doctorId", "doctorName", cours.doctorId);
            ViewBag.majorId = new SelectList(db.Majors, "majorId", "majorName", cours.majorId);
            return View(cours);
        }

        // POST: onettwothree/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "courseId,courseName,courseHours,majorId,doctorId")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.doctorId = new SelectList(db.Doctors, "doctorId", "doctorName", cours.doctorId);
            ViewBag.majorId = new SelectList(db.Majors, "majorId", "majorName", cours.majorId);
            return View(cours);
        }

        // GET: onettwothree/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // POST: onettwothree/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cours cours = db.Courses.Find(id);
            db.Courses.Remove(cours);
            db.SaveChanges();
            return RedirectToAction("Index");
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
