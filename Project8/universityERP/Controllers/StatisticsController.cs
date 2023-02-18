using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using universityERP.Models;

namespace universityERP.Controllers
{
[Authorize(Roles ="Admin")]
    public class StatisticsController : Controller
    {
        private universityERPEntities db = new universityERPEntities();

        // GET: Statistics
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Major);
            return View(students.ToList());
        }
        public ActionResult CreateBar()
        {

            int eng = db.Students.Count(item => item.Major.facilityId == 1);
            int med = db.Students.Count(item => item.Major.facilityId == 2);
            int Sci = db.Students.Count(item => item.Major.facilityId == 3);
            int it = db.Students.Count(item => item.Major.facilityId == 4);
            int art = db.Students.Count(item => item.Major.facilityId == 5);
            int bus = db.Students.Count(item => item.Major.facilityId == 6);

            var chart = new Chart(width: 500, height: 400)
            .AddSeries(chartType: "bar",
        xValue: new[] { "Engineering", "Medicine", "Science", "IT", "Art", "Business" },
        yValues: new[] { eng, med, Sci, it, art, bus })
                            .GetBytes("png");
            return File(chart, "image/bytes");
        }


        public ActionResult CreatePie()
        {

            int eng = db.Students.Count(item => item.Major.facilityId == 1);
            int med = db.Students.Count(item => item.Major.facilityId == 2);
            int Sci = db.Students.Count(item => item.Major.facilityId == 3);
            int it = db.Students.Count(item => item.Major.facilityId == 4);
            int art = db.Students.Count(item => item.Major.facilityId == 5);
            int bus = db.Students.Count(item => item.Major.facilityId == 6);

            var chart = new Chart(width: 500, height: 400)
            .AddSeries(chartType: "pie",
            xValue: new[] { "Engineering", "Medicine", "Science", "IT", "Art", "Business" },
            yValues: new[] { eng, med, Sci, it, art, bus })
                                .GetBytes("png");
            return File(chart, "image/bytes");
        }
        // GET: Statistics/Details/5
      
      

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
