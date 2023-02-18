using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using universityERP.Models;

namespace universityERP.Controllers
{
    public class MajorsController : Controller
    {
        private universityERPEntities db = new universityERPEntities();

        // GET: Majors
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            var majors = db.Majors.Include(m => m.Facility);
            return View(majors.ToList());
        }
        public ActionResult Index2(int? id)
        {

            var singleMajor = db.Majors.Where(m => m.facilityId == id);
            return View(singleMajor.ToList());
        }
        // GET: Majors/Details/5
        [Authorize(Roles = "Admin")]

        public ActionResult Details(int? id)
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
            return View(major);
        }

        // GET: Majors/Create
        [Authorize(Roles = "Admin")]

        public ActionResult Create()
        {
            ViewBag.facilityId = new SelectList(db.Facilities, "facilityId", "facilityName");
            return View();
        }

        // POST: Majors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "majorId,majorName,majorDescription,majorImage,numberOfHours,majorHourPrice,facilityId")] Major major, HttpPostedFileBase majorImage)
        {

            if (ModelState.IsValid)
            {
                string path = "../images/" + majorImage.FileName;
                majorImage.SaveAs(Server.MapPath(path));
                major.majorImage = majorImage.FileName;

                db.Majors.Add(major);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(major);
        }

        // GET: Majors/Edit/5
        [Authorize(Roles = "Admin")]

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

        // POST: Majors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include = "majorId,majorName,majorDescription,majorImage,numberOfHours,majorHourPrice,facilityId")] Major major, HttpPostedFileBase majorImage)
        {


            if (ModelState.IsValid)
            {
                var existingModel = db.Majors.AsNoTracking().FirstOrDefault(x => x.majorId == id);


                if (majorImage != null)
                {


                    string pathpic = Path.GetFileName(majorImage.FileName);
                    majorImage.SaveAs(Path.Combine(Server.MapPath("~/images/"), majorImage.FileName));
                    major.majorImage = pathpic;

                }
                else
                {
                    major.majorImage = existingModel.majorImage;
                }


                db.Entry(major).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            ViewBag.majorId = new SelectList(db.Majors, "majorId", "majorName", major.majorId);
            return View(major);


        }

        // GET: Majors/Delete/5
        [Authorize(Roles = "Admin")]

        public ActionResult Delete(int? id)
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
            return View(major);
        }

        // POST: Majors/Delete/5
        [Authorize(Roles = "Admin")]

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Major major = db.Majors.Find(id);
            db.Majors.Remove(major);
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
