using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System;
using Web_Framework.Models;
using Web_Framework.Context;
using System.Linq;
using System.Security.Claims;
using System.Data;
using System.Net;

namespace Web_Framework.Controllers
{
        public class EmployeeController : Controller
        {
            MyContext myContext;

            public EmployeeController(MyContext myContext)
        {
            this.myContext=myContext;
        }

        public IActionResult Index()
        {
            var data = myContext.Employee.ToList();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                myContext.Employee.Add(employee);
                var result = myContext.SaveChanges();
                if(result > 0)
                {
                    return RedirectToAction("Index","Employee");
                }
            }
            return View();
        }
        public ActionResult Edit(int? id, bool? saveChangesError = false)
        {

            Employee employee = myContext.Employee.Find(id);
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Employee model)
        {
            try
            {
                Employee employee = myContext.Employee.Find(id);
                employee.Id = model.Id;
                employee.Name = model.Name;
                employee.Email = model.Email;
                myContext.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("Edit", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index", "Employee");
        }
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
        
            Employee employee = myContext.Employee.Find(id);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Employee employee = myContext.Employee.Find(id);
                myContext.Employee.Remove(employee);
                myContext.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index","Employee");
        }
    }
}
