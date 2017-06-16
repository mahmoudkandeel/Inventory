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
    public class OrdersController : Controller
    {
        private InventoryEntities db = new InventoryEntities();

        public ActionResult PrintInvoice(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderDetails = order.OrderDetails;
            return View(order);
        }

        public JsonResult getProductCategories()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Category> categories = new List<Category>();
            categories = db.Categories.OrderBy(a => a.Name).ToList();
            return new JsonResult {Data = categories, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        public JsonResult getProductBrands(int categoryID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Brand> brands = new List<Brand>();
            //brands = db.Brands.Where(a => a.cat_id.Equals(categoryID)).OrderBy(a => a.Name).ToList();
            brands = db.Brands.Where(a => a.cat_id == (categoryID)).OrderBy(a => a.Name).ToList();
            return new JsonResult {Data = brands, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        public JsonResult getProducts(int BrandID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Product> products = new List<Product>();
            products = db.Products.Where(a => a.brand_id == (BrandID)).OrderBy(a => a.Name).ToList();
            return new JsonResult {Data = products, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        public JsonResult getProductPrice(int Pro_id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var productPrice = db.Products.Find(Pro_id).unitePrice;
            return new JsonResult {Data = productPrice, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        public JsonResult getProductQuantiy(int Pro_id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var UnitsInStock = db.Products.Find(Pro_id).UnitsInStock;
            return new JsonResult {Data = UnitsInStock, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        [HttpPost]
        public JsonResult save(Order order)
        {
            bool status = false;
            DateTime dateOrg;
            //var isValidDate = DateTime.TryParseExact(order.DeliveryDate, "mm-dd-yyyy", null, System.Globalization.DateTimeStyles.None, out dateOrg);
            var isValidDate = DateTime.TryParseExact(order.OrderDateString, "MM-dd-yyyy", null,
                System.Globalization.DateTimeStyles.None, out dateOrg);
            if (isValidDate)
            {
                order.DateTime = dateOrg;
            }
            order.emp_id = @User.Identity.GetUserId();
            var isValidModel = TryUpdateModel(order);
            if (isValidModel)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                status = true;
            }
            return new JsonResult {Data = new {status = status}};
        }

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Customer).Include(o => o.AspNetUser);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = db.OrderDetails.Where(o=>o.ord_id==id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.cust_id = new SelectList(db.Customers, "cust_id", "Name");
            ViewBag.emp_id = new SelectList(db.AspNetUsers, "Id", "FullName");
            ViewBag.orderNo = new Random().Next();
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ord_id,cust_id,DateTime,DeliveryDate,emp_id,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cust_id = new SelectList(db.Customers, "cust_id", "Name", order.cust_id);
            ViewBag.emp_id = new SelectList(db.AspNetUsers, "emp_id", "Name", order.emp_id);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.cust_id = new SelectList(db.Customers, "cust_id", "Name", order.cust_id);
            ViewBag.emp_id = new SelectList(db.AspNetUsers, "emp_id", "Name", order.emp_id);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ord_id,cust_id,DateTime,DeliveryDate,emp_id,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cust_id = new SelectList(db.Customers, "cust_id", "Name", order.cust_id);
            ViewBag.emp_id = new SelectList(db.AspNetUsers, "Id", "FullName", order.emp_id);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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