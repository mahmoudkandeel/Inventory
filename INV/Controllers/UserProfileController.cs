using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INV.Models;
using Microsoft.AspNet.Identity;

namespace INV.Controllers
{
    public class UserProfileController : Controller
    {
        InventoryEntities db = new InventoryEntities();
        public ActionResult Activity()
        {
            var userId = User.Identity.GetUserId();
            var emporders = db.AspNetUsers.Find(userId).Orders;

            return PartialView(emporders);
        }
        // GET: UserProfile
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var user = db.AspNetUsers.Find(userId);
            return View(user);
        }

        [HttpPost]
        public ActionResult Index(AspNetUser userdata)
        {
            var userId = User.Identity.GetUserId();
            var user = db.AspNetUsers.Find(userId);

            if (ModelState.IsValid)
            {
                user.FullName = userdata.FullName;
                user.Email = userdata.Email;
                user.PhoneNumber = userdata.PhoneNumber;
                user.Salary = userdata.Salary;
                user.Address = userdata.Address;
                user.HireDate = userdata.HireDate;

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View("Index", user);
        }
    }
}