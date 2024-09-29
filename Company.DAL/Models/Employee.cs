using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Models
{
    public class Employee : BaseEntity
    {
        [MaxLength(55)]
        
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        
        public int Age { get; set; }
        
        public decimal? Salary { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string? Email { get; set; }
        public DateTime HiringDate { get; set; }
        public string? ImageURL { get; set; }

        
        public Department? Department { get; set; }     // Navigational Property ... 

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }   
    }
}
