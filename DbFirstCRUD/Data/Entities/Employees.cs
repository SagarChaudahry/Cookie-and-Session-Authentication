using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DbFirstCRUD.Data.Entities
{
    public class Employees
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Department")]
        [Required(ErrorMessage = "Department is required.")]
        public int DepartmentId { get; set; }

        [Display(Name = "Designation")]
        [Required(ErrorMessage = "Designation is required.")]
        public int DesignationId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string DesignationName { get; set; } = string.Empty;
        [ValidateNever]
        public IEnumerable<SelectListItem> DepartmentList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> DesignationList { get; set; }
    }
}
