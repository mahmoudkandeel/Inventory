using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INV.Models;

namespace INV.Controllers
{
    public class ProductsController : Controller
    {
        public ActionResult LowStockItems()
        {
            var lowStockItems = db.Products.Where(p => p.UnitsInStock <= 2);
            return View(lowStockItems);
        }

        public ActionResult OutStockItems()
        {
            var outStockItems = db.Products.Where(p => p.UnitsInStock == 0);
            return View(outStockItems);
        }

        private InventoryEntities db = new InventoryEntities();

        public ActionResult SearchPrices()
        {
            return View();
        }

        public ActionResult SearchPriceRange(int from, int to)
        {
            ViewBag.searchResult = db.Products.Where(p => p.unitePrice >= from & p.unitePrice <= to);
            return PartialView(ViewBag.searchResult);
        }

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Brand).Include(p => p.Supplier);
            // var products = db.Products.Include(p => p.Brand).Include(p => p.sup_id);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.brand_id = new SelectList(db.Brands, "brand_id", "Name");
            ViewBag.sup_id = new SelectList(db.Suppliers, "sup_id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase img,
            [Bind(Include =
                "pro_id,brand_id,sup_id,Name,Photo,UnitsInStock,unitePrice,ExpireDate,EntryDate,Notes,Description")]
            Product product)
        {
            if (ModelState.IsValid)
            {
                if (img == null)
                {
                    product.Photo = "/Content/Images/Products/Default.jpg";
                }
                else
                {
                    product.Photo = product.Name + product.brand_id + Path.GetExtension(img.FileName);
                    img.SaveAs(Server.MapPath("~/Content/Images/Products/") + product.Photo);
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.brand_id = new SelectList(db.Brands, "brand_id", "Description", product.brand_id);
            ViewBag.sup_id = new SelectList(db.Suppliers, "sup_id", "Name", product.sup_id);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Brands = new SelectList(db.Brands, "brand_id", "Name", product.brand_id);
            ViewBag.Suppliers = new SelectList(db.Suppliers, "sup_id", "Name", product.sup_id);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase img,
            [Bind(Include =
                "pro_id,brand_id,sup_id,Name,Photo,UnitsInStock,unitePrice,ExpireDate,EntryDate,Notes,Description")]
            Product product)
        {
            if (ModelState.IsValid)
            {
                if (img == null)
                {
                    product.Photo = "/Content/Images/Products/Default.jpg";
                }
                else
                {
                    var imgDB = db.Products.Find(product.pro_id).Photo;
                    img.SaveAs(Server.MapPath("~/Content/Images/Products/") + imgDB);
                }
                db.Entry(product).State = EntityState.Modified; //Exception
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Brands = new SelectList(db.Brands, "brand_id", "Description", product.brand_id);
            ViewBag.Suppliers = new SelectList(db.Suppliers, "sup_id", "Name", product.sup_id);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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