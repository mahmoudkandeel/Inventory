using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INV.Models;
using Microsoft.AspNet.Identity;

namespace INV.Controllers
{
    public class SuppliesController : Controller
    {
        private InventoryEntities db = new InventoryEntities();

        public ActionResult PrintInvoice(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supply supply = db.Supplies.Find(id);
            if (supply == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplyDetails = supply.SupplyDetails;
            return View(supply);
        }

        [HttpPost]
        public JsonResult save(Supply supply)
        {
            bool status = false;
            DateTime dateOrg;
            //var isValidDate = DateTime.TryParseExact(order.DeliveryDate, "mm-dd-yyyy", null, System.Globalization.DateTimeStyles.None, out dateOrg);
            var isValidDate = DateTime.TryParseExact(supply.SupplyDateString, "MM-dd-yyyy", null,
                System.Globalization.DateTimeStyles.None, out dateOrg);
            if (isValidDate)
            {
                supply.DateTime = dateOrg;
            }
            supply.emp_id = @User.Identity.GetUserId();
            var isValidModel = TryUpdateModel(supply);
            if (isValidModel)
            {
                db.Supplies.Add(supply);
                db.SaveChanges();
                status = true;
            }
            return new JsonResult {Data = new {status = status}};
        }

        // GET: Supplies
        public ActionResult Index()
        {
            var supplies = db.Supplies.Include(s => s.AspNetUser).Include(s => s.Supplier);
            return View(supplies.ToList());
        }

        // GET: Supplies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var supply = db.SupplyDetails.Where(s => s.supply_id == id);
            if (supply == null)
            {
                return HttpNotFound();
            }
            return View(supply);
        }

        // GET: Supplies/Create
        public ActionResult Create()
        {
            ViewBag.emp_id = new SelectList(db.AspNetUsers, "Id", "Address");
            ViewBag.sup_id = new SelectList(db.Suppliers, "sup_id", "Name");
            ViewBag.supplyNo = new Random().Next();
            return View();
        }

        // POST: Supplies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "supply_id,sup_id,emp_id,SupplyNo,DateTime,Description")] Supply supply)
        {
            if (ModelState.IsValid)
            {
                db.Supplies.Add(supply);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.emp_id = new SelectList(db.AspNetUsers, "Id", "Address", supply.emp_id);
            ViewBag.sup_id = new SelectList(db.Suppliers, "sup_id", "Name", supply.sup_id);
            return View(supply);
        }

        // GET: Supplies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supply supply = db.Supplies.Find(id);
            if (supply == null)
            {
                return HttpNotFound();
            }
            ViewBag.emp_id = new SelectList(db.AspNetUsers, "Id", "Address", supply.emp_id);
            ViewBag.sup_id = new SelectList(db.Suppliers, "sup_id", "Name", supply.sup_id);
            return View(supply);
        }

        // POST: Supplies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "supply_id,sup_id,emp_id,SupplyNo,DateTime,Description")] Supply supply)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supply).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.emp_id = new SelectList(db.AspNetUsers, "Id", "Address", supply.emp_id);
            ViewBag.sup_id = new SelectList(db.Suppliers, "sup_id", "Name", supply.sup_id);
            return View(supply);
        }

        // GET: Supplies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supply supply = db.Supplies.Find(id);
            if (supply == null)
            {
                return HttpNotFound();
            }
            return View(supply);
        }

        // POST: Supplies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supply supply = db.Supplies.Find(id);
            db.Supplies.Remove(supply);
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