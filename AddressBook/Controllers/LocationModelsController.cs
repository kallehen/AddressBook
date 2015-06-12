using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AddressBook.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AddressBook.Controllers
{
    [Authorize]
    public class LocationModelsController : Controller
    {
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> manager;
        public LocationModelsController()
        {
            db = new ApplicationDbContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: LocationModels
        public ActionResult Index()
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            return View(db.Locations.ToList().Where(locationmodels => locationmodels.User.Id == currentUser.Id));
        }

        // GET: LocationModels/Details/5
        public ActionResult Details(int? id)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationModels locationModels = db.Locations.Find(id);
            if (locationModels == null)
            {
                return HttpNotFound();
            }
             if (locationModels.User.Id != currentUser.Id)
            {
                 return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
               }
            return View(locationModels);
        }

        // GET: LocationModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LocationModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LocationName,LocationPlace")] LocationModels locationModels)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                locationModels.User = currentUser;
                db.Locations.Add(locationModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(locationModels);
        }

        // GET: LocationModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationModels locationModels = db.Locations.Find(id);
            if (locationModels == null)
            {
                return HttpNotFound();
            }
            return View(locationModels);
        }

        // POST: LocationModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LocationName,LocationPlace")] LocationModels locationModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locationModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(locationModels);
        }

        // GET: LocationModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationModels locationModels = db.Locations.Find(id);
            if (locationModels == null)
            {
                return HttpNotFound();
            }
            return View(locationModels);
        }

        // POST: LocationModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LocationModels locationModels = db.Locations.Find(id);
            db.Locations.Remove(locationModels);
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