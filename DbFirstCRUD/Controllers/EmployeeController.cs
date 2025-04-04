using DbFirstCRUD.Data.Entities;
using DbFirstCRUD.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DbFirstCRUD.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IDesignationRepository _designationRepo;

        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepo, IDesignationRepository designationRepo)
        {
            _employeeRepository = employeeRepository;
            _departmentRepo = departmentRepo;
            _designationRepo = designationRepo;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Check if the user is authenticated
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            var employees = await _employeeRepository.GetAllEmployees();
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View(new Employees());
        }

        // Create Employee
        [HttpPost]
        public async Task<IActionResult> Create(Employees employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeRepository.AddEmployee(employee);
                return RedirectToAction("Index");
            }
            await PopulateDropdowns(); 
            return View(employee);
        }

        // Edit Employee
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                ViewBag.Message = "Employee not found";
                return NotFound();
            }

            await PopulateDropdowns(); 
            return View(employee);
        }

        // Edit Employee
        [HttpPost]
        public async Task<IActionResult> Edit(Employees employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeRepository.UpdateEmployee(employee);
                return RedirectToAction("Index");
            }
            await PopulateDropdowns(); 
            return View(employee);
        }

        // Delete Employee
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                ViewBag.Message = "Employee not found";
                return NotFound(); 
            }

            //ViewBag.Message = "Are you sure you want to delete this employee?";
            return View(employee);
        }

        // Delete Employee 

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int EmployeeId)
        {
            await _employeeRepository.DeleteEmployee(EmployeeId);
            return RedirectToAction("Index");
        }





        // Employee Details 
        [HttpGet]
        public async Task<IActionResult> Details(int EmployeeId)
        {
            var employee = await _employeeRepository.GetEmployeeById(EmployeeId);
            if (employee == null)
            {
                return NotFound(); 
            }

            return View(employee);
        }

        // Populate dropdowns for departments and designations
        private async Task PopulateDropdowns()
        {
            var departments = await _departmentRepo.GetAllDepartments();
            ViewBag.Departments = new SelectList(departments, "DepartmentId", "Name");

            var designations = await _designationRepo.GetAllDesignations();
            ViewBag.Designations = new SelectList(designations, "DesignationId", "DesignationName");
        }
    }
}
