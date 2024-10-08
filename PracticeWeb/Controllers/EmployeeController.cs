using PracticeWeb.DAL_DataAccessLayer_;
using PracticeWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticeWeb.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _repository;

        public EmployeeController()
        {
            _repository = new EmployeeRepository();
        }

        // Display the list of employees with search and pagination
        public ActionResult Index(string searchTerm = "", int pageNumber = 1, int pageSize = 5)
        {
            var totalEmployees = _repository.GetTotalEmployees(searchTerm);
            var employees = _repository.GetAllEmployees(searchTerm, pageNumber, pageSize);

            ViewBag.SearchTerm = searchTerm;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalEmployees / pageSize);

            return View(employees);
        }

        // Display the form to create a new employee
        public ActionResult Create()
        {
            return View();
        }

        // Handle the form submission to add a new employee
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _repository.AddEmployee(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // Display the form to edit an employee
        public ActionResult Edit(int id)
        {
            var employee = _repository.GetEmployeeById(id);
            if (employee == null)
                return HttpNotFound();

            return View(employee);
        }

        // Handle the form submission to update an employee
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdateEmployee(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // Handle soft deletion of an employee
        public ActionResult Delete(int id)
        {
            _repository.SoftDeleteEmployee(id);
            return RedirectToAction("Index");
        }
    }

}