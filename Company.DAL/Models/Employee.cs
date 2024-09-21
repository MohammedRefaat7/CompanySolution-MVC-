using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Models
{
    public class Employee : BaseEntity
    {
        [MaxLength(55)]
        public string Name { get; set; }
        public string? Address { get; set; }
        public decimal? Salary { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public DateTime HiringDate { get; set; }
        public string? ImageURL { get; set; }

        public Department Department { get; set; }
        public int? DepartmentId { get; set; }

    }
}
