using CustomerCrudOperations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerCrudOperations.Controllers
{
    public class CustomerController : Controller
    {
        // GET: /Customer/
        [HttpGet]
        public ActionResult InsertCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertCustomer(Customer objCustomer)
        {
            objCustomer.Birthdate = Convert.ToDateTime(objCustomer.Birthdate);
            if (ModelState.IsValid)
            {
                CustomerDbContext objDB = new CustomerDbContext();
                string result = objDB.AddCustomer(objCustomer);
                TempData["result1"] = result;
                ModelState.Clear(); // clearing model
                return RedirectToAction("ShowAllCustomerDetails");
            }
            else
            {
                ModelState.AddModelError("", "Error in saving data");
                return View();
            }
        }

        [HttpGet]
        public ActionResult ShowAllCustomerDetails()
        {
            Customer objCustomer = new Customer();
            CustomerDbContext objDB = new CustomerDbContext();
            objCustomer.ShowallCustomer = objDB.SelectAllData();
            return View(objCustomer);
        }

        [HttpGet]
        public ActionResult Details(string ID)
        {
            Customer objCustomer = new Customer();
            CustomerDbContext objDB = new CustomerDbContext();
            return View(objDB.SelectDatabyID(ID));
        }

        [HttpGet]
        public ActionResult Edit(string ID)
        {
            Customer objCustomer = new Customer();
            CustomerDbContext objDB = new CustomerDbContext();
            return View(objDB.SelectDatabyID(ID));
        }

        [HttpPost]
        public ActionResult Edit(Customer objCustomer)
        {
            objCustomer.Birthdate = Convert.ToDateTime(objCustomer.Birthdate);
            if (ModelState.IsValid)
            {
                CustomerDbContext objDB = new CustomerDbContext();
                string result = objDB.UpdateCustomer(objCustomer);
                TempData["result2"] = result;
                ModelState.Clear();
                return RedirectToAction("ShowAllCustomerDetails");
            }
            else
            {
                ModelState.AddModelError("", "Error in saving data");
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(String ID)
        {
            CustomerDbContext objDB = new CustomerDbContext();
            int result = objDB.DeleteData(ID);
            TempData["result3"] = result;
            ModelState.Clear();
            return RedirectToAction("ShowAllCustomerDetails");
        }
    }
}