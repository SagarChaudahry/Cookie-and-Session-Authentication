using DbFirstCRUD.CustomJwtFilter;
using DbFirstCRUD.Data.Entities;
using DbFirstCRUD.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;

namespace DbFirstCRUD.Controllers
{
    [ServiceFilter(typeof(JwtAuthorizeFilter))]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;


        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        
        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            int pageSize = 5; // Set the number of records per page
            var departments = await _departmentRepository.GetDepartmentsPaged(pageNumber, pageSize); 
            var totalCount = await _departmentRepository.GetTotalDepartmentCount(); 
            var viewModel = new PaginatedDepartmentViewModel 
            {
                Departments = departments.ToList(), 
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };

            return View(viewModel); // Return the view with the view model containing department data
        }


        // GET: Department/Create
        [HttpGet]

        public IActionResult Create()
        {
            return View(new Department());
        }

        // POST: Department/Create
        [HttpPost]

        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                await _departmentRepository.AddDepartment(department);
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Department/Edit/1
        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            var department = await _departmentRepository.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound(); // Return a 404 error if the department is not found
            }
            return View(department);
        }

        // POST: Department/Edit
        [HttpPost]

        public async Task<IActionResult> Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                await _departmentRepository.UpdateDepartment(department);
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Department/Details/1
        [HttpGet]

        public async Task<IActionResult> Details(int id)
        {
            var department = await _departmentRepository.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound(); // Return a 404 error if the department is not found
            }
            return View(department);
        }

        // GET: Department/Delete/1
        [HttpGet]

        public async Task<IActionResult> Delete(int id)
        {
            var department = await _departmentRepository.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound(); // Return a 404 error if the department is not found
            }
            return View(department);
        }

        // POST: Department/Delete/1
        [HttpPost]

        [ActionName("Delete")] // Specify that this method handles the delete action
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _departmentRepository.DeleteDepartment(id);
            return RedirectToAction("Index"); // Redirect to the index action after deletion
        }


        [HttpGet]
        public async Task<IActionResult> ExportToPdf()
        {
            var departments = await _departmentRepository.GetAllDepartments();

            return new ViewAsPdf("DepartmentPdfView", departments.ToList())
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                FileName = "DepartmentList.pdf"
            };
        }
    }
}
