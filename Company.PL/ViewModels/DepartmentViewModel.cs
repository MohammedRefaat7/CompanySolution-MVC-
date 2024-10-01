using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Company.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool? IsDeleted { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(55)]
        public string Name { get; set; }

        [MinLength(2)]
        [Required(ErrorMessage = "Code Is Required")]
        
        public string Code { get; set; }

        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
