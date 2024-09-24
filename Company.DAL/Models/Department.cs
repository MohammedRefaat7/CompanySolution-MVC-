using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Models
{
    public class Department : BaseEntity
    {
        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(55)]
        
        public string Name { get; set; }
        [Required(ErrorMessage = "Code Is Required")]
        public string Code { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
