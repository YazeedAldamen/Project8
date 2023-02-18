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

[Authorize(Roles="Admin")]
    public class FacilitiesController : Controller
    {
        private universityERPEntities db = new universityERPEntities();

        // GET: Facilities
        public ActionResult Index()
        {
            return View(db.Facilities.ToList());
        }

        // GET: Facilities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facility facility = db.Facilities.Find(id);
            if (facility == null)
            {
                return HttpNotFound();
            }
            return View(facility);
        }

        // GET: Facilities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Facilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "facilityId,facilityName,facilityDescription,facilityImage")] Facility facility, HttpPostedFileBase facilityImage)
        {


            if (ModelState.IsValid)
            {
                string path = "../images/" + facilityImage.FileName;
                facilityImage.SaveAs(Server.MapPath(path));
                facility.facilityImage = facilityImage.FileName;

                db.Facilities.Add(facility);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(facility);
        }

        // GET: Facilities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facility facility = db.Facilities.Find(id);
            if (facility == null)
            {
                return HttpNotFound();
            }
            return View(facility);
        }

        // POST: Facilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include = "facilityId,facilityName,facilityDescription,facilityImage")] Facility facility, HttpPostedFileBase facilityImage)
        {

            if (ModelState.IsValid)
            {
                var existingModel = db.Facilities.AsNoTracking().FirstOrDefault(x => x.facilityId == id);


                if (facilityImage != null)
                {

                    string pathpic = Path.GetFileName(facilityImage.FileName);
                    facilityImage.SaveAs(Path.Combine(Server.MapPath("~/images/"), facilityImage.FileName));
                    facility.facilityImage = pathpic;

                }
                else
                {
                    facility.facilityImage = existingModel.facilityImage;
                }


                db.Entry(facility).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(facility);
        }


        // GET: Facilities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facility facility = db.Facilities.Find(id);
            if (facility == null)
            {
                return HttpNotFound();
            }
            return View(facility);
        }

        // POST: Facilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Facility facility = db.Facilities.Find(id);
            db.Facilities.Remove(facility);
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
