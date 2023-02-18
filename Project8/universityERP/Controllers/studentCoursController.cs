using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using universityERP.Models;

namespace universityERP.Controllers
{
[Authorize] 
    public class studentCoursController : Controller
    {
        private universityERPEntities db = new universityERPEntities();

        // GET: studentCours
        public ActionResult Index()
        {
            var studentCourses = db.studentCourses.Include(s => s.Cours).Include(s => s.Student);
            return View(studentCourses.ToList());
        }
        public ActionResult Index2()
        {
            var user = User.Identity.GetUserName().ToString();



            int Sid = db.Students.FirstOrDefault(d => d.userEmail == user).studentId;
            var studentCourses = db.studentCourses.Include(s => s.Cours).Include(s => s.Student).Where(x => x.studentId == Sid);
            return View(studentCourses.ToList());
        }

        [HttpPost, ActionName("Pay")]
        [ValidateAntiForgeryToken]
        public ActionResult Pay([Bind(Include = "paymentId,studentId,payment,totalFees")] int Payment)
        {
            var user = User.Identity.GetUserName().ToString();

            int Sid = db.Students.FirstOrDefault(d => d.userEmail == user).studentId;

            Payment PY = new Payment
            {
                studentId = Sid,
                payment1 = Payment,
                totalFees = int.Parse(Session["totalFees"].ToString()),

            };

            var record = db.studentCourses.Where(x => x.studentId == Sid);
            foreach (var item in record)
            {
                item.isPaid = true;
            }


            var sum = db.studentCourses.Where(d => d.studentId == Sid).Sum(x => x.Cours.courseHours).ToString();
            int summ = Convert.ToInt32(sum);


            var price = db.studentCourses.Where(d => d.studentId == Sid).Select(x => x.Student.Major.majorHourPrice).FirstOrDefault();
            int pricee = Convert.ToInt32(price);
            int multi = (summ * pricee) + 50;
            int wallet = (int)db.studentCourses.Where(d => d.studentId == Sid).Select(x => x.Student.wallet).FirstOrDefault();
            int rest = multi - wallet;
            var recordd = db.Students.Find(Sid);
            recordd.wallet = Payment - rest;

            if (ModelState.IsValid)
            {

                db.Payments.Add(PY);

                db.SaveChanges();
                string Email = db.Students.Where(x => x.studentId == Sid).Select(x=>x.userEmail).Single();
                MailMessage mail = new MailMessage();
                mail.To.Add(Email);
                mail.From = new MailAddress("hopeorganization23@gmail.com");
                mail.Subject = "UniCat Admission";
                mail.Body = $"Your Payment of {Payment} JD has been successfully done and your schedual has been set";

                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587; // 25 465
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp.gmail.com";
                smtp.Credentials = new System.Net.NetworkCredential("hopeorganization23@gmail.com", "mbuyaativxrfntjx\r\n");
                smtp.Send(mail);
                return RedirectToAction("Index2");

            }


            return View(PY);
        }

        // GET: studentCours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentCours studentCours = db.studentCourses.Find(id);
            if (studentCours == null)
            {
                return HttpNotFound();
            }
            return View(studentCours);
        }

        // GET: studentCours/Create
        public ActionResult Create()
        {
            ViewBag.courseId = new SelectList(db.Courses, "courseId", "courseName");
            ViewBag.studentId = new SelectList(db.Students, "studentId", "firstName");
            return View();
        }

        // POST: studentCours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,courseId,studentId,isPaid")] studentCours studentCours)
        {
            if (ModelState.IsValid)
            {
                db.studentCourses.Add(studentCours);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.courseId = new SelectList(db.Courses, "courseId", "courseName", studentCours.courseId);
            ViewBag.studentId = new SelectList(db.Students, "studentId", "firstName", studentCours.studentId);
            return View(studentCours);
        }

        // GET: studentCours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentCours studentCours = db.studentCourses.Find(id);
            if (studentCours == null)
            {
                return HttpNotFound();
            }
            ViewBag.courseId = new SelectList(db.Courses, "courseId", "courseName", studentCours.courseId);
            ViewBag.studentId = new SelectList(db.Students, "studentId", "firstName", studentCours.studentId);
            return View(studentCours);
        }

        // POST: studentCours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,courseId,studentId,isPaid")] studentCours studentCours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentCours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.courseId = new SelectList(db.Courses, "courseId", "courseName", studentCours.courseId);
            ViewBag.studentId = new SelectList(db.Students, "studentId", "firstName", studentCours.studentId);
            return View(studentCours);
        }

        // GET: studentCours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentCours studentCours = db.studentCourses.Find(id);
            if (studentCours == null)
            {
                return HttpNotFound();
            }
            return View(studentCours);
        }

        // POST: studentCours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            studentCours studentCours = db.studentCourses.Find(id);
            db.studentCourses.Remove(studentCours);
            db.SaveChanges();
            return RedirectToAction("Index2");
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
