using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INV.Models;
using Newtonsoft.Json;

namespace INV.Controllers
{
    public class HomeController : Controller
    {
        InventoryEntities db = new InventoryEntities();

        public ActionResult Index()
        {
            ViewBag.OrdersCount = db.Orders.Count();
            ViewBag.ProductsCount = db.Products.Count();
            ViewBag.CustomerCount = db.Customers.Count();
            ViewBag.SuppliersCount = db.Suppliers.Count();
            ViewBag.LowOfStock = db.Products.Count(p => p.UnitsInStock <= 2);
            ViewBag.OutOfStock = db.Products.Count(p => p.UnitsInStock == 0);
            ViewBag.StockWorth = db.Products.Sum(p => p.UnitsInStock * p.unitePrice);
            return View();
        }

        public ContentResult GetCategoryWorthData()
        {
            List<CategoriesWorthViewModel> Home = new List<CategoriesWorthViewModel>();

            var results = from P in db.Products
                group new {P.Brand.Category, P} by new
                {
                    P.Brand.Category.Name
                }
                into g
                select new
                {
                    g.Key.Name,
                    Category_Worth = (int?) g.Sum(p => p.P.UnitsInStock * p.P.unitePrice)
                };

            foreach (var item in results)
            {
                CategoriesWorthViewModel categoriesWorthVm = new CategoriesWorthViewModel
                {
                    Name = item.Name,
                    CategoryWorth = item.Category_Worth
                };
                Home.Add(categoriesWorthVm);
            }

            return Content(JsonConvert.SerializeObject(Home), "application/json");
        }

        public ContentResult GetCustomerOrdersData()
        {
            List<CustomerOrdersViewModel> Home = new List<CustomerOrdersViewModel>();

            var results = db.Customers.Select(c => new
            {
                c.Name,
                NumOrders = db.Orders.Count(o => o.cust_id == c.cust_id)
            });

            foreach (var item in results)
            {
                CustomerOrdersViewModel customerOrdersVm = new CustomerOrdersViewModel
                {
                    Name = item.Name,
                    OrderCount = item.NumOrders
                };
                Home.Add(customerOrdersVm);
            }

            return Content(JsonConvert.SerializeObject(Home), "application/json");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}