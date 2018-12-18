using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Soul.Models;

namespace Soul.Controllers
{
    public class AdminController : Controller
    {
        private DbModels db = new DbModels();
        // GET: Admin
        public ActionResult Index()
        {
            return View(db.brokers);
        }

        [HttpGet]
        public ActionResult Login()
        {
            Admin adm = new Admin();
            return View(adm);

        }
        [HttpPost]
        public ActionResult Login(Admin adm)
        {
            try
            {
                using (DbModels db = new DbModels())
                {

                    if (db.Admins.Any(x => x.Id == adm.Id && x.Code == adm.Code))
                    {

                        ViewBag.SuccessMessage = "Login Successful";
                        return RedirectToAction("Main");

                    }

                    ViewBag.LoginErrorMessage = "Wrong Email and password";
                }

            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }

            return View("Login", adm);
        }

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult RegisterBroker(broker broker)
        {
            using (DbModels dbModel = new DbModels())
            {
                if (dbModel.brokers.Any(x => x.BrokerID == broker.BrokerID))
                {
                    ViewBag.DuplicateMessage = "BrokerID already exists";
                    return View("RegisterBroker", broker);
                }
                dbModel.brokers.Add(broker);
                dbModel.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registeration successful";
            return View("RegisterBroker", new broker());
        }

    }
}