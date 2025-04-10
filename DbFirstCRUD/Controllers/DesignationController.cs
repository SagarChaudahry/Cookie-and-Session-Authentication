using DbFirstCRUD.CustomJwtFilter;
using DbFirstCRUD.Data.Entities;
using DbFirstCRUD.Services;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;

namespace DbFirstCRUD.Controllers
{
    [ServiceFilter(typeof(JwtAuthorizeFilter))]
    public class DesignationController : Controller
    {
        private readonly IDesignationRepository _designationRepository;

        public DesignationController(IDesignationRepository designationRepository)
        {
            _designationRepository = designationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            int pageSize = 5;
            var designations = await _designationRepository.GetDesignationsPaged(pageNumber, pageSize);
            var totalCount = await _designationRepository.GetTotalDesignationCount();

            var viewModel = new PaginatedDesignationViewModel
            {
                Designations = designations.ToList(),
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalCount / pageSize)
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Designation());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Designation designation)
        {
            if (ModelState.IsValid)
            {
                await _designationRepository.AddDesignation(designation);
                return RedirectToAction("Index");
            }
            return View(designation);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var designation = await _designationRepository.GetDesignatioByIdAsync(id);
            if (designation == null)
                return NotFound();

            return View(designation);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Designation designation)
        {
            if (ModelState.IsValid)
            {
                await _designationRepository.UpdateDesignation(designation);
                return RedirectToAction("Index");
            }
            return View(designation);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var designation = await _designationRepository.GetDesignatioByIdAsync(id);
            if (designation == null)
                return NotFound();

            return View(designation);
        }



        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _designationRepository.DeleteDesignation(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var designation = await _designationRepository.GetDesignatioByIdAsync(id);
            if (designation == null)
                return NotFound();

            return View(designation);
        }

        [HttpGet]
        public async Task<IActionResult> GetDesignationById(int designationId)
        {
            var designation = await _designationRepository.GetDesignatioByIdAsync(designationId);
            if (designation == null)
                return NotFound();

            return Ok(designation);
        }

        [HttpPost]
        public async Task<IActionResult> AddDesignation(Designation designation)
        {
            if (ModelState.IsValid)
            {
                await _designationRepository.AddDesignation(designation);
                return RedirectToAction("Index");
            }
            return View(designation);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDesignation(Designation designation)
        {
            if (ModelState.IsValid)
            {
                await _designationRepository.UpdateDesignation(designation);
                return RedirectToAction("Index");
            }
            return View(designation);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDesignation(int designationId)
        {
            await _designationRepository.DeleteDesignation(designationId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ExportToPdf()
        {
            var departments = await _designationRepository.GetAllDesignations();

            return new ViewAsPdf("DesignationPdfView", departments.ToList())
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                FileName = "DesignationsList.pdf"
            };
        }
    }
}