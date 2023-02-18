using Microsoft.AspNet.Identity;
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
    public class CoursesController : Controller
    {
        private universityERPEntities db = new universityERPEntities();

        // GET: Courses
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.Major);
            return View(courses.ToList());
        }
        public ActionResult Index2()
        {
            var user = User.Identity.GetUserName().ToString();
            var id = db.Students.Where(x => x.userEmail == user).Single().majorId;

            var courses = db.Courses.Include(c => c.Major).Where(x => x.majorId == id);
            return View(courses.ToList());
        }
        // GET: Courses/Details/5
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

        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.majorId = new SelectList(db.Majors, "majorId", "majorName");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "courseId,courseName,courseHours,majorId")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(cours);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.majorId = new SelectList(db.Majors, "majorId", "majorName", cours.majorId);
            return View(cours);
        }

        // GET: Courses/Edit/5
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
            ViewBag.majorId = new SelectList(db.Majors, "majorId", "majorName", cours.majorId);
            return View(cours);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "courseId,courseName,courseHours,majorId")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.majorId = new SelectList(db.Majors, "majorId", "majorName", cours.majorId);
            return View(cours);
        }

        // GET: Courses/Delete/5
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

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cours cours = db.Courses.Find(id);
            db.Courses.Remove(cours);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Add(int? id)
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

        [HttpPost, ActionName("Add")]
        [ValidateAntiForgeryToken]
        public ActionResult AddConfirmed(int id)
        {
            var user = User.Identity.GetUserName().ToString();



            int Sid = db.Students.FirstOrDefault(d => d.userEmail == user).studentId;
            studentCours SC = new studentCours
            {
                courseId = id,
                studentId = Sid,
            };

            if (ModelState.IsValid)
            {

                db.studentCourses.Add(SC);
                db.SaveChanges();
                return RedirectToAction("Index2");

            }


            return View(SC);
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
