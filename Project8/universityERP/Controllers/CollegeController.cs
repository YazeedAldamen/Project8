using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using universityERP.Models;

namespace universityERP.Controllers
{
    public class CollegeController : ApiController
    {
        private universityERPEntities db = new universityERPEntities();

        // GET: api/College
        public IQueryable<Facility> GetFacilities()
        {
            return db.Facilities;
        }

        // GET: api/College/5
        [ResponseType(typeof(Facility))]
        public IHttpActionResult GetFacility(int id)
        {
            Facility facility = db.Facilities.Find(id);
            if (facility == null)
            {
                return NotFound();
            }

            return Ok(facility);
        }

        // PUT: api/College/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFacility(int id, Facility facility)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != facility.facilityId)
            {
                return BadRequest();
            }

            db.Entry(facility).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacilityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/College
        [ResponseType(typeof(Facility))]
        public IHttpActionResult PostFacility(Facility facility)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Facilities.Add(facility);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = facility.facilityId }, facility);
        }

        // DELETE: api/College/5
        [ResponseType(typeof(Facility))]
        public IHttpActionResult DeleteFacility(int id)
        {
            Facility facility = db.Facilities.Find(id);
            if (facility == null)
            {
                return NotFound();
            }

            db.Facilities.Remove(facility);
            db.SaveChanges();

            return Ok(facility);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FacilityExists(int id)
        {
            return db.Facilities.Count(e => e.facilityId == id) > 0;
        }
    }
}