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
    [Authorize(Roles = "Accountant")]
    public class PaymentsStatisticsController : Controller
    {
        private universityERPEntities db = new universityERPEntities();

        // GET: PaymentsStatistics
        public ActionResult Index()
        {
            var payments = db.Payments.Include(p => p.Student);
            return View(payments.ToList());
        }


        public ActionResult CreatePie()
        {
            var eng = db.Doctors.Where(d => d.Major.facilityId == 1).Sum(d => d.doctorSalary);
            double doc = db.Doctors.Where(d => d.Major.facilityId == 1).Count();

            double avg = Convert.ToDouble(eng) / doc;

            var med = db.Doctors.Where(d => d.Major.facilityId == 2).Sum(d => d.doctorSalary);
            double doc1 = db.Doctors.Where(d => d.Major.facilityId == 2).Count();
            double avg1 = Convert.ToDouble(med) / doc1;

            var Sci = db.Doctors.Where(d => d.Major.facilityId == 3).Sum(d => d.doctorSalary);
            double doc2 = db.Doctors.Where(d => d.Major.facilityId == 3).Count();
            double avg3 = Convert.ToDouble(Sci) / doc2;

            var it = db.Doctors.Where(d => d.Major.facilityId == 4).Sum(d => d.doctorSalary);
            double doc3 = db.Doctors.Where(d => d.Major.facilityId == 4).Count();
            double avg4 = Convert.ToDouble(it) / doc3;

            var art = db.Doctors.Where(d => d.Major.facilityId == 5).Sum(d => d.doctorSalary);
            double doc4 = db.Doctors.Where(d => d.Major.facilityId == 5).Count();
            double avg5 = Convert.ToDouble(art) / doc4;

            var bus = db.Doctors.Where(d => d.Major.facilityId == 6).Sum(d => d.doctorSalary);
            double doc5 = db.Doctors.Where(d => d.Major.facilityId == 6).Count();
            double avg6 = Convert.ToDouble(bus) / doc5;


            var chart = new Chart(width: 500, height: 400)
            .AddSeries(chartType: "Pie",
        xValue: new[] { "Engineering", "Medicine", "Science", "IT", "Art", "Business" },
        yValues: new[] { avg, avg1, avg3, avg4, avg5, avg6 })
                            .GetBytes("png");
            return File(chart, "image/bytes");
        }
        public ActionResult CreateBar()
        {

            var sum = db.Payments.Where(d => d.Student.Major.facilityId == 1).Sum(d => d.payment1);
            double count = db.Payments.Where(d => d.Student.Major.facilityId == 1).Count();
            double avg = Convert.ToDouble(sum) / count;

            var sum1 = db.Payments.Where(d => d.Student.Major.facilityId == 1).Sum(d => d.payment1);
            double count1 = db.Payments.Where(d => d.Student.Major.facilityId == 1).Count();
            double avg1 = Convert.ToDouble(sum) / count;

            var sum2 = db.Payments.Where(d => d.Student.Major.facilityId == 1).Sum(d => d.payment1);
            double count2 = db.Payments.Where(d => d.Student.Major.facilityId == 1).Count();
            double avg2 = Convert.ToDouble(sum) / count;

            var sum3 = db.Payments.Where(d => d.Student.Major.facilityId == 1).Sum(d => d.payment1);
            double count3 = db.Payments.Where(d => d.Student.Major.facilityId == 1).Count();
            double avg3 = Convert.ToDouble(sum) / count;

            var sum4 = db.Payments.Where(d => d.Student.Major.facilityId == 1).Sum(d => d.payment1);
            double count4 = db.Payments.Where(d => d.Student.Major.facilityId == 1).Count();
            double avg4 = Convert.ToDouble(sum) / count;


            var sum5 = db.Payments.Where(d => d.Student.Major.facilityId == 1).Sum(d => d.payment1);
            double count5 = db.Payments.Where(d => d.Student.Major.facilityId == 1).Count();
            double avg5 = Convert.ToDouble(sum) / count;


            var chart = new Chart(width: 500, height: 400)
            .AddSeries(chartType: "bar",
        xValue: new[] { "Engineering", "Medicine", "Science", "IT", "Art", "Business" },
        yValues: new[] { avg, avg1, avg2, avg3, avg4, avg5 })
                            .GetBytes("png");
            return File(chart, "image/bytes");
        }





    }
}
