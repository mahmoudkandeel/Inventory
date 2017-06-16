using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INV.Models;

namespace INV.Controllers
{
    public class OrderDetailsController : Controller
    {
        private InventoryEntities db = new InventoryEntities();

        public ActionResult New()
        {
            ViewBag.ord_id = new SelectList(db.Orders, "ord_id", "Status");
            ViewBag.pro_id = new SelectList(db.Products, "pro_id", "Name");
            return PartialView();
        }

        // GET: OrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.ord_id = new SelectList(db.Orders, "ord_id", "Status");
            ViewBag.pro_id = new SelectList(db.Products, "pro_id", "Name");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "ord_id,pro_id,UnitPrice,Quantity,Discount")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Create", "Orders");
            }

            ViewBag.ord_id = new SelectList(db.Orders, "ord_id", "Status", orderDetail.ord_id);
            ViewBag.pro_id = new SelectList(db.Products, "pro_id", "Name", orderDetail.pro_id);
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ord_id = new SelectList(db.Orders, "ord_id", "Status", orderDetail.ord_id);
            ViewBag.pro_id = new SelectList(db.Products, "pro_id", "Name", orderDetail.pro_id);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ord_id,pro_id,UnitPrice,Quantity,Discount")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ord_id = new SelectList(db.Orders, "ord_id", "Status", orderDetail.ord_id);
            ViewBag.pro_id = new SelectList(db.Products, "pro_id", "Name", orderDetail.pro_id);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
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