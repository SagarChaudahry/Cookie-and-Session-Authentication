using System.Reflection.Metadata.Ecma335;
using DbFirstCRUD.Data.Entities;
using DbFirstCRUD.Services;
using Microsoft.AspNetCore.Mvc;

namespace DbFirstCRUD.Controllers
{
    public class DesignationController : Controller
    {
        private readonly IDesignationRepository _designationRepository;


        public DesignationController(IDesignationRepository designationRepository)
        {
            _designationRepository = designationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var Designation = new Designation();
            return View(Designation);
        }

        [HttpGet]
        public async Task<IActionResult> GetDesignationById(int DesignationId)
        {
            var Designation = await _designationRepository.GetDesignatioByIdAsync(DesignationId);
            if (Designation == null)
            {
                return View(Designation);
            }
            return Ok(Designation);
        }

        [HttpPost]
        public async Task<IActionResult> AddDesignation(Designation designation)
        {
            if (ModelState.IsValid)
            {
                await _designationRepository.AddDesignation(designation);
                return RedirectToAction("GetDesignations");
            }
            return Ok(designation);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDesignation(Designation designation)
        {
            if (ModelState.IsValid)
            {
                await _designationRepository.UpdateDesignation(designation);
                return RedirectToAction("GetDesignations");
            }
            return View(designation);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDesignation(int DesignationId)
        {
            await _designationRepository.DeleteDesignation(DesignationId);
            return RedirectToAction("GetDesignations");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var designations = await _designationRepository.GetAllDesignations();
            return View(designations);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Designation designation)
        {
            if (ModelState.IsValid)
            {
                await _designationRepository.AddDesignation(designation);
                return RedirectToAction("Index", "Designation");
            }
            return View(designation);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var designation = await _designationRepository.GetDesignatioByIdAsync(id);
            if (designation == null)
            {
                return NotFound();
            }
            return View(designation);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Designation designation)
        {
            if (ModelState.IsValid)
            {
                await _designationRepository.UpdateDesignation(designation);
                return RedirectToAction("Index", "Designation");
            }
            return View(designation);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var designation = await _designationRepository.GetDesignatioByIdAsync(id);
            if (designation == null)
            {
                return NotFound();
            }
            return View(designation);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int DesignationId)
        {
            await _designationRepository.DeleteDesignation(DesignationId);
            return RedirectToAction("Index", "Designation");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var designation = await _designationRepository.GetDesignatioByIdAsync(id);
            if (designation == null)
            {
                return NotFound();
            }
            return View(designation);
        }
    }
}
